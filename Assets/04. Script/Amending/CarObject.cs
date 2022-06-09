// Player Script에서 자동으로 생성되어 사용, AmendPanel object를 연결, AmendPanel 하위의 AmendButton을 찾아 연결

using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Car Object", menuName = "Furniture/Car")]
public class CarObject : FurnitureObject
{
    public override void Enable()
    {
        base.Enable();
        type = FurnitureType.Car;
        savePath = string.Concat(Application.persistentDataPath, "/", this.name, ".save");
        if (!Load())
        {
            isAmended = false;
            furnitureToken = new FurnitureToken();
        }
    }

    // private void Update() {
    //     if (amendObject.isAmended)
    //     {
    //         Amend();
    //     }
    // }

    public override void Amend()
    {
        base.Amend();
        // Debug.Log("CarObject Amend");
        ConditionController conditionController = GameObject.FindObjectOfType<ConditionController>();
        conditionController.isSave = false;
        // PlayerScript playerScript = GameObject.FindObjectOfType<PlayerScript>();
        // playerScript.playerObject.isDead = true;
        SceneManager.LoadScene("Ending_Escape");
    }

    public override void Use()
    {
        base.Use();
    }


    // public override void Debugging()
    // {
    //     Debug.Log($"CarObject Debugging");
    //     base.Debugging();
    // }
}