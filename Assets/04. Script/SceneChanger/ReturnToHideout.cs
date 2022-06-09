// test용 씬 이동 로직

using UnityEngine;

public class ReturnToHideout : MonoBehaviour
{
    // 수동으로 연결
    [Header("Manual Link")]
    private EndOfTheDay endOfTheDay;
    // 자동으로 연결
    [Header("Automatical Link")]
    public bool isReady = new bool();
    public bool isPartnerReady = true;

    private void Start()
    {
        endOfTheDay = FindObjectOfType<EndOfTheDay>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Ready();
    }

    private void OnTriggerExit(Collider other)
    {
        NotReady();
    }

    public void Ready()
    {
        // Debug.Log("ReadyCheck");
        isReady = true;
        if (PartnerCheck())
        {
            // Debug.Log("PartnerReady");
            endOfTheDay.CallEndOfTheDay();
        }
    }

    public void NotReady()
    {
        isReady = false;
    }

    public bool PartnerCheck()
    {
        if (isPartnerReady)
            return true;
        else
            return false;
    }
}
