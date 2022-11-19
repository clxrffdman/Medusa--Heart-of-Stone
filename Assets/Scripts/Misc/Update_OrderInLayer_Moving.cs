using UnityEngine;
using System.Collections;


public class Update_OrderInLayer_Moving : MonoBehaviour {

	[SerializeField] private int internalSort = 0;

	private SpriteRenderer spriteRenderer;
	private Transform objectTransform;
	private bool hasChildSpriteRenderers;
	private Update_order_child_from_parent[] childArray;
	public int sortingOrderFactor = 100;

	void Start(){
		if (GetComponentsInChildren<Update_order_child_from_parent>() != null)
		{
			hasChildSpriteRenderers = true;
			childArray = GetComponentsInChildren<Update_order_child_from_parent>();
		}
		spriteRenderer = GetComponent<SpriteRenderer> ();
		objectTransform = GetComponent<Transform> ();
		spriteRenderer.sortingOrder = (int)(objectTransform.position.y * -sortingOrderFactor) + internalSort;
	}

	void Update(){
		spriteRenderer.sortingOrder = (int)(objectTransform.position.y * -sortingOrderFactor) + internalSort;
		if(hasChildSpriteRenderers)
        {
			foreach(Update_order_child_from_parent a in childArray)
            {
				a.UpdateSort();
            }
		}
	}
}

