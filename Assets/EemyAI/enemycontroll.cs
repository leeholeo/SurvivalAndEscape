using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemycontroll : MonoBehaviour
{
    RaycastHit Hit;
    public float attacDistance = 2;
    Vector3 tmppos;//�����Ÿ� ������� �������� �ô� ��ġ �����ϱ� ���� ��ġ. 
    //�����ð�
    float currentTime = 0;
    float currentTime2 = 0;
    //���� ������ �ð�
    float attackDelay = 3f;
    
    public int attackpower = 30;
    float lastdistance;
    //CharacterController cc;
    //������
    public Transform target;
    public Transform origin;//���� ��ġ
    //���
    NavMeshAgent agent;
    enum enemyState {idle, attack, chase, goback, punch}
    enemyState _enemystate = enemyState.idle;
    public Animator anim;
    public bool player_hide;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isWalk", true);
        //����� �������༭
        agent = GetComponent<NavMeshAgent>();
        //�����ɶ� ������(Player)�� �O�´�.
        //target = GameObject.Find("Player").transform;
        //������� �������� �˷��ش�.
        agent.destination = target.transform.position;
        //cc = GetComponent<CharacterController>() ;
        player_hide = false;
        lastdistance = 0;
        tmppos = transform.position;//�ӽ� ��ġ. 
    }

    // Update is called once per frame
    void Update()
    {
       
        chaseenemy();
        //
        if (_enemystate == enemyState.chase) {
            anim.SetBool("isWalk", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isBattle", false);
            anim.SetBool("isPunch", false);
            //UpdateRun();
        }
        if (_enemystate == enemyState.goback)
        {
            anim.SetBool("isWalk", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isBattle", false);
            anim.SetBool("isPunch", false);
            Updategoback();
        }
        if (_enemystate == enemyState.attack)
        {
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isBattle", true);
            anim.SetBool("isPunch", false);
            UpdateAttack();
        }
        if (_enemystate == enemyState.idle)
        {
            
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isBattle", false);
            anim.SetBool("isPunch", false);
            //UpdateIdle();
        }
        if (_enemystate == enemyState.punch)
        {
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isBattle", false);
            anim.SetBool("isPunch", true);
            //UpdateIdle();
            currentTime2 += Time.deltaTime;
            if (currentTime2 > 3f)
            { //0.5 �� ��ŭ ������
                
                currentTime2 = 0;
                _enemystate = enemyState.attack;//1���� ������ �ٽ� attack���� �ٲپ��־�� �Ѵ�.

            }
        }
        distancecheck();
    }

    private void Updategoback()
    {
        //��׷� Ǯ���� idle
        if (_enemystate == enemyState.goback) {
            currentTime += Time.deltaTime;
            if (currentTime > 10f)
            {
                _enemystate = enemyState.idle;
                currentTime = 0;
            }
        }
    }

    void UpdateIdle()
    {
        
    }

    void chaseenemy() {
        //Ÿ�� �������� �̵��ϴٰ�
        agent.speed = 1f;
        //������� �������� �˷��ش�.
        
        if (_enemystate == enemyState.idle && _enemystate == enemyState.attack) { //�״�� ���ڸ�.
            agent.destination = this.transform.position;
        }
        else if (_enemystate == enemyState.chase && lastdistance <= 10f) {
            agent.destination = target.transform.position; //10f ���̸� ��ǥ�� ����
            tmppos = target.transform.position;
        } else if (_enemystate == enemyState.chase) {
            agent.destination = tmppos; //����� ������ �ô� 10������ ������ ����.
        } else if (_enemystate == enemyState.goback)  //�����ڸ���
        {
            agent.destination = origin.transform.position;
        }


    }

    void UpdateAttack() {
        //�� �������� �ٶ󺸰�.
        Vector3 targetPostition = new Vector3(target.position.x,
                                       this.transform.position.y,
                                       target.position.z);
        this.transform.LookAt(targetPostition);
        //�÷��̾ ���ݹ��� �̳��� �÷��̾� ����.
        //���� �ð����� �÷��̾ ����
        //print("crr: " + currentTime + ", delay" + attackDelay);
        currentTime += Time.deltaTime;
        if (currentTime > attackDelay)
        { //attackDelay �� ��ŭ ������

            _enemystate = enemyState.punch;//��ġ ���·� �ٲ�.(��ġ ���¿��� HP ���� ����)
            currentTime = 0;

        }
    }
    void UpdateRun()
    {
        //agent.isStopped = true;
        //agent.speed = 0f;
        //agent.acceleration = 0f;
    }
    void distancecheck(){
        float distance =  Vector3.Distance(target.transform.position, transform.position);
        
        lastdistance = distance;
        if (distance >= 100f)
        {
            _enemystate = enemyState.idle;
        }
        else if (distance <=3f && _enemystate == enemyState.punch)
        {//punch���� ��ȯ�� ������ �ؾߵ�.
            _enemystate = enemyState.punch;
            //print(distance);
        }
        else if (distance <= 3f) {
            _enemystate = enemyState.attack;
            //print(distance);
        }
        else if (player_hide == false)
        {
            //�þ�
            _enemystate = enemyState.chase;
        }
        else if (player_hide == true)
        {
            //
            _enemystate = enemyState.goback;
        }
        //print("distance: " + distance);
        //print("state: " + _enemystate);
        //test code 
        /*if (Physics.Raycast(transform.position, transform.forward * 20f, out Hit)) {
            //Hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;//
            if (Hit.transform.name == target.transform.name) {
                print("��NPC�� �þ߿� ���Խ��ϴ�.");
            }

        }*/
        //print("status: "+ _enemystate+ ", distance:"+ distance+"tartget+ "+ target.transform.position + ", this:"+ transform.position);
        
    }

    void punchHP() {  //HP ����. anim event�� ȣ��.
        int powermount = attackpower * (-1);//HP���� power��ŭ ������ �ϹǷ� (-)
        target.GetComponent<PlayerScript>().playerObject.ChangeHealthPoint(powermount);
    }
  
}
