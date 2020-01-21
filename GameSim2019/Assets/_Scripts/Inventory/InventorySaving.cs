using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class InventorySaving
{


    public static void SaveInventory ( ItemInventory inventory, string sceneName, string musicState )
    {
        Quaternion rotation = inventory.transform.rotation;
        Vector3 position = inventory.transform.position;
        if ( SceneController.ActiveSceneName() == "HouseInterior")
        {
            InventoryData oldData = LoadInventory();
            if ( oldData != null )
            {

                inventory.transform.position = new Vector3(oldData.pos[0], oldData.pos[1], oldData.pos[2]);
                inventory.transform.rotation = Quaternion.Euler(new Vector3(oldData.rotation[0], oldData.rotation[1], oldData.rotation[2]));
            }
        }

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.Blobisaur";
        FileStream strm = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(inventory, sceneName, musicState);

        formatter.Serialize(strm, data);
        strm.Close();

        inventory.transform.position = position;
        inventory.transform.rotation = rotation;


    }


    public static InventoryData LoadInventory ( )
    {

        string path = Application.persistentDataPath + "/inventory.Blobisaur";
        if ( File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream strm = new FileStream(path, FileMode.Open);
            
            InventoryData data = formatter.Deserialize(strm) as InventoryData;

            strm.Close();

            return data;
        } 
        else
        {
            return null; // No save file exists
        }
    }


    public static bool SaveFileCheck ()
    {
        string path = Application.persistentDataPath + "/inventory.Blobisaur";

        if ( File.Exists(path) )
            return true;

        else
            return false;
    }

    public static void DeleteSaveFile ()
    {
        string path = Application.persistentDataPath + "/inventory.Blobisaur";
        File.Delete(path);
    }
}
