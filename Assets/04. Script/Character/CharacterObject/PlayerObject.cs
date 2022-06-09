// Player Script에서 자동으로 생성되어 사용

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Player Object", menuName = "Character/Player")]
public class PlayerObject : CharacterObject
{
    // 자동으로 연결
    [Header("Automatical Link")]
    // 사망시 fadeout 및 동작 금지
    // public FadeOut fadeOut;
    public FirstPersonController firstPersonController;
    public bool isDead = new bool();
    [HideInInspector]
    public string STAMINA = "행동력", HUNGRY = "포만감", THIRSTY = "수분", MENTAL = "정신력", MOVE_SPEED = "이동속도";
    // 특성
    public string playerTrait;
    public const string NO_TRAIT = "NO_TRAIT", MEDIC = "MEDIC";
    // 스태미나
    public int maxStaminaPoint;
    public int currentStaminaPoint;
    // 배고픔
    public int maxHungryPoint;
    public int currentHungryPoint;
    public string[] currentHungryText = { "배부름", "허기짐", "굶주림" };
    // 목마름
    public int maxThirstyPoint;
    public int currentThirstyPoint;
    public string[] currentThirstyText = { "정상수분", "갈증", "탈수" };
    // 정신력
    public int maxMentalPoint;
    public int currentMentalPoint;
    public string[] currentMentalText = { "행복", "불안", "우울" };
    // 질병
    [HideInInspector]
    public int NO_DISEASE = 0, COLD = 1, INFECTION = 2, TETANUS = 3;
    public int initialDisease;
    public int disease;
    public string[] diseaseText = { "질병없음", "감기", "감염", "파상풍" };
    public int diseaseDayCount;
    public bool isCured;
    public int[] healCount = new int[] { 0, 3, 3, 3 };
    public int[] healCountMedic = new int[] { 0, 1, 1, 1 };
    // 저장 및 불러오기를 위한 토큰
    public PlayerToken playerToken;
    public ConditionController conditionController;

    public override void Awake()
    {
        savePath = string.Concat(Application.persistentDataPath, "/", this.name, ".save");
        // Debug.Log("Player Awake");
        type = CharacterType.Player;
        if (!Load())
        {
            playerToken = new PlayerToken();
            // 체력 및 이동속도
            base.Awake();
            // 특성에 따른 스테이터스 최대값 초기화
            maxStaminaPoint = 100;
            maxHungryPoint = 100;
            maxThirstyPoint = 100;
            maxMentalPoint = 100;
            initialDisease = NO_DISEASE;
            // player trait 설정
            playerTrait = "NO_TRAIT";
            switch (playerTrait)
            {
                case NO_TRAIT:
                    break;
                case MEDIC:
                    break;
            }
        }
        conditionController = GameObject.FindObjectOfType<ConditionController>();
    }

    public override void OnEnable()
    {
        // Debug.Log("Player Enable");
        // fadeOut = FindObjectOfType<FadeOut>();
        firstPersonController = FindObjectOfType<FirstPersonController>();
        if (!File.Exists(savePath))
        {
            // 체력 및 이동속도
            base.OnEnable();
            // 현재 스테이터스 초기화
            currentStaminaPoint = maxStaminaPoint;
            currentHungryPoint = maxHungryPoint;
            currentThirstyPoint = maxThirstyPoint;
            currentMentalPoint = maxMentalPoint;
            disease = initialDisease;
        }
    }

    public void OnDestroy()
    {
        // Debug.Log("PlayerObject Destroyed");
        // Debug.Log("conditionController.isSave");
        // Debug.Log(conditionController.isSave);
        if (conditionController.isSave)
            Save();
        else
            Delete();
    }

    public override void CharacterDead()
    {
        // Debug.Log("Player Dead");
        isDead = true;
        conditionController.isSave = false;
        // animation 처리
        base.CharacterDead();
        // 게임 종료 처리
        firstPersonController.CanMove = false;
        // fadeOut.PlayerDead();
        SceneManager.LoadScene("GameOver");
    }
    
    // // 스태미나 변경, 남은 스태미나보다 많이 소비하려 하면 false를 return
    // public bool ChangeStaminaPoint(int amount)
    // {
    //     // 최대치를 넘어 회복하려 하는 경우
    //     if (currentStaminaPoint + amount > maxStaminaPoint)
    //         currentStaminaPoint = maxStaminaPoint;
    //     // 남은 스태미나보다 많이 소비하려 하는 경우
    //     else if (currentStaminaPoint + amount <= 0)
    //         return false;
    //     else
    //         currentStaminaPoint += amount;

    //     return true;
    // }
    
    // 배고픔 변경, 0이 되면 false를 return
    public bool ChangeHungryPoint(int amount)
    {
        if (currentHungryPoint == 0) return false;
        // 최대치를 넘어 회복하려 하는 경우
        if (currentHungryPoint + amount > maxHungryPoint)
            currentHungryPoint = maxHungryPoint;
        // 0이 되어 사망하는 경우
        else if (currentHungryPoint + amount <= 0)
        {
            currentHungryPoint = 0;
            // Debug.Log("currentHungryPoint is 0");
            CharacterDead();
            return false;
        }
        else
            currentHungryPoint += amount;

        return true;
    }
    
