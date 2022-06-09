using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public int survival_date = 1;
    public GameObject textObj;
    void Start()
    {
        string str = string.Format("당신은 \n{0}일간 생존했습니다", survival_date);
        textObj.GetComponent<Text>().text = str;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
