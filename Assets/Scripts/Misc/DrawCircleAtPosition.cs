using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircleAtPosition : MonoBehaviour
{
    public float sphereSize;
    public enum SphereColor { Red, Blue, Green}
    public SphereColor sphereColor;
    private void OnDrawGizmos()
    {
        switch (sphereColor)
        {
            case SphereColor.Red:
                Gizmos.color = Color.red;
                break;
            case SphereColor.Blue:
                Gizmos.color = Color.blue;
                break;
            case SphereColor.Green:
                Gizmos.color = Color.green;
                break;

        }

        Gizmos.DrawWireSphere(transform.position, sphereSize);
    }
}
