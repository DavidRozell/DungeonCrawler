using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Material hurt;
    public Material normal;
    public Material normalArmor;
    public GameObject sword;
    public GameObject armor;
    public GameObject playerMesh;
    public Text keyUI;
    public Text swordUI;
    public Text healthUI;
    public Text armorHealthUI;
    public Text enemyHealthUI;
    public bool hasKey = false;
    public bool hasSword = false;
    public bool hasArmor = false;
    public bool damaged = false;
    public bool died = false;
    public bool vulnerable = false;
    public int health = 100;
    public int armorHealth = 40;
    public bool attacking = false;
    public bool coolDown = false;
    private Animator playerAnim;
    private float speed = 3.6f;
    private float turnSpeed = 90;
    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
        if (forwardInput > 0 | forwardInput < 0)
        {
            playerAnim.SetBool("isWalking", true);
        }

        if (forwardInput == 0)
        {
            playerAnim.SetBool("isWalking", false);
        }
        healthUI.text = health.ToString();
        armorHealthUI.text = armorHealth.ToString();

        if (Input.GetKeyDown(KeyCode.Space) & attacking == false & coolDown == false & damaged == false)//& hasSword == true & coolDown == false & damaged == false)
        {
            playerAnim.SetBool("isAttacking", true);
            StartCoroutine(SwordAttacking());
        }

        if (health <= 0 & died == false)
        {
            StartCoroutine(Dead());
        }

        if (armorHealth <= 0)
        {
            hasArmor = false;
            Destroy(armor.gameObject);
            armorHealthUI.gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyArm"))
        {
            if (health > 0 & damaged == false & vulnerable == true)
            {
                if (hasArmor == true)
                {
                    damaged = true;
                    armorHealth = armorHealth - 10;
                }
                else
                {
                    damaged = true;
                    health = health - 15;

                    if (health <= 0)
                    {
                        health = 0;
                    }
                }
                StartCoroutine(Hurt());
            }
        }
    }

    IEnumerator Dead()
    {
        died = true;
        playerAnim.SetBool("died", true);
        playerAnim.SetBool("isAttacking", false);
        playerAnim.SetBool("isWalking", false);
        coolDown = true;
        speed = 0;
        turnSpeed = 0;
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Hurt()
    {
        playerAnim.SetBool("isAttacking", false);
        playerAnim.SetBool("isWalking", false);
        if (hasArmor == true)
        {
            armor.gameObject.GetComponent<Renderer>().material = hurt;
            yield return new WaitForSeconds(1);
            damaged = false;
            armor.gameObject.GetComponent<Renderer>().material = normalArmor;
        }
        else
        {
            if (health > 0)
            {
                playerMesh.gameObject.GetComponent<Renderer>().material = hurt;
            }
            yield return new WaitForSeconds(1);
            playerMesh.gameObject.GetComponent<Renderer>().material = normal;
            damaged = false;
        }
    }

    IEnumerator SwordAttacking()
    {
        coolDown = true;
        speed = 0;
        turnSpeed = 0;
        playerAnim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.7f);
        attacking = true;
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        playerAnim.SetBool("isAttacking", false);
        speed = 3.6f;
        turnSpeed = 90;
        yield return new WaitForSeconds(2);
        coolDown = false;
    }
}
