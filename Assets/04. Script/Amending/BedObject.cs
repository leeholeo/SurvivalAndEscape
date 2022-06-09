// Player Script에서 자동으로 생성되어 사용, AmendPanel object를 연결, AmendPanel 하위의 AmendButton을 찾아 연결

using UnityEngine;

[CreateAssetMenu(fileName = "New Bed Object", menuName = "Furniture/Bed")]
public class BedObject : FurnitureObject
{
    public override void Enable()
    {
        base.Enable();
        type = FurnitureType.Bed;
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
        endOfTheDay.HEALTH_INCREASE += 10;
        endOfTheDay.HUNGRY_DECREASE += 10;
        endOfTheDay.THIRSTY_DECREASE += 10;
        endOfTheDay.MENTAL_DECREASE += 10;
        // Debug.Log("BedObject Amend");
    }

    public override void Use()
    {
        base.Use();
    }


    // public override void Debugging()
    // {
    //     Debug.Log($"BedObject Debugging");
    //     base.Debugging();
    // }
}