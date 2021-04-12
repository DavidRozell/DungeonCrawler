using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    public bool canOpen = false;
    private Animator doorAnim;
    private bool cooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().hasKey == true & canOpen == false)
            {
                canOpen = true;
                collision.gameObject.GetComponent<PlayerController>().hasKey = false;
                collision.gameObject.GetComponent<PlayerController>().keyUI.gameObject.SetActive(false);
            }

            if (canOpen == true & cooldown == false)
            {
                StartCoroutine(Open());
            }
        }
    }

    IEnumerator Open()
    {
        cooldown = true;
        doorAnim.SetBool("open", true);
        yield return new WaitForSeconds(0.05f);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        doorAnim.SetBool("open", false);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(1);
        cooldown = false;

    }

}
