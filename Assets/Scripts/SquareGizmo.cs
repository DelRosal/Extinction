using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGizmo : MonoBehaviour
{
    [SerializeField]
    private float xSize;
    [SerializeField]
    private float ySize;

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(xSize, ySize, 1));
    }
}
