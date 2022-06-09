/* 가구들의 최상위 클래스, Amend scriptable object를 보유하고 있어 수리 가능하며
사용 가능한 기능들을 보유하고 있다.
*/

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;


public enum FurnitureType
{
    Bed,
    Car,
    Radio,
    Bath,
    Window,
}

public abstract class FurnitureObject : ScriptableObject
{
    // 수동으로 연결
    [Header("Manual Link")]
    public AmendObject amendObject;
    // 자동으로 연결
    [Header("Automatical Link")]
    public FurnitureType type;
    [TextArea(5,10)]
    public string description;
    public ConditionController conditionController;
    public EndOfTheDay endOfTheDay;
    public string savePath;
    public bool isAmended;
    public FurnitureToken furnitureToken;

    public virtual void Enable()
    {
        amendObject.Enable();
        // Debug.Log("FurnitureObject OnEnabled");
        conditionController = FindObjectOfType<ConditionController>();
        amendObject.amendEvent += Amend;
        endOfTheDay = FindObjectOfType<EndOfTheDay>();
    }

    public void Destroy()
    {
        // Debug.Log("PlayerObject Destroyed");
        // Debug.Log("conditionController.isSave");
        // Debug.Log(conditionController.isSave);
        if (conditionController.isSave)
            Save();
        else
            Delete();
    }
    
    public virtual void Amend()
    {
        // Debug.Log("FurnitureObject Amend");
        isAmended = true;
    }

    public virtual void Use()
    {
        // Debug.Log("FurnitureObject Use");
    }

    public virtual void Debugging()
    {
        // Debug.Log($"FurnitureObject debugging");
    }

    [ContextMenu("Save")]
    public void Save()
    {
        UpdateToken();
        IFormatter formatter = new BinaryFormatter();
        Stream stream =  new FileStream(savePath, FileMode.Create, FileAccess.Write);
        // Debug.Log("Saving Started");
        formatter.Serialize(stream, furnitureToken);
        stream.Close();
    }

    [ContextMenu("Load")]
    public bool Load()
    {
        if (File.Exists(savePath))
        {
            // Debug.Log("Loading Started");
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read);
            try
            {
                furnitureToken = (FurnitureToken)formatter.Deserialize(stream);
            }
            catch (System.Exception e)
            {
                // Debug.LogWarning($"error occured while loading!: {e}");
                Delete();
                return false;
            }
            DownloadToken();
            stream.Close();
            return true;
        }
        return false;
    }

    public void Delete()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            // Debug.Log("Save deleted");
        }
    }
    
    public void UpdateToken()
    {
        furnitureToken.isAmended = isAmended;
        // Debug.Log("Token Updated");
    }

    public void DownloadToken()
    {
        // Character
        isAmended = furnitureToken.isAmended;
        // Debug.Log("Token Downloaded");
    }
}

[System.Serializable]
public class FurnitureToken
{
    public bool isAmended;
}
