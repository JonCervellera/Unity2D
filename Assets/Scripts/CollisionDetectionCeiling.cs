using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionCeiling : MonoBehaviour
{
    [Header("State")]
    public bool isCeiled;
    public bool wasCeiledLastFrame;
    public bool justCeiled;
    public bool justNOTCeiled;
    public bool isJumping;


    [Header("Boxes")]
    public Vector2 ceilingBoxPos;
    public Vector2 ceilingBoxSize;

    [Header("Properties")]
    public int maxHits = 1;
    public bool detectCeiling = true;
    public ContactFilter2D filter;


    private void FixedUpdate()
    {
        ResetState();
        DetectCeiling();
    }

    void ResetState()
    {
        wasCeiledLastFrame = isCeiled;
        isCeiled = false;
        justNOTCeiled = false;
        justCeiled = false;
        isJumping = true;
    }

    void DetectCeiling()
    {
        if (!detectCeiling) return;

        Collider2D[] results = new Collider2D[maxHits];
        Vector3 newPos = (Vector3)ceilingBoxPos + transform.position;
        int numHits = Physics2D.OverlapBox(newPos, ceilingBoxSize, 0, filter, results);

        if (numHits > 0)
        {
            isCeiled = true;
        }
        isJumping = !isCeiled;

        if (!wasCeiledLastFrame && isCeiled)
        {
            Debug.Log("JUST CEILED");
            justCeiled = true;
        }
        if (wasCeiledLastFrame && !isCeiled)
        {
            Debug.Log("JUST NOT CEILED");
            justNOTCeiled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 newPos = (Vector3)ceilingBoxPos + transform.position;
        Gizmos.DrawWireCube(newPos, ceilingBoxSize);
    }
}