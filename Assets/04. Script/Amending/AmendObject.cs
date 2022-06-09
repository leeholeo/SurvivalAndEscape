// 수리 가능한 object

using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Amend Object", menuName = "Amend")]
public class AmendObject : ScriptableObject
{
    // 수동으로 연결
    [Header("Manual Link")]
    public ItemDataBaseObject itemDataBaseObject;
    public GameObject itemDetailPrefab;
    public Item[] requiredItemArray;
    public int[] requiredItemNumArray;
    public int itemArrayLength;
    // 자동으로 연결
    [Header("Automatical Link")]
    public float Y_START = 225f;
    public float Y_OFFSET = -150f;
    public int MENTAL_PLUS = 30;
    public InventoryObject playerInventory;
    public GameObject amendItemPanel;
    public Button amendButton;
    public GameObject[] requiredItemDetailPanelArray;
    public int[] currentItemNumArray;
    public bool[] isSufficientArray;
    public bool isSufficient = new bool();
    // public List<Tuple<int, int>>[] requiredItemIdxAndCntList;
    public delegate void AmendEvent();
    public event AmendEvent amendEvent;

    public void Enable()
    {
    
        currentItemNumArray = new int[itemArrayLength];
        isSufficientArray = new bool[itemArrayLength];
        requiredItemDetailPanelArray = new GameObject[itemArrayLength];
        // amendButton = GameObject.Find("AmendButton").GetComponent<Button>();
    }
    
    public void TriggerEnter(Collider other)
    {
        playerInventory = other.transform.Find("Player").GetComponent<TempPlayer>().inventory;
        // AmendPanel 하위의 AmendButton을 찾아 연결
        TextMeshProUGUI amendTitleText = GameObject.Find("AmendTitleText").GetComponent<TextMeshProUGUI>();
        // TextMeshProUGUI amendTitleText = amendPanel.transform.Find("AmendTitlePanel").gameObject.transform.Find("AmendTitleText").gameObject.GetComponent<TextMeshProUGUI>();
        amendTitleText.text = name;
        amendButton = GameObject.Find("AmendButton").GetComponent<Button>();
        amendButton.onClick.AddListener(Amend);
        amendItemPanel = GameObject.Find("AmendItemPanel");
        FillCurrent();
        CreateItemDetailPanel();
        FillDetailPanel();
    }
    
    public void TriggerExit(Collider other)
    {
        // AmendPanel 하위의 AmendButton을 찾아 연결
        // amendButton = amendPanel.transform.Find("AmendButton").GetComponent<Button>();
        DestroyDetailPanel();
        TextMeshProUGUI amendTitleText = GameObject.Find("AmendTitleText").GetComponent<TextMeshProUGUI>();
        amendTitleText.text = "";
        amendButton.onClick.RemoveListener(Amend);
    }
    
    public void FillCurrent()
    {
        // requiredItemArray에 들어있는 아이템을 순회하며 플레이어 인벤토리에 들어있는 개수를 확인
        isSufficient = true;
        for (int itemIdx = 0; itemIdx < itemArrayLength; itemIdx++)
        {
            // List<Tuple<int, int>> itemIdxAndCntList;
            currentItemNumArray[itemIdx] = playerInventory.CheckItem(requiredItemArray[itemIdx]);
            // (currentItemNumArray[itemIdx], itemIdxAndCntList) = playerInventory.CheckItem(requiredItemArray[itemIdx]);
            // requiredItemIdxAndCntList[itemIdx] = itemIdxAndCntList;
            // Debug.Log("currentItemNumArray");
            // Debug.Log(currentItemNumArray.Length);
            // Debug.Log("requiredItemNumArray");
            // Debug.Log(requiredItemNumArray.Length);
            // Debug.Log("itemIdx");
            // Debug.Log(itemIdx);
            if (currentItemNumArray[itemIdx] >= requiredItemNumArray[itemIdx])
                isSufficientArray[itemIdx] = true;
            else
                isSufficient = false;
        }

        if (isSufficient)
            amendButton.interactable = true;
        else
            amendButton.interactable = false;
    }

    public void CreateItemDetailPanel()
    {
        for (int itemIdx = 0; itemIdx < itemArrayLength; itemIdx++)
        {
            Vector3 originVector = amendItemPanel.transform.position;
            Vector3 newVector = new Vector3(0, Y_START + Y_OFFSET * itemIdx, 0);
            requiredItemDetailPanelArray[itemIdx] = Instantiate(itemDetailPrefab, newVector, Quaternion.identity);
            requiredItemDetailPanelArray[itemIdx].transform.SetParent(amendItemPanel.transform, false);
            // Debug.Log($"{itemIdx}번째 slot created");
        }
    }

    public void FillDetailPanel()
    {
        if (requiredItemDetailPanelArray.Length > 0)
        {
            for (int itemIdx = 0; itemIdx < itemArrayLength; itemIdx++)
            {
                Image itemDetailImagePanel = requiredItemDetailPanelArray[itemIdx].transform.Find("AmendItemImagePanel").GetComponent<Image>();
                // itemDetailImagePanel.color = new Color(1, 1, 1, 1);
                itemDetailImagePanel.sprite = itemDataBaseObject.GetItem[requiredItemArray[itemIdx].Id].uiDisplay;
                TextMeshProUGUI itemDetailText = requiredItemDetailPanelArray[itemIdx].transform.Find("AmendItemCountPanel").transform.Find("AmendItemCountText").GetComponent<TextMeshProUGUI>();
                itemDetailText.text = $"{currentItemNumArray[itemIdx]} / {requiredItemNumArray[itemIdx]}";
                if (isSufficientArray[itemIdx])
                    itemDetailText.color = new Color32(0, 255, 0, 255);
                else
                    itemDetailText.color = new Color32(255, 0, 0, 255);
            }
        }
    }
    
    public void DestroyDetailPanel()
    {
        if (requiredItemDetailPanelArray.Length > 0)
        {
            for (int itemIdx = 0; itemIdx < itemArrayLength; itemIdx++)
            {
                TextMeshProUGUI itemDetailText = requiredItemDetailPanelArray[itemIdx].transform.Find("AmendItemCountPanel").transform.Find("AmendItemCountText").GetComponent<TextMeshProUGUI>();
                itemDetailText.text = "";
                itemDetailText.color = new Color32(255, 255, 255, 255);
                Destroy(requiredItemDetailPanelArray[itemIdx]);
            }
            requiredItemDetailPanelArray = new GameObject[itemArrayLength];
        }
    }

    public void Amend()
    {
        // 모두 다 requiredItemNumArray에 들어있는 아이템 요구 개수보다 크면 그만큼 인벤토리에서 제거
        if (isSufficient)
        {
            for (int itemIdx = 0; itemIdx < itemArrayLength; itemIdx++)
            {
                Item item = requiredItemArray[itemIdx];
                int requiredItemNum = requiredItemNumArray[itemIdx];
                // 아이템 차감이 제대로 이우러지지 않을 경우 필요
                playerInventory.SubtractItem(item, requiredItemNum);
            }
            PlayerObject playerObject;
            playerObject = FindObjectOfType<PlayerObject>();
            playerObject.ChangeMentalPoint(MENTAL_PLUS);
            // Debug.Log(amendEvent);
            if (amendEvent != null)
                amendEvent();
            // Destroy(this);
        }
    }
}