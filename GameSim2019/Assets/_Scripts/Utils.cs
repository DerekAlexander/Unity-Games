using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class Utils
{
    ///<summary>put true to lock mouse or false to unlock</summary>
    public static void CursorState( bool b )
    {
        if ( b == true )
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
    }



#if UNITY_EDITOR

    public static IEnumerator Rave ( )
    {
        float r = 0, g = 0, b = 0;
        string getColor = "default";
        string[] wholeString;

        // init
        getColor = EditorPrefs.GetString("Playmode tint");
        wholeString = getColor.Split(';');
        r = float.Parse(wholeString[1]);
        g = float.Parse(wholeString[2]);
        b = float.Parse(wholeString[3]);

            //UnityEditor.EditorApplication.Beep();
        
        while ( true )  
        {
            
            getColor = EditorPrefs.GetString("Playmode tint");
            wholeString = getColor.Split(';');

            r = ( r + 0.005f ) % 1;
            g = ( g + 0.01f ) % 1;
            b = ( b + 0.02f ) % 1;

            wholeString[1] = r.ToString();
            wholeString[2] = g.ToString();
            wholeString[3] = b.ToString();

            string theNewWholeString = string.Join(";", wholeString);

            EditorPrefs.SetString("Playmode tint", theNewWholeString);
            //UnityEditorInternal.InternalEditorUtility.unityPreferencesFolder.
            //EditorWindow.GetWindow();
            

            
            

            //Debug.Log(EditorWindow.mouseOverWindow);
            //UnityEditor.EditorApplication.
            //UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            //EditorPrefs.SetFloat("Playmode tint", color);
            yield return null;
        }
    }

#endif
}
