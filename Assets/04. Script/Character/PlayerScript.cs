// player가 되는 object에 부착, player object를 자동으로 생성하여 사용

using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // 자동으로 연결
    [Header("Automatical Link")]
    public PlayerObject playerObject;
    // Start is called before the first frame update
    void Awake()
    {
        playerObject = ScriptableObject.CreateInstance<PlayerObject>();
        playerObject.name = string.Concat(gameObject.name, "Object");
    }

    public void OnDestroy()
    {
        DestroyImmediate(playerObject);
        // Debug.Log("Player Script Destroyed");
        // playerObject.Save();
    }

    public void OnApplicationQuit()
    {
        playerObject.Save();
        playerObject = ScriptableObject.CreateInstance<PlayerObject>();
    }
}