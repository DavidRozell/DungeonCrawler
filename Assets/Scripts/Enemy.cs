using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Material hurt;
    public Material normal;
    public GameObject skeletonMesh;
    public float speed;
    public int health;
    private GameObject player;
    private Animator enemyAnim;
    private float detectionradius = 4f;
    private bool showingHealth = false;
    private bool swinging = false;
    public bool damaged = false;
    private bool cooldown = false;
    private bool died = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < 0.8f & died == false & cooldown == false & damaged == false & swinging == false & player.gameObject.GetComponent<PlayerController>().died == false)
        {
            StartCoroutine(Swing());
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= detectionradius & died == false & cooldown == false & Vector3.Distance(player.transform.position, transform.position) > 0.8f & damaged == false & swinging == false & player.gameObject.GetComponent<PlayerController>().died == false)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            lookDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(lookDirection);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            detectionradius = 6f;
            enemyAnim.SetBool("isWalking", true);
        }

        if (Vector3.Distance(player.transform.position, transform.position) > detectionradius & died == false)
        {
            enemyAnim.SetBool("isWalking", false);
        }

        if (health <= 0 & died == false)
        {
            died = true;
            enemyAnim.SetBool("died", true);
            enemyAnim.SetBool("isWalking", false);
            enemyAnim.SetBool("isSwinging", false);
            speed = 0;
            StartCoroutine(Dead());

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            if (player.gameObject.GetComponent<PlayerController>().attacking == true & died == false & damaged == false)
            {
                enemyAnim.SetBool("isDamaged", true);
                health = health - 10;
                StartCoroutine(Hurt());
                if (health > 0 & showingHealth == false)
                {
                    player.GetComponent<PlayerController>().enemyHealthUI.text = health.ToString();
                    player.GetComponent<PlayerController>().enemyHealthUI.gameObject.SetActive(true);
                    StartCoroutine(ShowHealth());
                }
            }
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(4.5f);
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        transform.position = new Vector3(transform.position.x, 1.661f, transform.position.z);
    }

    IEnumerator Swing()
    {
        cooldown = true;
        swinging = true;
        enemyAnim.SetBool("isWalking", false);
        enemyAnim.SetBool("isSwinging", true);
        yield return new WaitForSeconds(0.4f);
        player.gameObject.GetComponent<PlayerController>().vulnerable = true;
        yield return new WaitForSeconds(0.3f);
        enemyAnim.SetBool("isSwinging", false);
        player.gameObject.GetComponent<PlayerController>().vulnerable = false;
        swinging = false;
        yield return new WaitForSeconds(1);
        cooldown = false;
    }

    IEnumerator Hurt()
    {
        if (health > 0)
        {
            skeletonMesh.gameObject.GetComponent<Renderer>().material = hurt;
        }
        damaged = true;
        enemyAnim.SetBool("isSwinging", false);
        enemyAnim.SetBool("isWalking", false);
        yield return new WaitForSeconds(1);
        skeletonMesh.gameObject.GetComponent<Renderer>().material = normal;
        enemyAnim.SetBool("isDamaged", false);
        damaged = false;
    }

    IEnumerator ShowHealth()
    {
        player.GetComponent<PlayerController>().enemyHealthUI.text = health.ToString();
        player.GetComponent<PlayerController>().enemyHealthUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        player.GetComponent<PlayerController>().enemyHealthUI.gameObject.SetActive(false);
    }
}
