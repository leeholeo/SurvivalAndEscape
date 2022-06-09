using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerheat : MonoBehaviour
{
    public Transform player;
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
        if (player.transform.name == other.transform.name) {
            print("attack");
        }
    }
}
