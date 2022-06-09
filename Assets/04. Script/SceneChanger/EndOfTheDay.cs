// 결산창 관련 로직, UI canvas에 부착, EndOfTheDayPanel 연결

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfTheDay : MonoBehaviour
{
    // 수동으로 연결
    [Header("Manual Link")]
    public ItemDataBaseObject itemDataBaseObject;
    // EndOfTheDayPanel 연결
    [SerializeField]
    private GameObject endOfTheDayPanel;
    private PlayerScript playerScript;
    private TempPlayer tempPlayer;
    private ConditionController conditionController;
    // 변경사항 목록
    private List<string> changedStatusTextList = new List<string>();
    private List<string> changedItemTextList = new List<string>();
    // 자동으로 연결
    [Header("Automatical Link")]
    public int HEALTH_INCREASE = 20;
    public int HUNGRY_DECREASE = -50;
    public int THIRSTY_DECREASE = -50;
    public int MENTAL_DECREASE = -40;
    // toggle
    // Start is called before the first frame update
    void Start()
    {
        // endButton.GetComponent<Button>().
        playerScript = FindObjectOfType<PlayerScript>();
        tempPlayer = FindObjectOfType<TempPlayer>();
        conditionController = FindObjectOfType<ConditionController>();
    }

    private TextMeshProUGUI eventChangeText, ChangeText;

    private void PanelChange()
    {
        // call texts
        eventChangeText = GameObject.Find("RandomEventText").GetComponent<TextMeshProUGUI>();
        ChangeText = GameObject.Find("ChangeText").GetComponent<TextMeshProUGUI>();
        // random event
        int eventIdx = RandomEvent(eventWeight);
        eventChangeText.text = eventText[eventIdx];
        // change
        switch (eventIdx)
        {
            // 이벤트 없음
            case 0:
                break;
            // 물자 획득
            case 1:
                // 인벤토리가 꽉 찬 경우, 추가 처리
                int itemDataBaseLength = itemDataBaseObject.Items.Length;
                int randomItemId = Random.Range(0, itemDataBaseLength);
                ItemObject itemObject = itemDataBaseObject.Items[randomItemId];
                Item newItem = itemObject.CreateItem();
                newItem.Name = itemObject.name;
                // Item newItem1 = new Item(itemObject);
                // newItem1.MaxStackSize = 1;
                // newItem1.Name = "~~";
                // newItem1.Id = 1;
                // newItem1.states = new ItemStateMaker[];
                tempPlayer.inventory.AddItem(newItem, 1);
                AddItemChange(true, newItem.Name);
                break;
            // 감기
            case 2:
                if (!playerScript.playerObject.ChangeDisease(playerScript.playerObject.COLD))
                    // eventChangeText.text += " BUT, you already have a cold!";
                    eventChangeText.text = "감기는 여전하다. 그래도 더 악화되진 않는다는 사실이 다행일까.";
                else
                {
                    AddStatusChange(false, playerScript.playerObject.HEALTH);
                    AddStatusChange(false, playerScript.playerObject.THIRSTY);
                    AddStatusChange(false, playerScript.playerObject.MENTAL);
                    AddStatusChange(false, playerScript.playerObject.MOVE_SPEED);
                }
                break;
            // 강도
            case 3:
                // 가지고 있던 물자가 없던 경우 처리, 특수 아이템 처리
                InventorySlot[] playerItems = tempPlayer.inventory.Container.Items;
                int inventoryLength = playerItems.Length;
                int randInventoryIdx = Random.Range(0, inventoryLength);
                Item randItem = playerItems[randInventoryIdx].item;
                tempPlayer.inventory.SubtractItem(randItem, 1);
                AddItemChange(false, randItem.Name);
                playerScript.playerObject.ChangeHealthPoint(-10);
                break;
            // 비
            case 4:
                // 환경
                playerScript.playerObject.ChangeThirstyPoint(10);
                playerScript.playerObject.ChangeMentalPoint(-10);
                AddStatusChange(true, playerScript.playerObject.THIRSTY);
                AddStatusChange(false, playerScript.playerObject.MENTAL);
                break;
        }
        string statusChangeText = string.Join("\n", changedStatusTextList);
        string ItemChangeText = string.Join("\n", changedItemTextList);
        ChangeText.text = string.Concat(statusChangeText, ItemChangeText);
    }

    public void CallEndOfTheDay()
    {
        // Debug.Log("CallEndOfTheDay");
        endOfTheDayPanel.SetActive(true);
        PanelChange();
        Time.timeScale = 0f;
    }

    public void CloseEndOfTheDay()
    {
        // Debug.Log("CloseEndOfTheDay");
        endOfTheDayPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void EndTheDay()
    {
        // Debug.Log("EndTheDay");
        CloseEndOfTheDay();
        // Debug.Log("SceneChange");
        ChangeStatusSet();
        if (playerScript.playerObject.isDead)
            {}
        else if (conditionController.NextDay())
            SceneManager.LoadScene("HideOut");
        else
            SceneManager.LoadScene("Ending_Survived");
        // StartCoroutine()
    }

    // IEnumerator<>

    public void ChangeStatusSet()
    {
        playerScript.playerObject.ChangeHealthPoint(HEALTH_INCREASE);
        playerScript.playerObject.ChangeHungryPoint(HUNGRY_DECREASE);
        playerScript.playerObject.ChangeThirstyPoint(THIRSTY_DECREASE);
        playerScript.playerObject.ChangeMentalPoint(MENTAL_DECREASE);
    }

    public int NOTHING = 0, SUPPLY = 1, DISEASE = 2, ROBBER = 3;
    private string[] eventText = {
        "특별한 일은 일어나지 않았다. 아무런 일도 일어나지 않았다는 사실이 다행이다.",
        "피신처 안에서 물자를 발견했다. 또 다른 물자가 있는지 더 찾아봐야겠다.",
        "감기에 걸렸다. 몸 상태가 영 좋지 않아 큰일이다.",
        "강도가 들었어 물자를 빼았기고 부상을 입었으나 어떻게든 쫓아내는 데 성공했다. 부상이 심해지면 안 될 텐데.",
        "오늘은 비가 온다. 하늘은 어둡고, 공기는 무겁다. 그나마 빗물을 이용할 수 있다는 사실이 다행이다."
    };
    // private string[] eventText = {
    //     "Nothing happened.",
    //     "You found some stuff!",
    //     "You got disease.",
    //     "Robberies stole some stuff! You are damaged.",
    //     "It's raining. The atmosphere is damp, heavy and gloomy."
    // };
    public float[] eventWeight = { 20, 15, 5, 15, 10 };

    private int RandomEvent(float[] probs)
    {
        float total = 0;
        foreach (float elem in probs)
        {
            total += elem;
        }
        float randomPoint = Random.value * total;
        for (int i= 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
                return i;
            else 
                randomPoint -= probs[i];
        }
        return probs.Length - 1;
    }

    private void AddStatusChange(bool isGet, string stat)
    {
        if (isGet)
            changedStatusTextList.Add(stat + "이 증가하였습니다");
        else
            changedStatusTextList.Add(stat + "이 감소하였습니다");
        // Debug.Log(stat + " 변경이 기록되었습니다.");
    }
    
    private void AddItemChange(bool isGet, string item)     // ItemObject를 받기
    {
        if (isGet is true)
        {
            changedItemTextList.Add(item + "을 획득하였습니다.");
            // Debug.Log(item + "을 획득하였습니다.");
        }
        else
        {
            changedItemTextList.Add(item + "을 잃었습니다.");
            // Debug.Log(item + "을 잃었습니다.");
        }
    }
}
