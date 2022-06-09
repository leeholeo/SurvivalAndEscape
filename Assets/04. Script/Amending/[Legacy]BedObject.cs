// // Player Script에서 자동으로 생성되어 사용, AmendPanel object를 연결, AmendPanel 하위의 AmendButton을 찾아 연결

// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// [CreateAssetMenu(fileName = "New Bed Object", menuName = "Furniture/Bed")]
// public class Legacy_BedObject : FurnitureObject
// {
//     public AmendObject amendObject;
//     public bool isAmended = new bool();
//     public GameObject amendPanel;
//     public Button amendButton;
//     public InventoryObject playerInventory;

//     public override void Awake()
//     {
//         type = FurnitureType.Bed;
//         amendObject = ScriptableObject.CreateInstance<AmendObject>();
//         amendObject.name = "Bed Amend Object";
//         // amendObject.OnEnable()
//     }

//     public IEnumerator ConnectPanel()
//     {
//         // AmendPanel object를 연결
//         amendPanel = GameObject.Find("AmendPanel");
//         Debug.Log("bedobject amendPanel connected");
//         yield return null;
//         yield return amendObject.ConnectPanel();
//     }

//     public override void Use()
//     {
//         base.Use();
//     }

//     public void TriggerEnter(Collider other)
//     {
//         if (isAmended)
//         {
//             // use
//         }
//         else
//         {
//             playerInventory = other.GetComponent<TempPlayer>().inventory;
//             // AmendPanel 하위의 AmendButton을 찾아 연결
//             TextMeshProUGUI amendTitleText = GameObject.Find("AmendTitleText").GetComponent<TextMeshProUGUI>();
//             // TextMeshProUGUI amendTitleText = amendPanel.transform.Find("AmendTitlePanel").gameObject.transform.Find("AmendTitleText").gameObject.GetComponent<TextMeshProUGUI>();
//             amendTitleText.text = name;
//             amendButton = amendPanel.transform.Find("AmendButton").GetComponent<Button>();
//             amendButton.onClick.AddListener(Amend);
//             amendObject.FillCurrent(playerInventory);
//             amendObject.CreateItemDetailPanel();
//             amendObject.FillDetailPanel();
//         }
//     }

//     public void TriggerExit(Collider other)
//     {
//         if (isAmended)
//         {
//             // use
//         }
//         else
//         {
//             // AmendPanel 하위의 AmendButton을 찾아 연결
//             // amendButton = amendPanel.transform.Find("AmendButton").GetComponent<Button>();
//             amendObject.DestroyDetailPanel();
//             TextMeshProUGUI amendTitleText = GameObject.Find("AmendTitleText").GetComponent<TextMeshProUGUI>();
//             amendTitleText.text = "";
//             amendButton.onClick.RemoveListener(Amend);
//         }
//     }

//     public void Amend()
//     {
//         if (playerInventory)
//         {
//             if (amendObject.Amend(playerInventory))
//             {
//                 isAmended = true;
//                 EndOfTheDay endOfTheDay;
//                 endOfTheDay = FindObjectOfType<EndOfTheDay>();
//                 endOfTheDay.MENTAL_DECREASE += 10;
//                 Destroy(amendObject);
//             }
//         }
//     }

//     public void Debugging()
//     {
//         Debug.Log($"isAmended: {isAmended}");
//     }
// }