using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //PLAYER DATA TO BE SAVED
    public string saveName;
    public int health;
    public float[] playerPosition;
    public int sceneIndex;
    public int saveIndex;
    public string[] invent;
    public int objectiveQuantity;
    public string[] objectiveStrings;
    public List<string> taskList;
    



    public PlayerData(InventoryManager inventory, PlayerController player, TaskManager tm, int saveIndexInput)
    {
        saveName = SaveGameManager.Instance.fileSaveName;
        saveIndex = saveIndexInput;
        sceneIndex = player.currentSceneIndex;
        health = player.playerHealth;

        playerPosition = new float[3];

        playerPosition[0] = player.playerPos.x;
        playerPosition[1] = player.playerPos.y;
        playerPosition[2] = player.playerPos.z;

        invent = new string[15];
        for (int i = 0; i < inventory.inventoryItemsList.Count; i++)
        {
            invent[i] = inventory.inventoryItemsList[i];
        }

        taskList = tm.SaveTasks();

        objectiveQuantity = SaveGameManager.Instance.savedObjectiveQuantity;
        objectiveStrings = new string[SaveGameManager.Instance.savedObjectiveQuantity];
        for (int i = 0; i < SaveGameManager.Instance.savedObjectiveQuantity; i++)
        {
            objectiveStrings[i] = SaveGameManager.Instance.savedObjectives[i];
        }
    }
}
