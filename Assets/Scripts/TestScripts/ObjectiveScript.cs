using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveScript : MonoBehaviour
{
    [Header("Objective Variables")]
    private Image image;
    private TextMeshProUGUI text;
    public int id;
    public Sprite objIcon;
    public string objText;
    public List<ObjectiveScript> subObjectives;


    // Start is called before the first frame update
    void Start()
    {
        
        ObjectiveManager.Instance.Objectives.Add(this);

        if(image == null)
        {
            image = transform.GetChild(0).GetComponent<Image>();
        }

        text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if(objIcon == null)
        {
            image.sprite = Resources.Load<Sprite>("Icons/ObjectiveIcon");
        }
        else
        {
            image.sprite = objIcon;
        }

        text.text = objText;

        LeanTween.moveLocalX(transform.GetChild(0).gameObject, -91, 0.4f);
        LeanTween.moveLocalX(transform.GetChild(1).gameObject, 31, 0.4f);
    }

    // Update is called once per frame
    public void ObjectiveCompleted()
    {
        Destroy(gameObject);

    }

    public string ObjectiveToString()
    {
        return id + "_" + objText + "_" + objIcon;
    }

    public void Save(int id)
    {
        SaveGameManager.Instance.savedObjectives[id] = ObjectiveToString();
    }

    public void CheckSubObjectives()
    {

    }

}
