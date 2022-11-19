using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScalar : MonoBehaviour
{
    public Vector3 initialScale;
    public float sortingOrderFactor;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
        transform.localScale = initialScale + new Vector3(transform.position.y * -sortingOrderFactor, transform.position.y * -sortingOrderFactor, transform.position.y * -sortingOrderFactor);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = initialScale + new Vector3(transform.position.y * -sortingOrderFactor, transform.position.y * -sortingOrderFactor, transform.position.y * -sortingOrderFactor);
    }
}
