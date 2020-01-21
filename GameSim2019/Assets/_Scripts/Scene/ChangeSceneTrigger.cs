using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{

    private SceneController controller;
    public enum MusicType { SAME, ISLAND, COMPETITION, RANCH};
    public MusicType musicToChangeTo = MusicType.SAME;

    public string sceneToLoad;
    public string triggerEnterText;

    private bool isPlayerHere = false;

    // Start is called before the first frame update
    void Start ( )
    {
        controller = FindObjectOfType<SceneController>();
    }

    // Update is called once per frame
    void Update ( )
    {
        if ( Input.GetKeyDown(KeyCode.E) && isPlayerHere )
        {
            if ( !controller)
                controller = FindObjectOfType<SceneController>();
            SwitchMusicPlayList();
            controller.LoadScene(sceneToLoad);
        }
    }


    private void SwitchMusicPlayList ( )
    {
        MusicController music = FindObjectOfType<MusicController>();
        if ( !music )
            return;

        switch ( musicToChangeTo )
        {
            case MusicType.COMPETITION:
                music.ChangeToCompetitionMusic();
                break;
            case MusicType.RANCH:
                music.ChangeToRanchMusic();
                break;
            case MusicType.ISLAND:
                music.ChangeToIslandMusic();
                break;
            default:
                // do nothing
                break;
        }
    }

    private void OnTriggerEnter ( Collider other )
    {
        if ( other.tag == "Player" )
        {
            FindObjectOfType<HelperText>().DisplayTest(triggerEnterText);
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
