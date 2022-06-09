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
    Vector3 tmppos;//일정거리 벗어났을때 마지막에 봤던 위치 저장하기 위한 위치. 
    //누적시간
    float currentTime = 0;
    float currentTime2 = 0;
    //공격 딜레이 시간
    float attackDelay = 3f;
    
    public int attackpower = 30;
    float lastdistance;
    //CharacterController cc;
    //목적지
    public Transform target;
    public Transform origin;//원래 위치
    //요원
    NavMeshAgent agent;
    enum enemyState {idle, attack, chase, goback, punch}
    enemyState _enemystate = enemyState.idle;
    public Animator anim;
    public bool player_hide;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetBool("isWalk", true);
        //요원을 정의해줘서
        agent = GetComponent<NavMeshAgent>();
        //생성될때 목적지(Player)를 찿는다.
        //target = GameObject.Find("Player").transform;
        //요원에게 목적지를 알려준다.
        agent.destination = target.transform.position;
        //cc = GetComponent<CharacterController>() ;
        player_hide = false;
        lastdistance = 0;
        tmppos = transform.position;//임시 위치. 
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
            { //0.5 초 만큼 딜레이
                
                currentTime2 = 0;
                _enemystate = enemyState.attack;//1번만 실행후 다시 attack으로 바꾸어주어야 한다.

            }
        }
        distancecheck();
    }

    private void Updategoback()
    {
        //어그로 풀리고 idle
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
        //타겟 방향으로 이동하다가
        agent.speed = 1f;
        //요원에게 목적지를 알려준다.
        
        if (_enemystate == enemyState.idle && _enemystate == enemyState.attack) { //그대로 그자리.
            agent.destination = this.transform.position;
        }
        else if (_enemystate == enemyState.chase && lastdistance <= 10f) {
            agent.destination = target.transform.position; //10f 안이면 목표를 따라감
            tmppos = target.transform.position;
        } else if (_enemystate == enemyState.chase) {
            agent.destination = tmppos; //벗어나면 마지막 봤던 10떨어진 곳으로 따라감.
        } else if (_enemystate == enemyState.goback)  //원래자리로
        {
            agent.destination = origin.transform.position;
        }


    }

    void UpdateAttack() {
        //축 고정으로 바라보게.
        Vector3 targetPostition = new Vector3(target.position.x,
                                       this.transform.position.y,
                                       target.position.z);
        this.transform.LookAt(targetPostition);
        //플레이어가 공격범위 이내면 플레이어 공격.
        //일정 시간마다 플레이어를 공격
        //print("crr: " + currentTime + ", delay" + attackDelay);
        currentTime += Time.deltaTime;
        if (currentTime > attackDelay)
        { //attackDelay 초 만큼 딜레이

            _enemystate = enemyState.punch;//펀치 상태로 바꿈.(펀치 상태에서 HP 깎을 예정)
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
        {//punch에서 전환은 위에서 해야됨.
            _enemystate = enemyState.punch;
            //print(distance);
        }
        else if (distance <= 3f) {
            _enemystate = enemyState.attack;
            //print(distance);
        }
        else if (player_hide == false)
        {
            //시야
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
                print("적NPC의 시야에 들어왔습니다.");
            }

        }*/
        //print("status: "+ _enemystate+ ", distance:"+ distance+"tartget+ "+ target.transform.position + ", this:"+ transform.position);
        
    }

    void punchHP() {  //HP 감소. anim event로 호출.
        int powermount = attackpower * (-1);//HP값이 power만큼 깍혀야 하므로 (-)
        target.GetComponent<PlayerScript>().playerObject.ChangeHealthPoint(powermount);
    }
  
}
