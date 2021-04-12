using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPotion : MonoBehaviour
{
    public GameObject player;

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
            if (other.gameObject.GetComponent<PlayerController>().health < 100)
            {
                other.gameObject.gameObject.GetComponent<PlayerController>().health = other.gameObject.gameObject.GetComponent<PlayerController>().health + 40;
                if (other.gameObject.gameObject.GetComponent<PlayerController>().health > 100)
                {
                    other.gameObject.gameObject.GetComponent<PlayerController>().health = 100;
                }
                Destroy(gameObject);
            }
        }
    }
}
