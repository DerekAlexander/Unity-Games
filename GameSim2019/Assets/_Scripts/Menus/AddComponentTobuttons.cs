using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddComponentTobuttons : MonoBehaviour
{
    public Button[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        buttons = Resources.FindObjectsOfTypeAll<Button>();

        for ( int i = 0; i < buttons.Length; i++ )
        {
            buttons[i].gameObject.AddComponent<ChangeSizeOnHover>();
        }
    }

}
