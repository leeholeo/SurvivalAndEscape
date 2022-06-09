using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// https://docs.unity3d.com/ScriptReference/Object.DontDestroyOnLoad.html#:~:text=DontDestroy.cs%20script%3A
public class CharacterStatus : MonoBehaviour
{
    [HideInInspector]
    public int GOOD = 0, BAD = 1, WORST = 2;
    [HideInInspector]
    public string HEALTH = "체력", STAMINA = "행동력", HUNGRY = "포만감", THIRSTY = "수분";
    // 체력
    private int maxHealthPoint;
    private int currentHealthPoint;
    private string[] currentHealthText = new string[] { "건강함", "부상", "빈사" };
    // 스태미나
    private int maxStaminaPoint;
    private int currentStaminaPoint;
    // 배고픔
    private int maxHungryPoint;
    private int currentHungryPoint;
    private string[] currentHungryText = new string[] { "배부름", "허기짐", "굶주림" };
    // 목마름
    private int maxThirstyPoint;
    private int currentThirstyPoint;
    private string[] currentThirstyText = new string[] { "정상수분", "갈증", "탈수" };
    // 정신력
    private int maxMentalPoint;
    private int currentMentalPoint;
    private string[] currentMentalText = new string[] { "행복", "불안", "우울" };
    // 질병
    private int initialDisease;
    public int currentDisease;
    public string[] diseaseText = new string[] { "질병 없음", "감기", "감염", "파상풍" };
    private int diseaseDayCount;
    private bool isCured;
    [HideInInspector]
    public int NO_DISEASE = 0, COLD = 1, INFECTION = 2, TETANUS = 3;
    // public string NO_DISEASE_TEXT = "정상", COLD_TEXT = "감기", INFECTION_TEXT = "감염", TETANUS_TEXT = "파상풍";
    private int[] healCount = new int[] { 0, 3, 3, 3 };
    private int[] healCountMedic = new int[] { 0, 1, 1, 1 };
    // 이동속도
    private int initialMoveSpeed;
    private int moveSpeed;
    private string[] moveSpeedText = new string[] { "빠름", "보통속도", "느림" };
    [HideInInspector]
    public int FAST = 10, AVERAGE_SPEED = 8, SLOW = 5;
    
    private void Awake()
    {
        gameObject.tag = "CharacterStatus";
        GameObject[] characterStatus = GameObject.FindGameObjectsWithTag("CharacterStatus");
        if (characterStatus.Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private FadeOut fadeOut;
    private MouseLook mouseLook;
    private PlayerMovement playerMovement;

    void Start()
    {
        // FadeOut script 연결
        fadeOut = FindObjectOfType<FadeOut>();
        // 특성에 따른 스테이터스 최대값 초기화
        maxHealthPoint = 100;
        maxStaminaPoint = 100;
        maxHungryPoint = 100;
        maxThirstyPoint = 100;
        maxMentalPoint = 100;
        initialDisease = 0;
        initialMoveSpeed = AVERAGE_SPEED;
        // 현재 스테이터스 초기화
        currentHealthPoint = maxHealthPoint;
        currentStaminaPoint = maxStaminaPoint;
        currentHungryPoint = maxHungryPoint;
        currentThirstyPoint = maxThirstyPoint;
        currentMentalPoint = maxMentalPoint;
        currentDisease = initialDisease;
        moveSpeed = initialMoveSpeed;
        Debug.Log(currentHealthPoint);
        Debug.Log(currentStaminaPoint);
        Debug.Log(currentHungryPoint);
        Debug.Log(currentThirstyPoint);
        Debug.Log(currentMentalPoint);
        Debug.Log(currentDisease);
        Debug.Log(moveSpeed);
        mouseLook = FindObjectOfType<MouseLook>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    // 사망 처리
    private void CharacterDead()
    {
        Debug.Log("Character dead");
        Destroy(mouseLook.GetComponent<MouseLook>());
        Destroy(playerMovement.GetComponent<PlayerMovement>());
        StartCoroutine(DoDead());
    }

    IEnumerator DoDead ()
    {
        yield return StartCoroutine(fadeOut.DoFade());
        Debug.Log("Scene change done");
        SceneManager.LoadScene("TestScene");
        Debug.Log("timescale changed");
        Debug.Log("Destroy gameObject");
        Destroy(gameObject);
    }

    // 체력 변경, 0이 되면 false를 return
    public bool ChangeHealthPoint(int amount)
    {
        Debug.Log($"{currentHealthPoint} + {amount}");
        // 최대치를 넘어 회복하려 하는 경우
        if (currentHealthPoint + amount > maxHealthPoint)
            currentHealthPoint = maxHealthPoint;
        // 0이 되어 사망하는 경우
        else if (currentHealthPoint + amount <= 0)
        {
            currentHealthPoint = 0;
            Debug.Log("currentHealthPoint is 0");
            CharacterDead();
            return false;
        }
        else
            currentHealthPoint += amount;

        Debug.Log($"= {currentHealthPoint}");
        return true;
    }
    
    // 스태미나 변경, 남은 스태미나보다 많이 소비하려 하면 false를 return
    public bool ChangeStaminaPoint(int amount)
    {
        // 최대치를 넘어 회복하려 하는 경우
        if (currentStaminaPoint + amount > maxStaminaPoint)
            currentStaminaPoint = maxStaminaPoint;
        // 남은 스태미나보다 많이 소비하려 하는 경우
        else if (currentStaminaPoint + amount <= 0)
            return false;
        else
            currentStaminaPoint += amount;

        return true;
    }
    
    // 배고픔 변경, 0이 되면 false를 return
    public bool ChangeHungryPoint(int amount)
    {
        // 최대치를 넘어 회복하려 하는 경우
        if (currentHungryPoint + amount > maxHungryPoint)
            currentHungryPoint = maxHungryPoint;
        // 0이 되어 사망하는 경우
        else if (currentHungryPoint + amount <= 0)
        {
            currentHungryPoint = 0;
            return false;
        }
        else
            currentHungryPoint += amount;

        return true;
    }
    
    // 목마름 변경, 0이 되면 false를 return
    public bool ChangeThirstyPoint(int amount)
    {
        // 최대치를 넘어 회복하려 하는 경우
        if (currentThirstyPoint + amount > maxThirstyPoint)
            currentThirstyPoint = maxThirstyPoint;
        // 0이 되어 사망하는 경우
        else if (currentThirstyPoint + amount <= 0)
        {
            currentThirstyPoint = 0;
            Debug.Log("currentThirstyPoint is 0");
            CharacterDead();
            return false;
        }
        else
            currentThirstyPoint += amount;

        return true;
    }
    
    // 정신력 변경, 0이 되면 false를 return
    public bool ChangeMentalPoint(int amount)
    {
        // 최대치를 넘어 회복하려 하는 경우
        if (currentMentalPoint + amount > maxMentalPoint)
            currentMentalPoint = maxMentalPoint;
        // 0이 되어 사망하는 경우
        else if (currentMentalPoint + amount <= 0)
        {
            currentMentalPoint = 0;
            Debug.Log("currentMentalPoint is 0");
            CharacterDead();
            return false;
        }
        else
            currentMentalPoint += amount;

        return true;
    }

    // 질병 변경, 실패하면 false를 return
    public bool ChangeDisease(int amount)
    {
        return false;
    }

    // 질병 회복 count, 질병에서 회복되면 true를 return
    public bool HealDisease(int amount)
    {
        return false;
    }

    // 이동속도 변경
    public void ChangeMoveSpeed(int amount)
    {
        
    }
}
