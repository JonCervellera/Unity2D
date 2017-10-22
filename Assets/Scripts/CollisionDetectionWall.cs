using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionWall : MonoBehaviour
{
    [Header("State")]
    public bool isWalled;
    public bool wasWalledLastFrame;
    public bool justWalled;
    public bool justNOTWalled;
    public bool isWalking;

    [Header("Boxes")]
    public Vector2 wallBoxPos;
    public Vector2 wallBoxSize;

    [Header("Properties")]
    public int maxHits = 1;
    public bool detectWall = true;
    public ContactFilter2D filter;


    private void FixedUpdate()
    {
        ResetState();
        DetectWall();
    }

    void ResetState()
    {
        wasWalledLastFrame = isWalled;
        isWalled = false;
        justNOTWalled = false;
        justWalled = false;
        isWalking = true;
    }

    void DetectWall()
    {
        if (!detectWall) return;

        Collider2D[] results = new Collider2D[maxHits];
        Vector3 newPos = (Vector3)wallBoxPos + transform.position;
        int numHits = Physics2D.OverlapBox(newPos, wallBoxSize, 0, filter, results);

        if (numHits > 0)
        {
            isWalled = true;
        }
        isWalking = !isWalled;

        if (!wasWalledLastFrame && isWalled)
        {
            Debug.Log("JUST HIT THE WALL");
            justWalled = true;
        }
        if (wasWalledLastFrame && !isWalled)
        {
            Debug.Log("JUST NOT HIT THE WALL");
            justNOTWalled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 newPos = (Vector3)wallBoxPos + transform.position;
        Gizmos.DrawWireCube(newPos, wallBoxSize);
    }
}