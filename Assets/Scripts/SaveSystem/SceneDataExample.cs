using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDataExample : MonoBehaviour
{

    public string saveName;
    public int sceneIndex;
    public int saveIndex;
    public int savedObjectAmount;
    public string[] savedObjects;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LoadSceneExample(SceneData sceneData)
    {
        saveName = sceneData.saveName;
        sceneIndex = sceneData.sceneIndex;
        saveIndex = sceneData.saveIndex;
        savedObjectAmount = sceneData.savedObjectAmount;
        savedObjects = sceneData.savedObjects;
    }
}
