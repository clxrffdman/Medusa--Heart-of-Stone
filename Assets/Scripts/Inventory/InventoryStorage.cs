using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStorage : MonoBehaviour
{

    public GameObject player;

    public List<Item> itemList;

    public List<GameObject> itemPrefabs;




    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>().gameObject;
        }

        itemList = InventoryManager.Instance.itemTypesList;
    }


    public Item itemFromIndex(int index)
    {

        if (index == -1)
        {
            return null;
        }
        return itemList[index];
    }

    public GameObject prefabFromIndex(int index)
    {
        if (index == -1)
        {
            return null;
        }

        return itemPrefabs[index];
    }

    public string descriptionFromIndex(int index)
    {

        if (itemList[index] != null)
        {
            return itemList[index].description;
        }
        else
        {
            return "ERROR: ITEM DESCRIPTION NOT FOUND!";
        }





    }

    public void EffectFromUse(int itemIndex)
    {
        switch (itemIndex)
        {


            case 3:
                
                print("used ammo refill");
                break;
            case 4:

                var grenade = Instantiate((GameObject)Resources.Load("bullet4", typeof(GameObject)), player.transform.position, Quaternion.identity);
                break;
            case 5:

                var stun = Instantiate((GameObject)Resources.Load("stunNade", typeof(GameObject)), player.transform.position, Quaternion.identity);
                break;
            case 6:

                var shield = Instantiate((GameObject)Resources.Load("barrierBullet", typeof(GameObject)), player.transform.position, Quaternion.identity);

                break;
            case 7:

                var scan = Instantiate((GameObject)Resources.Load("scanBullet", typeof(GameObject)), GameObject.Find("Crosshair").transform.position, Quaternion.identity);

                break;


            default:
                print("NO FUNCTION: ERROR");
                break;
        }
    }

    
}
