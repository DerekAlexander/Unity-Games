using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviour
{
    public Text displayText;
    //public GameObject currency;
    public int currencyDifficulty;
    public int currencyToWin = 300;
    public static CurrencyController instance;

    public int currencyTotal;

    public void Start()
    {
        instance = this;
        //currency = currency.GetComponent<GameObject>();
        currencyToWin = 300 * currencyDifficulty;
        currencyTotal = 100;
        UpdateInfo();
    }

    public void UpdateInfo()
    {

        //displayText = transform.Find("Text").GetComponent<Text>();

        displayText.text = currencyTotal.ToString();


    }
}
