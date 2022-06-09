using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public string DialogText = "";
    public InventoryObject inventory;
    private GameObject Main;

    // Start is called before the first frame update
    void Start()
    {
        Main = GameObject.Find("Main_Script");
        inventory.InitSpace();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(this.inventory);
            Main.GetComponent<MainScript>().ObjectDialogEnter(DialogText, this.inventory);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Main.GetComponent<MainScript>().ObjectDialogExit();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
    }
}
