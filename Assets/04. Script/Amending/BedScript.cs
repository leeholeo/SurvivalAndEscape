//  가구 오브젝트에 부착, type을 지정하면 type에 맞는 Furniture 오브젝트를 자동으로 생성하여 사용

using UnityEngine;

public class BedScript : MonoBehaviour
{
    // 수동으로 연결
    [Header("Manual Link")]
    public GameObject amendPanel;
    public BedObject bedObject;
    void Awake()
    {
        bedObject.Enable();
    }

    public void OnDestroy()
    {
        bedObject.Destroy();
        // Debug.Log("BedObject Script Destroyed");
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            if (!bedObject.isAmended)
            {
                amendPanel.SetActive(true);
                bedObject.amendObject.TriggerEnter(other);
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            if (amendPanel.activeSelf)
            {
                bedObject.amendObject.TriggerExit(other);
                amendPanel.SetActive(false);
            }
        }
    }

    // 개선 가능
    public void OnTriggerStay(Collider other)
    {
        if (bedObject.isAmended && amendPanel.activeSelf)
        {
            bedObject.amendObject.TriggerExit(other);
            amendPanel.SetActive(false);
        }
    }
}
