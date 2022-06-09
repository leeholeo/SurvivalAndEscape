//  가구 오브젝트에 부착, type을 지정하면 type에 맞는 Furniture 오브젝트를 자동으로 생성하여 사용

using UnityEngine;

public class BathScript : MonoBehaviour
{
    // 수동으로 연결
    [Header("Manual Link")]
    public GameObject amendPanel;
    public BathObject bathObject;
    public InventoryObject inventory;
    // 자동으로 연결
    [Header("Automatical Link")]
    public string DialogText = "Bath";
    private GameObject Main;
    void Awake()
    {
        bathObject.Enable();
        Main = GameObject.Find("Main");
    }

    public void OnDestroy()
    {
        bathObject.Destroy();
        Debug.Log("BedObject Script Destroyed");
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            if (!bathObject.isAmended)
            {
                amendPanel.SetActive(true);
                bathObject.amendObject.TriggerEnter(other);
            }
            else
            {
                Debug.Log(this.inventory);
                Main.GetComponent<MainScript>().ObjectDialogEnter(DialogText, this.inventory);
            }
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            if (amendPanel.activeSelf)
            {
                bathObject.amendObject.TriggerExit(other);
                amendPanel.SetActive(false);
            }
            else
            {
                Main.GetComponent<MainScript>().ObjectDialogExit();
            }
        }
    }

    // 개선 가능
    public void OnTriggerStay(Collider other)
    {
        if (bathObject.isAmended && amendPanel.activeSelf)
        {
            bathObject.amendObject.TriggerExit(other);
            amendPanel.SetActive(false);
            Main.GetComponent<MainScript>().ObjectDialogEnter(DialogText, this.inventory);
        }
    }
}
