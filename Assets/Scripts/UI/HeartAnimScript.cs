using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimScript : MonoBehaviour
{

    public Animator outlineAnim;
    public Animator maskAnim;

    // Start is called before the first frame update
    void Start()
    {
        outlineAnim = transform.GetChild(1).GetComponent<Animator>();
        maskAnim = transform.GetChild(2).GetComponent<Animator>();
    }

    // Update is called once per frame
    public void BreakHeart()
    {
        outlineAnim.SetBool("broken", true);
        maskAnim.SetBool("broken", true);
    }
}
