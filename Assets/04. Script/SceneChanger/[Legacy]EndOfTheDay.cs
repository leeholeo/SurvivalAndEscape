// // 결산창 관련 로직, UI canvas에 부착, EndOfTheDayPanel 연결

// using System.Collections;
// using System.Collections.Generic;
// using TMPro;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class EndOfTheDayLegacy : MonoBehaviour
// {
//     // 수동으로 연결
//     [Header("Manual Link")]
//     // EndOfTheDayPanel 연결
//     [SerializeField]
//     private GameObject endOfTheDayPanel;
//     // 자동으로 연결
//     [Header("Automatical Link")]
//     private PlayerScript playerScript;
//     // private CharacterStatus characterStatus;
//     // 변경사항 목록
//     private List<string> changedStatusTextList = new List<string>();
//     private List<string> changedDiseaseTextList = new List<string>();
//     private List<string> changedItemTextList = new List<string>();
//     private TextMeshProUGUI eventChangeText, statusChangeText, itemChangeText;
//     // toggle
//     // private bool isEndOfTheDay = false;
//     // Start is called before the first frame update
//     private int NOTHING = 0, SUPPLY = 1, DISEASE_COLD = 2, DISEASE_INFECTION = 3, DISEASE_TETANUS = 4;
//     // private string[] eventText = {
//     //     "특별한 일은 일어나지 않았습니다.",
//     //     "피신처 안에서 물자를 발견하였습니다!",
//     //     "감기에 걸렸습니다.",
//     //     "작은 상처로 인해 감염되었습니다.",
//     //     "녹슨 못에 찔려 파상풍에 걸렸습니다."
//     // };
//     public string[] eventText = {
//         "Nothing happened.",
//         "You found some stuff!",
//         "You got cold.",
//         "You got infected.",
//         "You got tetanus."
//     };
//     public float[] eventWeight = { 20, 15, 5, 3, 2 };
    
//     void Start()
//     {
//         // endButton.GetComponent<Button>().
//         playerScript = FindObjectOfType<PlayerScript>();
//         // characterStatus = FindObjectOfType<CharacterStatus>();
//         AddStatusChange(30, playerScript.playerObject.HEALTH);
//         AddStatusChange(20, playerScript.playerObject.HEALTH);
//         AddStatusChange(-30, playerScript.playerObject.STAMINA);
//         AddStatusChange(-20, playerScript.playerObject.HUNGRY);
//         AddDiseaseChange(playerScript.playerObject.NO_DISEASE);
//         AddDiseaseChange(playerScript.playerObject.INFECTION);
//         AddDiseaseChange(playerScript.playerObject.COLD);
//         AddDiseaseChange(playerScript.playerObject.TETANUS);
//         AddItemChange(true, "물");
//         AddItemChange(true, "물");
//         AddItemChange(false, "빵");
//         AddItemChange(true, "밤빵");
//         AddItemChange(false, "단팥빵");
//     }

//     void Update()
//     {
//         // if (Input.GetKeyDown(KeyCode.P))
//         // {
//         //     if (isEndOfTheDay)
//         //         CloseEndOfTheDay();
//         //     else
//         //         CallEndOfTheDay();
//         // }
//     }

//     private void PanelChange()
//     {
//         // call texts
//         eventChangeText = GameObject.Find("RandomEvent").GetComponent<TextMeshProUGUI>();
//         statusChangeText = GameObject.Find("StatusChange").GetComponent<TextMeshProUGUI>();
//         itemChangeText = GameObject.Find("ItemChange").GetComponent<TextMeshProUGUI>();
//         // random event
//         int eventIdx = RandomEvent(eventWeight);
//         eventChangeText.text = eventText[eventIdx];
//         // status change
//         statusChangeText.text = string.Join("\n", changedStatusTextList);
//         // item change
//         itemChangeText.text = string.Join("\n", changedItemTextList);
//     }

//     public void CallEndOfTheDay()
//     {
//         Debug.Log("CallEndOfTheDay");
//         // isEndOfTheDay = true;
//         endOfTheDayPanel.SetActive(true);
//         PanelChange();
//         Time.timeScale = 0f;
//     }

//     public void CloseEndOfTheDay()
//     {
//         // isEndOfTheDay = false;
//         Debug.Log("CloseEndOfTheDay");
//         endOfTheDayPanel.SetActive(false);
//         Time.timeScale = 1f;
//     }

//     public void EndTheDay()
//     {
//         Debug.Log("EndTheDay");
//         CloseEndOfTheDay();
//         Debug.Log("SceneChange");
//         SceneManager.LoadScene("HideOut");
//     }

//     private int RandomEvent(float[] probs)
//     {
//         float total = 0;

//         foreach (float elem in probs) {
//             total += elem;
//         }

//         float randomPoint = Random.value * total;

//         for (int i= 0; i < probs.Length; i++) {
//             if (randomPoint < probs[i]) {
//                 return i;
//             }
//             else {
//                 randomPoint -= probs[i];
//             }
//         }
//         return probs.Length - 1;
//     }

//     private void AddStatusChange(int amount, string stat)
//     {
//         if (amount > 0)
//             changedStatusTextList.Add(stat + "이 증가하였습니다");
//         else if (amount < 0)
//             changedStatusTextList.Add(stat + "이 감소하였습니다");
//         else
//             Debug.LogWarning(stat + "변경에 0을 입력하지 마십시오.");
//         Debug.Log(stat + "이 변경이 기록되었습니다.");
//     }
    
//     private void AddDiseaseChange(int disease)
//     {
//         if (disease == 0)
//         {
//             changedDiseaseTextList.Add(playerScript.playerObject.diseaseText[disease] + "상태가 되었습니다.");
//             Debug.Log(playerScript.playerObject.diseaseText[disease] + "상태가 되었습니다.");
//         }
//         else if (playerScript.playerObject.disease < disease)
//         {
//             changedDiseaseTextList.Add(playerScript.playerObject.diseaseText[disease] + "에 걸렸습니다!");
//             Debug.Log(playerScript.playerObject.diseaseText[disease] + "에 걸렸습니다!");
//         }
//     }
    
//     private void AddItemChange(bool isGet, string item)     // ItemObject를 받기
//     {
//         if (isGet is true)
//         {
//             changedItemTextList.Add(item + "을 획득하였습니다.");
//             Debug.Log(item + "을 획득하였습니다.");
//         }
//         else
//         {
//             changedItemTextList.Add(item + "을 잃었습니다.");
//             Debug.Log(item + "을 잃었습니다.");
//         }
//     }
// }
