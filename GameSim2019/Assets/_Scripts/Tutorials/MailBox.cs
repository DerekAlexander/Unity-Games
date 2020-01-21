using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : MonoBehaviour
{
    [Tooltip("List of tutorials that are in the mailbox. When one is ready to be seen, the mail icon appears")]
    public List<Tutorial> tuts = new List<Tutorial>();

    public GameObject creeper;

    // Start is called before the first frame update
    void Start ( )
    {

    }

    // Update is called once per frame
    void Update ( )
    {
        creeper.SetActive(CheckTuts());
    }



    private bool CheckTuts ( )
    {
        for ( int i = 0; i < tuts.Count; i++ )
        {
            if ( tuts[i].gameObject.activeSelf )
                if ( tuts[i].readyToBeSeen )
                {
                    return true;
                }
        }
        return false;
    }
}
