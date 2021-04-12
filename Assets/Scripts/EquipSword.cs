using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSword : MonoBehaviour
{
    public GameObject player;
    public bool strong;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;

            if (strong == false & player.GetComponent<PlayerController>().hasStrongSword == false)
            {
                player.GetComponent<PlayerController>().normalSwordUI.gameObject.SetActive(true);
                GameObject.Find("PlayerSwordNormal").gameObject.GetComponent<MeshRenderer>().enabled = true;
                Destroy(gameObject);
            }
            else
            {
                if (strong == true)
                {
                    player.GetComponent<PlayerController>().hasStrongSword = true;
                    player.GetComponent<PlayerController>().normalSwordUI.gameObject.SetActive(false);
                    player.GetComponent<PlayerController>().strongSwordUI.gameObject.SetActive(true);
                    GameObject.Find("PlayerSwordNormal").gameObject.GetComponent<MeshRenderer>().enabled = false;
                    GameObject.Find("PlayerSwordStrong").gameObject.GetComponent<MeshRenderer>().enabled = true;
                    Destroy(gameObject);
                }
            }
            
            if (player.GetComponent<PlayerController>().hasSword == false)
            {
                player.GetComponent<PlayerController>().hasSword = true;
            }
        }
    }
}
