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
            //print(other.name + "���� ����!");
            for (int i = 0; i < enemys.Length; i++) {
                enemys[i].GetComponent<enemycontroll>().player_hide = true;
                //print(other.name + "���� ����1!");
            }
        }
        

    }



    // Collider ������Ʈ�� is Trigger�� true�� ���·� �浹���� ��

    private void OnTriggerStay(Collider other)

    {
        if (other.tag == "Player") {
            //print(other.name + "���� ��!");
        }
        

    }



    // Collider ������Ʈ�� is Trigger�� true�� ���·� �浹�� ������ ��

    private void OnTriggerExit(Collider other)

    {
        if(other.tag == "Player") {
            //print(other.name + "���� ��!");
        }
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<enemycontroll>().player_hide = false;
        }


    }

}
