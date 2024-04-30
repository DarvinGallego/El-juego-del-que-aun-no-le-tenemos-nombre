using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//Sistema de guardado basado en serializacion binaria

public static class SaveManager
{
    public static void SavePlayerData (PlayerController Player)
    {
        DataPlayer dataPlayer = new DataPlayer(Player);
        string dataPath = Application.persistentDataPath + "/player.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, dataPlayer);
        fileStream.Close();
    }

    public static DataPlayer LoadPlayerData()
    {
        string dataPath = Application.persistentDataPath + "/player.save";
        
        if(File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            DataPlayer dataPlayer = (DataPlayer) formatter.Deserialize(fileStream);
            fileStream.Close();

            return dataPlayer;
        }
        else
        {
            Debug.LogError("No se encontro ningun archivo de guardado");
            return null;
        }
    }
}