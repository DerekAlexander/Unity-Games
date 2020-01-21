using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FarmSaving
{


    public static void SaveFarm ( FarmManager farm )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/farm.Blobisaur";
        FileStream strm = new FileStream(path, FileMode.Create);

        FarmData farmData = new FarmData(farm);

        formatter.Serialize(strm, farmData);
        strm.Close();

    }



    public static void SaveFarm ( FarmData farm )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/farm.Blobisaur";
        FileStream strm = new FileStream(path, FileMode.Create);

        FarmData farmData = farm;

        formatter.Serialize(strm, farmData);
        strm.Close();

    }


    public static FarmData LoadFarm ()
    {
        string path = Application.persistentDataPath + "/farm.Blobisaur";
        if ( File.Exists(path) )
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream strm = new FileStream(path, FileMode.Open);

            FarmData data = formatter.Deserialize(strm) as FarmData;

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
        string path = Application.persistentDataPath + "/farm.Blobisaur";

        if ( File.Exists(path) )
            return true;

        else
            return false;
    }

    public static void DeleteSaveFile ()
    {
        string path = Application.persistentDataPath + "/farm.Blobisaur";
        File.Delete(path);
    }

}
