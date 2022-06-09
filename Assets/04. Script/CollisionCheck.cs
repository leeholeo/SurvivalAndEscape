using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   void OnTriggerEnter(Collider other)
    {
        // NPC
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Hide");
        }
    }
    void OnTriggerExit(Collider other)
    {
        // 원하는 코드 작성
    }
}
