using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidableobj : MonoBehaviour
{
    GameObject[] enemys;
    // Start is called before the first frame update
    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)

    {
        if (other.tag == "Player") {
            //print(other.name + "감지 시작!");
            for (int i = 0; i < enemys.Length; i++) {
                enemys[i].GetComponent<enemycontroll>().player_hide = true;
                //print(other.name + "감지 시작1!");
            }
        }
        

    }



    // Collider 컴포넌트의 is Trigger가 true인 상태로 충돌중일 때

    private void OnTriggerStay(Collider other)

    {
        if (other.tag == "Player") {
            //print(other.name + "감지 중!");
        }
        

    }



    // Collider 컴포넌트의 is Trigger가 true인 상태로 충돌이 끝났을 때

    private void OnTriggerExit(Collider other)

    {
        if(other.tag == "Player") {
            //print(other.name + "감지 끝!");
        }
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<enemycontroll>().player_hide = false;
        }


    }

}
