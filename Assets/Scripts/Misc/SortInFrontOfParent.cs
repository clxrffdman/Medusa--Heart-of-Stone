using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortInFrontOfParent : MonoBehaviour
{
    public SpriteRenderer parentSpriteRenderer;
    public bool sortBehindParent;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    void Update()
    {
        if (sortBehindParent)
        {
            PlaceBehindParent();
        }
        else
        {
            PlaceInFrontOfParent();
        }
    }

    public void PlaceInFrontOfParent()
    {
        spriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder + 1;
    }

    public void PlaceBehindParent()
    {
        spriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder - 1;
    }
}
