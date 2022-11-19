using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class lives on the LevelManager object and is for saving saveable objects in the game.
public class SaveGameManager : MonoBehaviour
{
    private static SaveGameManager instance;
    public List<SaveableObject> SaveableObjects;

    public string fileSaveName;
    public int saveIndex;
    public int savedObjectAmount;
    public string[] savedObjects;
    public int savedObjectiveQuantity;
    public string[] savedObjectives;

    public bool loaded;
    public bool freshSave;
    public bool sceneToScene;

    public static SaveGameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveGameManager>();
                //DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        SaveableObjects = new List<SaveableObject>();
        fileSaveName = GameManager.Instance.fileSaveName;
        saveIndex = GameManager.Instance.saveIndex;
        loaded = GameManager.Instance.loaded;
        freshSave = GameManager.Instance.freshSave;
        sceneToScene = GameManager.Instance.sceneToScene;
}

    public void SaveSaveableObjects()
    {
        //for (int i = SaveableObjects.Count-1; i >= 0; i--)
        //{
        //    if(!(SaveableObjects[i].GetComponent<InteractableItem>() == null))
        //    {
        //        if (SaveableObjects[i].GetComponent<InteractableItem>().hasBeenCollected == true)
        //        {
        //            SaveableObjects.RemoveAt(i);
        //        }                
        //    }
        //}

        savedObjectAmount = SaveableObjects.Count;
        savedObjects = new string[SaveableObjects.Count];

        for (int i = 0; i < SaveableObjects.Count; i++)
        {
            SaveableObjects[i].Save(i);
        }
    }

    public void LoadSaveableObjects()
    {
        if (savedObjectAmount <= 0)
        {
            return;
        }
       
        //This code destroys all saveable objects that are not environmental objects.
        //foreach(SaveableObject obj in SaveableObjects)
        //{
        //    if(obj != null)
        //    {
        //        if (!(obj.GetComponent<EnvironmentalObject>()))
        //        {
        //            Destroy(obj.gameObject);
        //        }
        //    }
        //}

        SaveableObjects.Clear();


        int objectCount = savedObjectAmount;
        for(int i = 0; i < objectCount; i++)
        {
            string[] value = savedObjects[i].Split('_');
            GameObject tmp = null;
            //value 0 is object type
            //value 1 is position
            //value 2 is object name
            //value 3 is object state


            switch (value[0]) {

                case "Pickup":
                    //tmp = Instantiate(Resources.Load("ResourcePrefabs/" + value[2]) as GameObject, GameObject.Find("ItemPickups").transform); 
                    if (GameObject.Find(value[2]) != null)
                    {
                        tmp = GameObject.Find(value[2]);
                        SaveableObjects.Add(tmp.GetComponent<PickupObject>());
                    }
                    break;
                case "Enemy":
                    //tmp = Instantiate(Resources.Load("ResourcePrefabs/PickUpItem") as GameObject);
                    break;
                case "Environmental":
                    //tmp = Instantiate(Resources.Load("ResourcePrefabs/environmentalInteractable") as GameObject, GameObject.Find("EnvironmentalInteractables").transform);
                    if(GameObject.Find(value[2]) != null)
                    {
                        tmp = GameObject.Find(value[2]);
                        SaveableObjects.Add(tmp.GetComponent<EnvironmentalObject>());
                    }
                    break;
            }

            if(tmp != null)
            {
                SaveableObject saveableObject = tmp.GetComponent<SaveableObject>();
                if (saveableObject == null)
                {
                    print(tmp.name + "has NO saveable object");
                }

                saveableObject.Load(value);
            }  
        }
    }

    public Vector3 StringToVector(string value)
    {
        value = value.Trim(new char[] { '(', ')' });
        value = value.Replace(" ", "");
        string[] pos = value.Split(',');

        return new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
    }

    public Quaternion StringToQuaternion(string value)
    {
        return Quaternion.identity;
    }
}
