using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveSystem
{

    //Notes on the Saving System:
    //The following functions are called by the LevelController, SingleAcion, and SceneProgressor to save PlayerData
    //and SceneData into a folder on the user's computer.

    //The LoadPlayer and LoadScene functions are called by the LevelController and MainMenuManager.

    public static void SavePlayer(InventoryManager inventory, PlayerController player, TaskManager tm, int saveIndex)
    {
        if(File.Exists(Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon"))
        {
            //Unpack(Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon", Application.persistentDataPath + "/saves/save" + saveIndex);
        }
        
        BinaryFormatter formatter = new BinaryFormatter();


        string path = Application.persistentDataPath + "/saves/save" + saveIndex + "/saveGameName" + saveIndex + ".tiredmoon";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(inventory, player, tm, saveIndex);

        formatter.Serialize(stream, data);
        stream.Close();
        //Pack(Application.persistentDataPath + "/saves/save" + saveIndex, Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon");

    }

    public static void SaveScene(PlayerController player, int saveIndex)
    {
        if (File.Exists(Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon"))
        {
            //Unpack(Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon", Application.persistentDataPath + "/saves/save" + saveIndex);
        }

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/saves/save" + saveIndex + "/saveGameName" + saveIndex + "SceneData." + player.currentSceneIndex + ".tiredmoon";
        FileStream stream = new FileStream(path, FileMode.Create);

        SceneData data = new SceneData(player, saveIndex);

        formatter.Serialize(stream, data);
        stream.Close();
        //Pack(Application.persistentDataPath + "/saves/save" + saveIndex, Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon");
    }

    public static PlayerData LoadPlayer(int saveIndex)
    {
        if (File.Exists(Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon"))
        {
            //Unpack(Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon", Application.persistentDataPath + "/saves/save" + saveIndex);
        }

        string path = Application.persistentDataPath + "/saves/save" + saveIndex + "/saveGameName" + saveIndex + ".tiredmoon";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            //Pack(Application.persistentDataPath + "/saves/save" + saveIndex, Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon");
            return data;

        }
        else
        {
            Debug.Log("SAVE FILE NOT FOUND IN " + path);
            return null;
        }
    }

    public static SceneData LoadScene(int saveIndex, int sceneIndex)
    {
        if (File.Exists(Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon"))
        {
            //Unpack(Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon", Application.persistentDataPath + "/saves/save" + saveIndex);
        }

        string path = Application.persistentDataPath + "/saves/save" + saveIndex + "/saveGameName" + saveIndex + "SceneData." + sceneIndex + ".tiredmoon";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SceneData data = formatter.Deserialize(stream) as SceneData;
            stream.Close();

            //Pack(Application.persistentDataPath + "/saves/save" + saveIndex, Application.persistentDataPath + "/saves/save" + saveIndex + "/save" + saveIndex + ".tiredmoon");
            return data;

        }
        else
        {
            Debug.Log("SAVE FILE NOT FOUND IN " + path);
            return null;
        }

        
    }

    public static void ClearSaveSlot(int saveIndex)
    {
        string path = Application.persistentDataPath + "/saves/save" + saveIndex;
        

        if (Directory.Exists(path)) { 
            Directory.Delete(path, true); 
        }
        else
        {
            Debug.Log("SAVE FILE NOT FOUND IN " + path);
        }
        Directory.CreateDirectory(path);

        
    }

    /*
    public static void Pack(string sourceFolder, string targetFile)
    {
        File.Delete(targetFile);
        using (var target = File.OpenWrite(targetFile))
        {
            using (var writer = new BinaryWriter(target))
            {
                var filesToPack = Directory.GetFiles(sourceFolder);
                writer.Write(filesToPack.Length);
                foreach (var file in filesToPack)
                {
                    var bytes = File.ReadAllBytes(file);
                    writer.Write(Path.GetFileName(file));
                    writer.Write(bytes.Length);
                    writer.Write(bytes);
                }
            }
        }

        
    }

    public static void Unpack(string sourceFile, string targetFolder)
    {
        using (var source = File.OpenRead(sourceFile))
        {
            using (var reader = new BinaryReader(source))
            {
                int totalFiles = reader.ReadInt32();
                for (int i = 0; i < totalFiles; i++)
                {
                    var file = reader.ReadString();
                    var len = reader.ReadInt32();
                    var bytes = reader.ReadBytes(len);
                    File.WriteAllBytes(Path.Combine(targetFolder, file), bytes);
                }
            }
        }
    }
    */


}
