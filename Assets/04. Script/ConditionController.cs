using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class ConditionController : MonoBehaviour
{
    // 자동으로 연결
    [Header("Automatical Link")]
    public string savePath;
    public ConditionControllerToken conditionControllerToken;
    public int day;
    public int DAY_LIMIT = 5;
    public bool isSave = true;
    public bool isEnd = false;
    // Start is called before the first frame update
    void Awake()
    {
        savePath = string.Concat(Application.persistentDataPath, "/", this.name, ".save");
        if (!Load())
        {
            day = 0;
            conditionControllerToken = new ConditionControllerToken();
        }
    }

    public bool NextDay()
    {
        day++;
        if (day == DAY_LIMIT)
        {
            isSave = false;
            return false;
        }
        else
            return true;
    }

    private void OnDestroy() {
        if (isEnd)
            Delete();
        else if (isSave)
            Save();
        else
        {
            isEnd = true;
            Save();
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        UpdateToken();
        IFormatter formatter = new BinaryFormatter();
        Stream stream =  new FileStream(savePath, FileMode.Create, FileAccess.Write);
        Debug.Log("Saving Started");
        formatter.Serialize(stream, conditionControllerToken);
        stream.Close();
    }

    [ContextMenu("Load")]
    public bool Load()
    {
        if (File.Exists(savePath))
        {
            Debug.Log("Loading Started");
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(savePath, FileMode.Open, FileAccess.Read);
            try
            {
                conditionControllerToken = (ConditionControllerToken)formatter.Deserialize(stream);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"error occured while loading!: {e}");
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
            Debug.Log("Save deleted");
        }
    }
    
    public void UpdateToken()
    {
        conditionControllerToken.day = day;
        Debug.Log("Token Updated");
    }

    public void DownloadToken()
    {
        // Character
        day = conditionControllerToken.day;
        Debug.Log("Token Downloaded");
    }
}

[System.Serializable]
public class ConditionControllerToken
{
    public int day;
}
