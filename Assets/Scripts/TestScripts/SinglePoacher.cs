using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePoacher : MonoBehaviour
{

    public NpcController npcController;
    public GameObject[] patrolPositions;
    // Start is called before the first frame update
    void Awake()
    {
        npcController = transform.GetChild(0).GetComponent<NpcController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
