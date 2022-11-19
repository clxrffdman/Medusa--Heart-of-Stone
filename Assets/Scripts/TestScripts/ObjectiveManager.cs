using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    private static ObjectiveManager objectiveInstance;

    public List<ObjectiveScript> Objectives;


    public static ObjectiveManager Instance
    {
        get
        {
            if (objectiveInstance == null)
            {
                objectiveInstance = GameObject.FindObjectOfType<ObjectiveManager>();
            }
            return objectiveInstance;
        }


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddObjective(int id, string objText, string objIcon)
    {
        GameObject newObj = Instantiate(Resources.Load("ObjectivePrefabs/Objective") as GameObject, transform.GetChild(0));
        newObj.GetComponent<ObjectiveScript>().id = id;
        newObj.GetComponent<ObjectiveScript>().objText = objText;
        if (objIcon != "")
        {
            newObj.GetComponent<ObjectiveScript>().objIcon = Resources.Load<Sprite>(objIcon);
        }
    }

    public void AddObjective(string value)
    {
        string[] values = value.Split('_');
        GameObject newObj = Instantiate(Resources.Load("ObjectivePrefabs/Objective") as GameObject, transform.GetChild(0));
        newObj.GetComponent<ObjectiveScript>().id = int.Parse(values[0]);
        newObj.GetComponent<ObjectiveScript>().objText = values[1];
        if (values[2] != "")
        {
            newObj.GetComponent<ObjectiveScript>().objIcon = Resources.Load<Sprite>(values[2]);
        }
    }

    public void AddObjective(int id)
    {
        switch (id)
        {

            case 0:
                AddObjective(id, "Objective: Objective Zero", "");
                break;
            case 1:
                AddObjective(id, "Objective: Objective One", "");
                break;

            default:
                print("NO OBJECTIVE ID FOUND");
                break;

        }

    }

    public void PassObjective(int id)
    {
        for (int i = Objectives.Count - 1; i >= 0; i--)
        {
            if (Objectives[i].id == id)
            {
                Objectives[i].ObjectiveCompleted();
                Objectives.RemoveAt(i);
            }
        }
    }

    public void RemoveAllObjectives()
    {
        for (int i = Objectives.Count - 1; i >= 0; i--)
        {
            Objectives[i].ObjectiveCompleted();
            Objectives.RemoveAt(i);


        }
    }

    public void SaveObjectives()
    {

        SaveGameManager.Instance.savedObjectiveQuantity = Objectives.Count;
        SaveGameManager.Instance.savedObjectives = new string[Objectives.Count];
            for(int i = 0; i < Objectives.Count; i++)
            {
            SaveGameManager.Instance.savedObjectives[i] = Objectives[i].ObjectiveToString();
            }
        
    }

    


}
