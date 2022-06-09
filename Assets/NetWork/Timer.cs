using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timetext;
    public PlayerScript playerScript;
    public EndOfTheDay endOfTheDay;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimerTest());
        playerScript = GameObject.FindObjectOfType<PlayerScript>();
        endOfTheDay = GameObject.FindObjectOfType<EndOfTheDay>();
    }

    // Update is called once per frame
    void Update()
    {
        int timesplit = (int)timer;
   
            TimeSpan t = TimeSpan.FromSeconds(timesplit);

        timetext.text = string.Format("{0:D2}:{1:D2}",
                t.Minutes,
                t.Seconds);
    }

    #region timer
    public IEnumerator TimerTest()
    {
        while (timer > 0)
        {
            {
                timer -= Time.deltaTime;
                string minutes = Mathf.Floor(timer / 60).ToString("00");
                string second = (timer % 60).ToString("00");

                //Debug.Log(string.Format("{0}:{1}", minutes, second));
                yield return null;
            }
        }
        if (timer <= 0)
        {
            playerScript.playerObject.ChangeHealthPoint(-20);
            playerScript.playerObject.ChangeHungryPoint(-20);
            playerScript.playerObject.ChangeThirstyPoint(-20);
            playerScript.playerObject.ChangeMentalPoint(-20);
            endOfTheDay.CallEndOfTheDay();
        }
    }
    #endregion
}
