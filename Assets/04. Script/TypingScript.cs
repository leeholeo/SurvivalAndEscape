using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class TypingScript : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI tx;
    private string text = "전쟁이 일어났다.\n" +
        "포격이 쏟아지는 도심 속에서 나는 가까스로 탈출했다.\n" +
        "정신없이 도망치던 중 버려진 집을 발견했다.\n" +
        "내가 얼마나 더 버틸 수 있을지 모르겠다.\n" +
        "살아남기 위해서는 구조대를 기다리거나,\n"+
        "차량을 수리해 이 도시에서 벗어나야 한다.";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeTextToFullAlpha());
        //title.SetActive(false);
        
        // tx.SetActive(true);
        StartCoroutine(_typing()); 
    }
    public IEnumerator FadeTextToFullAlpha() // ���İ� 0���� 1�� ��ȯ
    {
        title.color = new Color(title.color.r, title.color.g, title.color.b, 0);
        while (title.color.a < 1.0f)
        {
            title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a + (Time.deltaTime / 2.0f));
            yield return null;
        }
        StartCoroutine(FadeTextToZero());
    }

    public IEnumerator FadeTextToZero()  // ���İ� 1���� 0���� ��ȯ
    {
        title.color = new Color(title.color.r, title.color.g, title.color.b, 1);
        while (title.color.a > 0.0f)
        {
            title.color = new Color(title.color.r, title.color.g, title.color.b, title.color.a - (Time.deltaTime / 2.0f));
            yield return null;
        }
        //StartCoroutine(FadeTextToFullAlpha());
    }
    IEnumerator _typing()
    {
        yield return new WaitForSeconds(5f);
        for(int i = 0; i <= text.Length; i++)
        {
            tx.text = text.Substring(0, i);

            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(5f);
        
        SceneManager.LoadScene("Hideout");
    }
}
