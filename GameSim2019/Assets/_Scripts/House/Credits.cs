using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{

    public Image fade;
    public GameObject Text;
    public GameObject Button;
    public GameObject pauseScren;

    public string lookAtCreditsText = "Press E to look at credits";

    public Vector3 startingPos;
    public Vector3 endingPos;
    
    public float scrollSpeed = 1;

    bool isPlayerHere = false;
    bool showingCredits = false;

    // Start is called before the first frame update
    void Start()
    {
        fade.raycastTarget = false;
        startingPos.x = Text.transform.position.x;
        endingPos.x = Text.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown(KeyCode.E) && isPlayerHere && !showingCredits && !pauseScren.activeSelf )
        {
            StartCoroutine(RollCredits());
        }
    }
    
    IEnumerator RollCredits ( )
    {
        FindObjectOfType<KeyBindings>().InputState(false);
        FindObjectOfType<MovementHouse>().InputState(false);
        Text.transform.position = startingPos;
        showingCredits = true;
        fade.raycastTarget = true;
        Utils.CursorState(false);

        while ( fade.color.a < 1 ) // fade to black
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a + .02f);
            yield return null;
        }

        Button.SetActive(true); // turn end button on



        while ( Vector3.Distance(Text.transform.position, endingPos) > 1) // scoll text up
        {
            Text.transform.position = new Vector3(Text.transform.position.x,
                                                  Text.transform.position.y + ( scrollSpeed * Time.deltaTime ),
                                                  Text.transform.position.z);
            yield return null;
        }

        Button.SetActive(false);

        StartCoroutine(FadeToTransparency());
    }
    

    public void ButtonResponse ( )
    {
        StopAllCoroutines();
        Text.transform.position = startingPos;
        StartCoroutine(FadeToTransparency());
        Button.SetActive(false);
        Utils.CursorState(true);
    }


    IEnumerator FadeToTransparency ( )
    {
        while ( fade.color.a > 0 )
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a - .02f);
            yield return null;
        }

        showingCredits = false;
        fade.raycastTarget = false;
        Utils.CursorState(true);
        FindObjectOfType<KeyBindings>().InputState(true);
        FindObjectOfType<MovementHouse>().InputState(true);
    }


    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            FindObjectOfType<HelperText>().DisplayTest(lookAtCreditsText);
            isPlayerHere = true;
        }
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            FindObjectOfType<HelperText>().DisableText();
            isPlayerHere = false;
        }
    }

}
