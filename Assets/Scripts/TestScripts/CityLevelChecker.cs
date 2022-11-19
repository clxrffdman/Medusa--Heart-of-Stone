using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityLevelChecker : MonoBehaviour
{

    public bool isLevel;
    public GameObject[] tiedBackdrop;
    public SpriteRenderer sr;
    public bool isGreyed;
    public bool isTransition;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            isLevel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isLevel = false;
        }
    }
}
