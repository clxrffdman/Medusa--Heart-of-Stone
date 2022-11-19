using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProgressor : MonoBehaviour
{
    public int toLevel;
    public InventoryManager inventoryManager;
    public Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {

            SavePlayer();
            Invoke("InvokeMove", 0.5f);

        }
    }

    void InvokeMove()
    {
        SaveGameManager.Instance.sceneToScene = true;

        SceneManager.LoadScene(toLevel);
    }

    public void SavePlayer()
    {
        SaveGameManager.Instance.freshSave = false;
        ObjectiveManager.Instance.SaveObjectives();
        //SaveSystem.SavePlayer(InventoryManager.Instance, GameObject.Find("Player").GetComponent<PlayerController>(), TaskManager.Instance, SaveGameManager.Instance.saveIndex);
        SaveSystem.SavePlayer(InventoryManager.Instance, FindObjectOfType<PlayerController>(), TaskManager.Instance, SaveGameManager.Instance.saveIndex);
        SaveGameManager.Instance.SaveSaveableObjects();
        
        //SaveSystem.SaveScene(GameObject.Find("Player").GetComponent<PlayerController>(), SaveGameManager.Instance.saveIndex);
        SaveSystem.SaveScene(FindObjectOfType<PlayerController>(), SaveGameManager.Instance.saveIndex);
        print("game saved!!");
    }
}
