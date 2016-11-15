using UnityEngine;
using System.Collections;

/// <summary>
/// Adds gravity to the blocks
/// </summary>
public class FallBlock : MonoBehaviour
{

    /// <summary>
    /// The amount of gravity added
    /// </summary>
    public float gravity = .01f;
    /// <summary>
    /// If the block has landed on another block yet or not
    /// </summary>
    public bool fall = true;
    /// <summary>
    /// The layer that the block is on
    /// </summary>
    public LayerMask layerMask;
    public Collider2D collider;

    /// <summary>
    /// moves the block down each frame.
    /// </summary>
    void Update()
    {
        if (fall)
        {
            gravity += gravity * Time.deltaTime;
            transform.position -= new Vector3(0, gravity, 0);
        }
        DetectBelow();
    }

    /// <summary>
    /// Stops the block from falling after it has hit the block below or the bottom of the scene
    /// </summary>
    /// <param name="collider2D">The block it has collided with</param>
    void BlockCollision(Collider2D collider2D)
    {
        fall = false;
        Vector2 gridPlace = gridController.ConvertToGrid(transform.position.x, transform.position.y);
        Vector3 newPos = gridController.ConvertToWorld((int)gridPlace.x, (int)gridPlace.y);
        transform.position = newPos;
        gridController.addToGrid((int)gridPlace.y, (int)gridPlace.x, this.gameObject);
    }

    /// <summary>
    /// Checks for a block below
    /// </summary>
    void DetectBelow()
    {
        int gridX = GetComponent<Block>().gridX;
        int gridY = GetComponent<Block>().gridY;
        Vector2 bottom = new Vector2(transform.position.x, transform.position.y - .6f);
        RaycastHit2D hit = Physics2D.Raycast(bottom, Vector2.down, .03f, layerMask);
        Vector3 dis = new Vector3(0, -.005f, 0);
        Debug.DrawRay(bottom, dis);
        collider = hit.collider;
        if (hit.collider == null)
        {
            fall = true;
        }
        else
        {
            BlockCollision(hit.collider);
        }
    }
}
