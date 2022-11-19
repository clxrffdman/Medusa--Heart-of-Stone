using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoacherController : MonoBehaviour
{

    public List<SinglePoacher> poachers;

    // Start is called before the first frame update
    void Start()
    {
        foreach(SinglePoacher sp in poachers)
        {

            sp.npcController.targetDestination = sp.patrolPositions[0].transform;
            sp.npcController.ChangeStateToSceneControlled();
            sp.npcController.StartAStarPathfinding();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
