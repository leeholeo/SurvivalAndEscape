// Player Script에서 자동으로 생성되어 사용, AmendPanel object를 연결, AmendPanel 하위의 AmendButton을 찾아 연결

using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Radio Object", menuName = "Furniture/Radio")]
public class RadioObject : FurnitureObject
{
    // 자동으로 연결
    [Header("Automatical Link")]
    public TextMeshProUGUI radioText;

    public override void Enable()
    {
        base.Enable();
        type = FurnitureType.Radio;
        savePath = string.Concat(Application.persistentDataPath, "/", this.name, ".save");
        if (!Load())
        {
            isAmended = false;
            furnitureToken = new FurnitureToken();
        }
    }

    // private void Update() {
    //     if (amendObject.isAmended)
    //     {
    //         Amend();
    //     }
    // }

    public override void Amend()
    {
        base.Amend();
        endOfTheDay.MENTAL_DECREASE += 10;
        // Debug.Log("RadioObject Amend");
    }

    private string[] radioTextByDay = new string[] {
        "죽음이 도처에 깔려 있습니다. 총과 포탄뿐만이 아닌 굶주림, 탈수, 우울감으로 생을 마감하는 사람들이 늘고 있습니다. 전쟁의 끝은 아직 보이지 않습니다.",
        "오늘 아침 시장에 포격이 일어나 많은 피해가 일어났습니다. 80명 이상이 사망한 것으로 추정됩니다.",
        "전쟁으로는 충분하지 않다는 듯이, 범죄와 폭력이 증가하고 있습니다. 무장 집단과 강도 집단이 생겨나고 있다는 소식이 있습니다. 외부 활동에 주의하십시오.",
        "기근과 질병이 만연하고 있습니다. 식량은 갈수록 구하기 힘들어질 것이며, 의료 물품은 귀중품으로 취급되고 있습니다.",
        "약탈자들이 매일 밤 사람들을 공격하며 난동을 부리고 있습니다. 머무르고 있는 곳의 방비를 강화하시기를 권고드립니다. 각별히 주의하십시오.",
        "도시 외각에 포격이 있을 것이라는 첩보가 들어왔습니다. 가능하면 지하에 머무르시고 외부 활동을 피하십시오."
    };
    // private string[] radioTextByDay = new string[] {
    //     "Death takes its toll. Sniper fire, mortar shell and cold temperatures are the cause of deaths of many civilians. The spokesman for the military said: \"Civilian casualties are the result of tragic accidents and criminal activity within the city\".",
    //     "We interrupt our broadcast to inform you of a tragic incident. This morning a mortar shell exploded in the marketplace, killing over sixty people and wounding many more.",
    //     "As if war wasn't enough, crime is on the rise in the city. There are reports of armed assault and robberies. Be advised to remain at home and lock your doors. Armed bands have taken to the streets.",
    //     "Famine and disease reign over the ravaged city of Pogoren. Death takes heavy toll due with no access to food, clean water and medical supplies.",
    //     "Bands of looters are on the rampage, attacking homes every night. Extreme vigilance is advised",
    //     "Observers warn that we must be prepared for increased shelling tonight. Do not venture outside and stay in the basement if possible."
    // };

    public override void Use()
    {
        base.Use();
    }

    public void FillRadioText()
    {
        radioText = GameObject.Find("RadioText").GetComponent<TextMeshProUGUI>();
        conditionController = FindObjectOfType<ConditionController>();
        // Debug.Log(conditionController.day);
        // Debug.Log(radioTextByDay);
        // Debug.Log(radioText);
        // Debug.Log(radioText.text);
        radioText.text = radioTextByDay[conditionController.day];
    }

    public void FillBlankRadioText()
    {
        radioText.text = "";
    }

    // public override void Debugging()
    // {
    //     Debug.Log($"RadioObject Debugging");
    //     base.Debugging();
    // }
}