// character들의 최상위 클래스

using UnityEngine;


public enum CharacterType
{
    Player,
    Bandit,
    Neutral,
}

public abstract class CharacterObject : ScriptableObject
{
    // 자동으로 연결
    [Header("Automatical Link")]
    public CharacterType type;
    [TextArea(15,20)]
    public string description = "Player";
    public string savePath;

    [HideInInspector]
    public string HEALTH = "체력";
    [HideInInspector]
    public const int GOOD = 0, BAD = 1, WORST = 2;
	// 체력
    public int maxHealthPoint;
    public int currentHealthPoint;
    public string[] currentHealthText = { "건강함", "부상", "빈사" };
    // 이동속도
    public int initialMoveSpeed;
    public int moveSpeed;
    public string[] moveSpeedText = { "빠름", "보통속도", "느림" };
    [HideInInspector]
    public const int FAST = 10, AVERAGE_SPEED = 8, SLOW = 5;

    public virtual void Awake()
    {
        // 특성에 따른 스테이터스 최대값 초기화
        maxHealthPoint = 100;
        initialMoveSpeed = AVERAGE_SPEED;
    }

    public virtual void OnEnable()
    {
        // 현재 스테이터스 초기화
        currentHealthPoint = maxHealthPoint;
        moveSpeed = initialMoveSpeed;
    }

    public virtual void CharacterDead()
    {
        // Debug.Log("Character dead");
        // animation 처리
    }

    // 체력 변경, 0이 되면 false를 return
    public bool ChangeHealthPoint(int amount)
    {
        if (currentHealthPoint == 0) return false;
        // Debug.Log($"{currentHealthPoint} + {amount}");
        // 최대치를 넘어 회복하려 하는 경우
        if (currentHealthPoint + amount > maxHealthPoint)
            currentHealthPoint = maxHealthPoint;
        // 0이 되어 사망하는 경우
        else if (currentHealthPoint + amount <= 0)
        {
            currentHealthPoint = 0;
            // Debug.Log("currentHealthPoint is 0");
            CharacterDead();
            return false;
        }
        else
            currentHealthPoint += amount;

        // Debug.Log($"= {currentHealthPoint}");
        return true;
    }

    // 이동속도 변경
    public void ChangeMoveSpeed(int amount)
    {
        // Debug.Log($"moveSpeed {moveSpeed} to {amount}");
        moveSpeed = amount;
    }
}

[System.Serializable]
public abstract class CharacterToken
{
    public string description;
    public string savePath;
    // 체력
    public int maxHealthPoint;
    public int currentHealthPoint;
    // 이동속도
    public int initialMoveSpeed;
    public int moveSpeed;
}