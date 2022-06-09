using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickToMainScene(){
        // 메인 화면으로 이동
        SceneManager.LoadScene("Opening");
        // Debug.Log("메인화면으로 이동");
    } 
}
