using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Update_order_child_from_parent : MonoBehaviour
{
    public int internal_layer;
    private SpriteRenderer sr;
    public SpriteRenderer parent_sr;

    // Start is called before the first frame update
    void Start()
    {
        parent_sr = transform.parent.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        UpdateSort();
    }

    public void UpdateSort()
    {
        sr.sortingOrder = parent_sr.sortingOrder + internal_layer;
    }
}
