using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyToWinText : MonoBehaviour {

    private int currencyToWin;
    private void Update()
    {
        currencyToWin = 300 * GameModeSetter.instance.CurrencyDifficulty();

        GetComponent<Text>().text = currencyToWin.ToString();
    }
}
