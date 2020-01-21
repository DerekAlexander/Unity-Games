using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Egg : MonoBehaviour, DayNightEventInterface
{
    public GameObject blobie;
    public GameObject topOfEgg;
    public NavMeshObstacle obstacle;

    public char speed = 'c', glide = 'c', power = 'c';

    public int daysTillHatch = 1;
    public int secondsTillHatch = 75;

    private bool isInHatchingProcess = false;


    public AudioClip clip;
    AudioSource source;

    private void Awake ()
    {
        FindObjectOfType<DayNightCycle>().RegisterForDayNightEvents(this);
        FindObjectOfType<FarmManager>().RegisterEgg(this);
        obstacle = GetComponent<NavMeshObstacle>();
        source = GetComponent<AudioSource>();

        if ( daysTillHatch == 0 && !isInHatchingProcess )
        {
            StartCoroutine(WaitingTillHatchTime());
        }
    }


    public void InstatiateStats (char sp, char gl, char pw, int days, int seconds )
    {
        speed = sp;
        glide = gl;
        power = pw;
        daysTillHatch = days;
        secondsTillHatch = seconds;

        if ( daysTillHatch <= 0 && !isInHatchingProcess)
            StartCoroutine(WaitingTillHatchTime());
    }

    public void InstantiateletterGrades(char sp, char gl, char pw)
    {
        speed = sp;
        glide = gl;
        power = pw;
    }

    IEnumerator Hatch ( )
    {
        source.PlayOneShot(clip);
        obstacle.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(.25f);
        GameObject newBlobie = Instantiate(blobie, transform.position, transform.rotation) as GameObject;
        AIStatSheet stats = newBlobie.GetComponent<AIStatSheet>();
        stats.SetGlideGrade(glide);
        stats.SetPowerGrade(power);
        stats.SetSpeedGrade(speed);
        newBlobie.GetComponent<AIFarmBehavior>().state = AIFarmBehavior.State.IDLE;

        // TODO: replace this with a fading effect on the egg
        FindObjectOfType<FarmManager>().DeregisterEgg(this);
        StartCoroutine(CrackEgg());
    }



    IEnumerator CrackEgg ( )
    {
        topOfEgg.GetComponent<Rigidbody>().isKinematic = false;
        topOfEgg.GetComponent<Rigidbody>().AddForce(Vector3.up * 50);

        yield return new WaitForSeconds(.5f);

        Destroy(gameObject);
    }


    IEnumerator WaitingTillHatchTime ( )
    {
        isInHatchingProcess = true;
        while ( secondsTillHatch > 0)
        {
            secondsTillHatch--;
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(Hatch());
    }


    IEnumerator FadeEgg ( )
    {
        yield return null;
    }

    public void Morning ()
    {
        daysTillHatch--;

        if ( daysTillHatch <= 0 && !isInHatchingProcess )
            StartCoroutine(WaitingTillHatchTime());
    }

    public void Night ()
    {
        
    }
}