    // 목마름 변경, 0이 되면 false를 return
    public bool ChangeThirstyPoint(int amount)
    {
        if (currentThirstyPoint == 0) return false;
        // 최대치를 넘어 회복하려 하는 경우
        if (currentThirstyPoint + amount > maxThirstyPoint)
            currentThirstyPoint = maxThirstyPoint;
        // 0이 되어 사망하는 경우
        else if (currentThirstyPoint + amount <= 0)
        {
            currentThirstyPoint = 0;
            // Debug.Log("currentThirstyPoint is 0");
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
        if (currentMentalPoint == 0) return false;
        // 최대치를 넘어 회복하려 하는 경우
        if (currentMentalPoint + amount > maxMentalPoint)
            currentMentalPoint = maxMentalPoint;
        // 0이 되어 사망하는 경우
        else if (currentMentalPoint + amount <= 0)
        {
            currentMentalPoint = 0;
            // Debug.Log("currentMentalPoint is 0");
            CharacterDead();
            return false;
        }
        else
            currentMentalPoint += amount;

        return true;
    }

    // 질병 변경, 실패하면 false를 return
    public bool ChangeDisease(int diseaseType)
    {
        if (disease == diseaseType)
        {
            return false;
        }

        disease = diseaseType;
        if (disease == NO_DISEASE)
        {
            moveSpeed = AVERAGE_SPEED;
        }
        else if (disease == COLD)
        {
            moveSpeed = SLOW;
            ChangeHealthPoint(-10);
            ChangeThirstyPoint(-10);
            ChangeMentalPoint(-10);
        }
        return true;
    }

    // 질병 회복 count, 질병에서 회복되면 true를 return
    public bool HealDisease(int amount)
    {
        return false;
    }

    // 치료제를 사용, 이미 사용했다면 false를 return
    public bool UseCure(int amount)
    {
        // if (isCured)
        //     return false;
        // else
        //     return true;
        if (disease == NO_DISEASE)
            return false;
        else
            ChangeDisease(NO_DISEASE);
            return true;
    }

    [ContextMenu("Save")]
    public void Save()
    {
        UpdateToken();
        IFormatter formatter = new BinaryFormatter();
        Stream stream =  new FileStream(savePath, FileMode.Create, FileAccess.Write);
        // Debug.Log("Saving Started");
        formatter.Serialize(stream, playerToken);
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
                playerToken = (PlayerToken)formatter.Deserialize(stream);
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
        // Character
        // Debug.Log("playerToken");
        // Debug.Log(playerToken);
        // Debug.Log("playerToken.description");
        // Debug.Log(playerToken.description);
        // Debug.Log("description");
        // Debug.Log(description);
        playerToken.description = description;
        playerToken.savePath = savePath;
        playerToken.maxHealthPoint = maxHealthPoint;
        playerToken.currentHealthPoint = currentHealthPoint;
        playerToken.initialMoveSpeed = initialMoveSpeed;
        playerToken.moveSpeed = moveSpeed;
        // Player
        playerToken.playerTrait = playerTrait;
        playerToken.maxStaminaPoint = maxStaminaPoint;
        playerToken.currentStaminaPoint = currentStaminaPoint;
        playerToken.maxHungryPoint = maxHungryPoint;
        playerToken.currentHungryPoint = currentHungryPoint;
        playerToken.maxThirstyPoint = maxThirstyPoint;
        playerToken.currentThirstyPoint = currentThirstyPoint;
        playerToken.maxMentalPoint = maxMentalPoint;
        playerToken.currentMentalPoint = currentMentalPoint;
        playerToken.initialDisease = initialDisease;
        playerToken.disease = disease;
        playerToken.diseaseDayCount = diseaseDayCount;
        playerToken.isCured = isCured;
        // Debug.Log("Token Updated");
    }

    public void DownloadToken()
    {
        // Character
        description = playerToken.description;
        savePath = playerToken.savePath;
        maxHealthPoint = playerToken.maxHealthPoint;
        currentHealthPoint = playerToken.currentHealthPoint;
        initialMoveSpeed = playerToken.initialMoveSpeed;
        moveSpeed = playerToken.moveSpeed;
        // Player
        playerTrait = playerToken.playerTrait;
        maxStaminaPoint = playerToken.maxStaminaPoint;
        currentStaminaPoint = playerToken.currentStaminaPoint;
        maxHungryPoint = playerToken.maxHungryPoint;
        currentHungryPoint = playerToken.currentHungryPoint;
        maxThirstyPoint = playerToken.maxThirstyPoint;
        currentThirstyPoint = playerToken.currentThirstyPoint;
        maxMentalPoint = playerToken.maxMentalPoint;
        currentMentalPoint = playerToken.currentMentalPoint;
        initialDisease = playerToken.initialDisease;
        disease = playerToken.disease;
        diseaseDayCount = playerToken.diseaseDayCount;
        isCured = playerToken.isCured;
        // Debug.Log("Token Downloaded");
    }
}

[System.Serializable]
public class PlayerToken: CharacterToken
{
    // 특성
    public string playerTrait;
    // 스태미나
    public int maxStaminaPoint;
    public int currentStaminaPoint;
    // 배고픔
    public int maxHungryPoint;
    public int currentHungryPoint;
    // 목마름
    public int maxThirstyPoint;
    public int currentThirstyPoint;
    // 정신력
    public int maxMentalPoint;
    public int currentMentalPoint;
    // 질병
    public int initialDisease;
    public int disease;
    public int diseaseDayCount;
    public bool isCured;
}
