using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;

public class MainScript : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    
    private GameObject PlayerDialog;
    private GameObject PlayerInventory;
    private DynamicInterface PlayerInventorySpec;
    private GameObject ObjectDialog;
    private GameObject ObjectInventory;
    private StaticInterface ObjectInventorySpec;
    private TextMeshProUGUI ObjectDialogText;
    private InputDevice targetDevice;
    private bool curActiveStat = false;
    private bool curObjEnter = false;

    public InventoryObject inventory;
    public ConditionController conditionController;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDialog = GameObject.Find("PlayerDialog");
        PlayerDialog.SetActive(false);

        PlayerInventory = GameObject.Find("PlayerInventory");
        PlayerInventorySpec = PlayerInventory.GetComponent<DynamicInterface>();
        PlayerInventorySpec.InitializeSlot();
        PlayerInventory.SetActive(false);

        ObjectDialog = GameObject.Find("ObjectDialog");
        ObjectDialogText = GameObject.Find("ObjectDialogText").GetComponent<TextMeshProUGUI>();
        Debug.Log(ObjectDialogText);
        ObjectDialog.SetActive(false);

        ObjectInventory = GameObject.Find("ObjectInventory");
        ObjectInventorySpec = ObjectInventory.GetComponent<StaticInterface>();
        ObjectInventory.SetActive(false);

        conditionController = GameObject.FindObjectOfType<ConditionController>();

        inventory.Load();
        // Debug.Log("inventory: " + inventory.Container.Count);

        List<InputDevice> devices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool primaryButtonValue);
        if (primaryButtonValue)
        {
            OnButtonPressed();
        }
    }

    public void ObjectDialogEnter(string text, InventoryObject _inventory)
    {
        PlayerDialog.SetActive(true);
        PlayerInventory.SetActive(true);
        ObjectDialogText.text = text;
        ObjectDialog.SetActive(true);
        ObjectInventorySpec.inventory = _inventory;
        ObjectInventorySpec.InitializeSlot();
        ObjectInventory.SetActive(true);
        curObjEnter = true;
    }

    public void ObjectDialogExit()
    {
        PlayerDialog.SetActive(false);
        PlayerInventory.SetActive(false);
        ObjectDialogText.text = "";
        ObjectDialog.SetActive(false);
        ObjectInventory.SetActive(false);
        ObjectInventorySpec.inventory = null;
        ObjectInventorySpec.DistructSlot();
        curObjEnter = false;
        curActiveStat = false;
    }

    public void OnButtonPressed()
    {
        if (curActiveStat == false && curObjEnter == false)
        {
            PlayerDialog.SetActive(true);
            PlayerInventory.SetActive(true);
            curActiveStat = true;
        }
        else if (curActiveStat && curObjEnter == false)
        {
            PlayerDialog.SetActive(false);
            PlayerInventory.SetActive(false);
            curActiveStat = false;
        }
    }

    public void OnApplicationQuit()
    {
        if (conditionController.isSave)
        {
            inventory.Save();
            inventory.Container.Items = new InventorySlot[16];
        }
        else
        {
            inventory.Delete();
        }
    }
}
