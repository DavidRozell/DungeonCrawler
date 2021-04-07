using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    public Material doorOpened;
    public GameObject doorMesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().hasKey == true)
            {
                collision.gameObject.GetComponent<PlayerController>().hasKey = false;
                collision.gameObject.GetComponent<PlayerController>().keyUI.gameObject.SetActive(false);
                gameObject.GetComponent<MeshCollider>().enabled = false;
                doorMesh.gameObject.GetComponent<Renderer>().material = doorOpened;
            }
        }
    }

}
