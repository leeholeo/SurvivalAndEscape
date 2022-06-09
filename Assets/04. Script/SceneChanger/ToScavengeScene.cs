// scavenge map에서 파밍 지역으로 이동, scavenge map의 버튼에 부착하여 사용, 버튼은 항상 "SceneNameButton"의 형태여야 한다.

using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ToScavengeScene : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private IntoTheNight intoTheNight;
    private string gameObjectName;
    private string sceneName;

    void Start()
    {
        intoTheNight = FindObjectOfType<IntoTheNight>();
    }

    public void ToSceneByName()
    {
        gameObjectName = gameObject.name;
        sceneName = gameObjectName.Substring(0, gameObjectName.Length - "Button".Length);
        intoTheNight.ToSceneByName(sceneName);
    }

    public TextMeshProUGUI text;
    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        text.fontStyle = FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        text.fontStyle &= ~FontStyles.Underline;
    }
}
