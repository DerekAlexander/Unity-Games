using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {

    public static GameTimer instance;
    public float timer;
    public static bool useTimer;
    private int timerToDisplay;

	// Use this for initialization
	void Start ()
    {
        instance = this;
	}

	// Update is called once per frame
	void Update ()
    {
        if(GetComponent<Text>() )
        {
            InGameTimer();
        }

    }

    private void InGameTimer()
    {
        if (useTimer == true)
        {
            timerToDisplay = (int)timer;
            GetComponent<Text>().text = "timer: " + timerToDisplay.ToString();

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (CurrencyController.instance.currencyTotal >= CurrencyController.instance.currencyToWin)
                {
                    SceneManager.LoadScene("LoseScene");
                    //present you lose ui with button to go to start screen and button to exit.
                }
                else
                {
                    SceneManager.LoadScene("WinScene");
                    //present you win ui with same buttons.
                }
            }
        }
        else
        {
            timerToDisplay = 000;
            GetComponent<Text>().text = "timer: ∞"; 

        }

    }

}
