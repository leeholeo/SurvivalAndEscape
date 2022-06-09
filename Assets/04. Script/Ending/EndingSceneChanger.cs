// 조건이 충족 되었을 때, 엔딩 씬으로 전환해주는 스크립트 입니다.
// 조건 1. 탈출에 필요한 모든 아이템들을 모았을 경우 
// 조건 2. 5일동안 생존에 성공한 경우.
// 조건 3. 게임오버

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneChanger : MonoBehaviour
{
    // 충돌시 씬을 전환해주는 함수
    public int ending_flag = 0;
    void OnTriggerEnter(Collider other){
        // 탈출엔딩
        if(ending_flag == 0){
            SceneManager.LoadScene("Ending_Escape");
        }
        // 생존엔딩
        else if (ending_flag == 1){
            SceneManager.LoadScene("Ending_Survived");
        }
        // 게임오버
        else{
            SceneManager.LoadScene("GameOver");
        }
    }
}
