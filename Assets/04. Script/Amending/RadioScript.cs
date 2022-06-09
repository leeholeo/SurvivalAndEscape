//  가구 오브젝트에 부착, type을 지정하면 type에 맞는 Furniture 오브젝트를 자동으로 생성하여 사용

using UnityEngine;

public class RadioScript : MonoBehaviour
{
    // 수동으로 연결
    [Header("Manual Link")]
    public GameObject amendPanel;
    public GameObject radioPanel;
    public RadioObject radioObject;
    void Awake()
    {
        radioObject.Enable();
    }

    public void OnDestroy()
    {
        radioObject.Destroy();
        // Debug.Log("RadioObject Script Destroyed");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!radioObject.isAmended)
            {
                amendPanel.SetActive(true);
                radioObject.amendObject.TriggerEnter(other);
            }
            else
            {
                radioPanel.SetActive(true);
                radioObject.FillRadioText();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!amendPanel.activeSelf)
            {
                radioObject.FillBlankRadioText();
                radioPanel.SetActive(false);
            }
            else
            {
                radioObject.amendObject.TriggerExit(other);
                amendPanel.SetActive(false);
            }
        }
    }

    // 개선 가능
    public void OnTriggerStay(Collider other)
    {
        if (radioObject.isAmended && amendPanel.activeSelf)
        {
            radioObject.amendObject.TriggerExit(other);
            amendPanel.SetActive(false);
            radioPanel.SetActive(true);
            radioObject.FillRadioText();
        }
    }
}
