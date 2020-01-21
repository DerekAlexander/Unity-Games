using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Sleep : MonoBehaviour
{
    public Image fade;
    public string sleepText = "Press E to sleep";
    public string[] cantSleepText;
    public string[] justWokeUpText = {"\"Morning already?\"" };

    FarmData data;

    Vector3 morningRotation = new Vector3(351.82f, 329.9994f, -4.097109e-05f); // right before sun rise

    bool canSleep = true;
    bool isPlayerHere = false;


    // Start is called before the first frame update
    void Start ( )
    {
        data = FarmSaving.LoadFarm();

        canSleep = data.dayNight.isNight;

    }

    // Update is called once per frame
    void Update ( )
    {
        if ( canSleep && isPlayerHere && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(FadeToBlack());
        }
        else if ( !canSleep && isPlayerHere && Input.GetKeyDown(KeyCode.E) )
        {
            
            FindObjectOfType<HelperText>().DisplayTest(cantSleepText[Random.Range(0, cantSleepText.Length)]);
        }


    }

    IEnumerator FadeToBlack ( )
    {
        while (fade.color.a < 1)
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a + .02f);
            yield return null;
        }
        yield return new WaitForSeconds(.25f);
        ProcessSleep();

        while ( fade.color.a > 0 )
        {
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a - .02f);
            yield return null;
        }
    }


    private void ProcessSleep ()
    {
        data.dayNight.sunRotation = new float[] { morningRotation.x, morningRotation.y, morningRotation.z };
        canSleep = false;
        FindObjectOfType<HelperText>().DisplayTest(justWokeUpText[Random.Range(0, justWokeUpText.Length)]);

        FarmSaving.SaveFarm(data);
    }



    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            FindObjectOfType<HelperText>().DisplayTest(sleepText);
            isPlayerHere = true;
        }
    }

    private void OnTriggerExit ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            isPlayerHere = false;
            FindObjectOfType<HelperText>().DisableText();
        }
    }


}
