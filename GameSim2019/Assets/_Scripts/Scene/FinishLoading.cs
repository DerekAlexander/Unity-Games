using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLoading : MonoBehaviour
{
    SceneController sceneController;
    // Start is called before the first frame update
    void Start ( )
    {
        sceneController = FindObjectOfType<SceneController>();
        Utils.CursorState(false);
    }

    // Update is called once per frame
    void Update ( )
    {

    }


    public void ButtonResponse ( )
    {
        sceneController.LetLoad();
    }
}
