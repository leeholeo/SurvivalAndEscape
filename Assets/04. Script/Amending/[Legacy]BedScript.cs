// //  가구 오브젝트에 부착, type을 지정하면 type에 맞는 Furniture 오브젝트를 자동으로 생성하여 사용

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Legacy_BedScript : MonoBehaviour
// {
//     [HideInInspector]
//     public BedObject bedObject;
//     public Item[] amendItemArray;
//     public int[] amendItemNumArray;
//     public GameObject amendPanel;
//     private Collider coll;
//     // Start is called before the first frame update
//     void Awake()
//     {
//         bedObject = ScriptableObject.CreateInstance<BedObject>();
//         bedObject.amendObject.requiredItemArray = amendItemArray;
//         // Debug.LogError("requiredItemArray filled");
//         bedObject.amendObject.requiredItemNumArray = amendItemNumArray;
//         // Debug.LogError("requiredItemNumArray filled");
//         bedObject.amendObject.SetArray();
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         coll = other;
//         StartCoroutine("TriggerEnterCoroutine");
//     }

//     IEnumerator TriggerEnterCoroutine()
//     {
//         amendPanel.SetActive(true);
//         yield return null;
//         yield return bedObject.ConnectPanel();
//         yield return null;
//         bedObject.TriggerEnter(coll);
//     }

//     private void OnTriggerExit()
//     {
//         StartCoroutine("TriggerExitCoroutine");
//     }

//     IEnumerator TriggerExitCoroutine()
//     {
//         bedObject.TriggerExit(coll);
//         yield return null;
//         amendPanel.SetActive(false);
//         coll = null;
//     }
// }
