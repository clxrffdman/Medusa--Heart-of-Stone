using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurLampDetection : MonoBehaviour
{

    public bool isInRange;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Minotaur")
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Minotaur")
        {
            isInRange = false;
        }
    }
}
