using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataExample : MonoBehaviour
{

    public string saveName;
    public int health;
    public float[] playerPosition;
    public int sceneIndex;
    public int saveIndex;
    public string[] invent;
    public int objectiveQuantity;
    public string[] objectiveStrings;
    public List<string> taskList;


    public void LoadPlayerExample(PlayerData playerData)
    {
        saveName = playerData.saveName;
        health = playerData.health;
        playerPosition = playerData.playerPosition;
        sceneIndex = playerData.sceneIndex;
        saveIndex = playerData.saveIndex;
        invent = playerData.invent;
        objectiveQuantity = playerData.objectiveQuantity;
        objectiveStrings = playerData.objectiveStrings;
        taskList = playerData.taskList;
    }
}
