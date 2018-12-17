using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDestroy : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Npc")
        {
            Destroy(other.gameObject);
        }
    }
}
