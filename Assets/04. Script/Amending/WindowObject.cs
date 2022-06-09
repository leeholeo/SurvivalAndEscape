// Player Script에서 자동으로 생성되어 사용, AmendPanel object를 연결, AmendPanel 하위의 AmendButton을 찾아 연결

using UnityEngine;

[CreateAssetMenu(fileName = "New Window Object", menuName = "Furniture/Window")]
public class WindowObject : FurnitureObject
{
    public override void Enable()
    {
        base.Enable();
        type = FurnitureType.Window;
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
        endOfTheDay.MENTAL_DECREASE += 10;
        endOfTheDay.eventWeight[endOfTheDay.ROBBER] -= 10;
        if (endOfTheDay.eventWeight[endOfTheDay.ROBBER] < 0)
        {
            endOfTheDay.eventWeight[endOfTheDay.ROBBER] = 0;
        }
        // Debug.Log("WindowObject Amend");
    }

    public override void Use()
    {
        base.Use();
    }


    // public override void Debugging()
    // {
    //     Debug.Log($"WindowObject Debugging");
    //     base.Debugging();
    // }
}