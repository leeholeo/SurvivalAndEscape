// Player Script에서 자동으로 생성되어 사용, AmendPanel object를 연결, AmendPanel 하위의 AmendButton을 찾아 연결

using UnityEngine;

[CreateAssetMenu(fileName = "New Bath Object", menuName = "Furniture/Bath")]
public class BathObject : FurnitureObject
{
    public override void Enable()
    {
        base.Enable();
        type = FurnitureType.Bath;
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
        // Debug.Log("BathObject Amend");
    }

    public override void Use()
    {
        base.Use();
    }


    // public override void Debugging()
    // {
    //     Debug.Log($"BathObject Debugging");
    //     base.Debugging();
    // }
}