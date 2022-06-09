//  가구 오브젝트에 부착, type을 지정하면 type에 맞는 Furniture 오브젝트를 자동으로 생성하여 사용

using UnityEngine;

public class CarScript : MonoBehaviour
{
    // 수동으로 연결
    [Header("Manual Link")]
    public GameObject amendPanel;
    public CarObject carObject;
    void Awake()
    {
        carObject.Enable();
    }

    public void OnDestroy()
    {
        carObject.Destroy();
        // Debug.Log("CarObject Script Destroyed");
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            if (!carObject.isAmended)
            {
                amendPanel.SetActive(true);
                carObject.amendObject.TriggerEnter(other);
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            if (amendPanel.activeSelf)
            {
                carObject.amendObject.TriggerExit(other);
                amendPanel.SetActive(false);
            }
        }
    }

    // 개선 가능
    public void OnTriggerStay(Collider other)
    {
        if (carObject.isAmended && amendPanel.activeSelf)
        {
            carObject.amendObject.TriggerExit(other);
            amendPanel.SetActive(false);
        }
    }
}
