using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipArmor : MonoBehaviour
{
    public bool hasArmor = false;
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
            if (other.gameObject.GetComponent<PlayerController>().hasArmor == false)
            {
                hasArmor = true;
                player = other.gameObject;
                player.GetComponent<PlayerController>().armorHealth = 40;
                player.GetComponent<PlayerController>().armorHealthUI.gameObject.SetActive(true);
                player.GetComponent<PlayerController>().hasArmor = true;
                GameObject.Find("PlayerShield").gameObject.GetComponent<MeshRenderer>().enabled = true;
                Destroy(gameObject);
            }
        }
    }
}
