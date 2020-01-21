#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class MenuItems 
{
    [MenuItem("Tools/Delete SaveFile")]
    private static void NewMenuOption()
    {
        InventorySaving.DeleteSaveFile();
        FarmSaving.DeleteSaveFile();
    }





    [MenuItem("Tools/Snap Grass")]
    private static void SnapGrassToGround ( )
    {
        GameObject[] grass = GameObject.FindGameObjectsWithTag("Grass");

        for ( int i = 0; i < grass.Length; i++ )
        {
            RaycastHit hit;

            if ( Physics.Raycast(grass[i].transform.position, grass[i].transform.forward * -1, out hit))
            {
                grass[i].transform.position = hit.point;
            }
        }
    }


}
#endif