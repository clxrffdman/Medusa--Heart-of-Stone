using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    //SCENE DATA TO BE SAVED 
    public string saveName;
    public int sceneIndex;
    public int saveIndex;
    public int savedObjectAmount;
    public string[] savedObjects;
    

    public SceneData(PlayerController player, int saveIndexInput)
    {
        saveName = SaveGameManager.Instance.fileSaveName;
        saveIndex = saveIndexInput;
        sceneIndex = player.currentSceneIndex;
        savedObjectAmount = SaveGameManager.Instance.savedObjectAmount;
        savedObjects = new string[SaveGameManager.Instance.savedObjects.Length];
        for(int i = 0; i < savedObjects.Length; i++)
        {
            savedObjects[i] = SaveGameManager.Instance.savedObjects[i];
        }
    }
}
