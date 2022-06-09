// // 체력 감소 test 

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class StatusTest : MonoBehaviour
// {
//     private PlayerScript playerScript;
//     void Start()
//     {
//         playerScript = FindObjectOfType<PlayerScript>();
//     }

//     public int changePoint = -1;
//     private bool isTriggered = false;
//     void Update()
//     {
//         // if (isTriggered)
//         //     if (playerScript.playerObject.ChangeHealthPoint(changePoint) is false)
//         //         isTriggered = false;
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         // isTriggered = true;
//         playerScript.playerObject.ChangeHealthPoint(-50);
//     }
// }
