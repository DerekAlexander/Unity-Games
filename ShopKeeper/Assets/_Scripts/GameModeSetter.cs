using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSetter : MonoBehaviour {

    public string mode;
    public string difficulty;
    public int currencyDifficulty;
    public static GameModeSetter instance;


    // Use this for initialization
    void Start ()
    {
        instance = this;
       
        mode = MenuManager.instance.Mode();
        difficulty = MenuManager.instance.Difficulty();

        switch (mode)
        {
            case "Endless":
                {
                    GameTimer.useTimer = false;
                }
                break;

            case "Normal":
                {
                    GameTimer.useTimer = true;
                }
                break;

        }

        switch (difficulty)
        {
            case "Easy":
                {
                    CurrencyController.instance.currencyDifficulty = 1;
                    currencyDifficulty = 1;
                }
                break;

            case "Medium":
                {
                    CurrencyController.instance.currencyDifficulty = 2;
                    currencyDifficulty = 2;
                }
                break;

            case "Hard":
                {
                    CurrencyController.instance.currencyDifficulty = 3;
                    currencyDifficulty = 3;
                }
                break;
        }

    }

    public int CurrencyDifficulty()
    {
        return currencyDifficulty;
    }

}
