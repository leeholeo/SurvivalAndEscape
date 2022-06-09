// 밤에 할 행동 체크, trigger object를 만들고 script를 부착하여 사용

using UnityEngine;

public class ScavengeCheck : MonoBehaviour
{
    private IntoTheNight intoTheNight;

    void Start()
    {
        intoTheNight = FindObjectOfType<IntoTheNight>();
    }

    private void OnTriggerEnter(Collider other)
    {
        intoTheNight.Ready(intoTheNight.SCAVENGE);
    }

    private void OnTriggerExit(Collider other)
    {
        intoTheNight.NotReady(intoTheNight.SCAVENGE);
    }
}
