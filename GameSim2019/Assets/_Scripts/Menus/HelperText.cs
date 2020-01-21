using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelperText : MonoBehaviour
{

    public Text text;
    public Shadow textShadow;
    public Image background;
    public GameObject shop;

    // Start is called before the first frame update
    void Start ( )
    {

        text = GetComponent<Text>();
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        textShadow = GetComponent<Shadow>();
        textShadow.effectColor = new Color(textShadow.effectColor.r, textShadow.effectColor.g, textShadow.effectColor.b, 0);
        background.color = new Color(background.color.r, background.color.g, background.color.b, text.color.a);

        text.text = "";
    }

    private void Update ()
    {

    }



    public void DisplayTest ( string text )
    {
        this.text.text = text;
        
        StopAllCoroutines();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn ( )
    {
        while ( text.color.a < 1 )
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + .05f);
            background.color = new Color(background.color.r, background.color.g, background.color.b, text.color.a);
            textShadow.effectColor = new Color(textShadow.effectColor.r, textShadow.effectColor.g, textShadow.effectColor.b, textShadow.effectColor.a + .05f);
            yield return null;
        }
    }

    public void DisableText ( )
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut ()
    {
        while ( text.color.a > 0 )
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - .05f);
            background.color = new Color(background.color.r, background.color.g, background.color.b, text.color.a);
            textShadow.effectColor = new Color(textShadow.effectColor.r, textShadow.effectColor.g, textShadow.effectColor.b, textShadow.effectColor.a - .05f);
            yield return null;
        }
    }

}
