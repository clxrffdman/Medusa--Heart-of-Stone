using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    protected Camera cameraMain;
    protected float startPos;
    public float parallaxEffect;
    //public float maxLeftPos;
    //public float maxRightPos;
    //public Vector3 altStartPos;
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = Camera.main;
        startPos = transform.position.x;
        //if (altStartPos != Vector3.zero)
        //{
        //    transform.position = altStartPos;
        //}

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (maxRightPos != 0)
        //{
        //    if(cameraHolder.transform.position.x > maxRightPos)
        //    {
        //        return;
        //    }
        //}

        //if (maxLeftPos != 0)
        //{
        //    if (cameraHolder.transform.position.x < maxLeftPos)
        //    {
        //        return;
        //    }
        //}
        float dist = ((startPos - cameraMain.transform.position.x) * parallaxEffect * -1);
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
    }
}
