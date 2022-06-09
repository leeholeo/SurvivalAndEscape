using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class userUI : MonoBehaviour
{
    public Scrollbar healthPoint_scroll;
    public Scrollbar staminaPoint_scroll;
    public Scrollbar hungryPoint_scroll;
    public Scrollbar mentalPoint_scroll;
    public Scrollbar thirstyPoint_scroll;
    // Start is called before the first frame update
    float healthPoint;
    float hungryPoint;
    float mentalPoint;
    float staminaPoint;
    float thirstyPoint;
    void Start()
    {
        healthPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentHealthPoint;
        hungryPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentHungryPoint;
        mentalPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentMentalPoint;
        staminaPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentStaminaPoint;
        thirstyPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentThirstyPoint;


    }

    // Update is called once per frame
    void Update()
    {
        //HP
        healthPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentHealthPoint;
        healthPoint_scroll.size = healthPoint / 100;
        if (healthPoint == 0) { //값 접근 범위때문에 차례로 넣어야 됨;;
            ColorBlock cb = healthPoint_scroll.colors;
            Color cc = cb.disabledColor;
            cc.a = 0f;
            cb.disabledColor = cc;
            healthPoint_scroll.colors = cb;
        }
        //스태미나
        staminaPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentStaminaPoint;
        staminaPoint_scroll.size = staminaPoint / 100;
        if (staminaPoint == 0)
        {
            ColorBlock cb = staminaPoint_scroll.colors;
            Color cc = cb.disabledColor;
            cc.a = 0f;
            cb.disabledColor = cc;
            staminaPoint_scroll.colors = cb;
        }
        //Mental
        mentalPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentMentalPoint;
        mentalPoint_scroll.size = mentalPoint / 100;
        if (mentalPoint == 0)
        {
            ColorBlock cb = staminaPoint_scroll.colors;
            Color cc = cb.disabledColor;
            cc.a = 0f;
            cb.disabledColor = cc;
            mentalPoint_scroll.colors = cb;
        }
        //hungry
        hungryPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentHungryPoint;
        hungryPoint_scroll.size = hungryPoint / 100;
        if (hungryPoint == 0)
        {
            ColorBlock cb = hungryPoint_scroll.colors;
            Color cc = cb.disabledColor;
            cc.a = 0f;
            cb.disabledColor = cc;
            hungryPoint_scroll.colors = cb;
        }
        //thirsty
        thirstyPoint = this.transform.GetComponent<PlayerScript>().playerObject.currentThirstyPoint;
        thirstyPoint_scroll.size = thirstyPoint / 100;
        if (thirstyPoint == 0)
        {
            ColorBlock cb = thirstyPoint_scroll.colors;
            Color cc = cb.disabledColor;
            cc.a = 0f;
            cb.disabledColor = cc;
            thirstyPoint_scroll.colors = cb;
        }
        
    }
}