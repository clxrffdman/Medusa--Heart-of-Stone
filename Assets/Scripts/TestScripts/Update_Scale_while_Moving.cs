using UnityEngine;
using System.Collections;


public class Update_Scale_while_Moving : MonoBehaviour
{

	public Vector3 originalScale;

	private SpriteRenderer spriteRenderer;
	private Transform objectTransform;
	private bool hasChildSpriteRenderers;
	private Update_order_child_from_parent[] childArray;
	public int sortingOrderFactor = 100;

	void Start()
	{

		/*
		if (GetComponentsInChildren<Update_order_child_from_parent>() != null)
		{
			hasChildSpriteRenderers = true;
			childArray = GetComponentsInChildren<Update_order_child_from_parent>();
		}
		*/
		
		objectTransform = GetComponent<Transform>();
		originalScale = objectTransform.localScale;
		
	}

	void Update()
	{
		objectTransform.localScale = originalScale*(objectTransform.position.y * -sortingOrderFactor);
	}
}

