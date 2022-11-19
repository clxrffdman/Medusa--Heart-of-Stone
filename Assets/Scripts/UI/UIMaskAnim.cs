using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMaskAnim : MonoBehaviour
{

    public SpriteMask sm;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sm = GetComponent<SpriteMask>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(sr.sprite != sm.sprite)
        {
            sm.sprite = sr.sprite;
        }
    }
}
