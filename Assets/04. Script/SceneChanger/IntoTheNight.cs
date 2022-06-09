// 낮에서 밤으로 이동, UI canvas에 부착, IntoTheNightPanel 연결

using UnityEngine;
using UnityEngine.SceneManagement;

public class IntoTheNight : MonoBehaviour
{
    // 수동으로 연결
    [Header("Manual Link")]
    public GameObject scavengeMapPanel;
    public GameObject scavengeText;
    // 자동으로 연결
    [Header("Automatical Link")]
    [HideInInspector]
    public int SCAVENGE = 0, GUARD = 1, SLEEP = 2;
    private EndOfTheDay endOfTheDay;
    [HideInInspector]
    public int SCAVENGE_TYPE = 0, STAY_TYPE = 1;
    private int[] behaviourType = new int[] { 0, 1, 1 };
    private bool[] isReady = new bool[] { false, false };
    // 임시
    private bool[] isPartnerReady = new bool[] { true, true };
    // private bool[] isPartnerReady = new bool[] { false, false, false };
    private string gameObjectName;
    private string sceneName;

    void Start()
    {
        endOfTheDay = FindObjectOfType<EndOfTheDay>();
    }

    void Update()
    {
        
    }
    
    public void Ready(int behaviour)
    {
        // Debug.Log("ReadyCheck");
        int type = behaviourType[behaviour];
        isReady[type] = true;
        if (PartnerCheck(type))
        {
            // Debug.Log("PartnerCheck");
            // Debug.Log(type);
            if (type == SCAVENGE_TYPE)
            {
                // Debug.Log("ScavengeTypeCheck");
                OpenMap();
            }
            else if (type == STAY_TYPE)
            {
                // Debug.Log("StayTypeCheck");
                endOfTheDay.CallEndOfTheDay();
            }
        }
    }

    public void NotReady(int behaviour)
    {
        int type = behaviourType[behaviour];
        // map이 열려 있으면 닫기
        if (behaviour == SCAVENGE)
            CloseMap();
        isReady[type] = false;
    }

    public bool PartnerCheck(int _type)
    {
        if (isPartnerReady[_type])
            return true;
        else
            return false;
    }

    private void OpenMap()
    {
        scavengeMapPanel.SetActive(true);
        scavengeText.SetActive(true);
    }

    private void CloseMap()
    {
        scavengeMapPanel.SetActive(false);
        scavengeText.SetActive(false);
    }

    public void ToSceneByName(string _scenename)
    {
        CloseMap();
        SceneManager.LoadScene(_scenename);
    }
}
