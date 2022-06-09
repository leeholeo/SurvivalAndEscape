using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverDay : MonoBehaviour
{
    // 수동으로 연결
    [Header("Manual Link")]
    public TextMeshProUGUI textMeshProUGUI;
    // 자동으로 연결
    [Header("Automatical Link")]
    public ConditionController conditionController;

    // Start is called before the first frame update
    void Start()
    {
        conditionController = GameObject.FindObjectOfType<ConditionController>();
        textMeshProUGUI.text = $"당신은\n{conditionController.day}일간 생존했습니다.";
    }

    
}
