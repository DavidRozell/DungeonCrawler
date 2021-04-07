using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipKey : MonoBehaviour
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
            if (other.gameObject.GetComponent<PlayerController>().hasKey == false)
            {
                player = other.gameObject;
                player.GetComponent<PlayerController>().keyUI.gameObject.SetActive(true);
                player.GetComponent<PlayerController>().hasKey = true;
                Destroy(gameObject);
            }
        }
    }

}
