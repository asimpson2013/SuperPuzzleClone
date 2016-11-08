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
    /// moves the block down each frame.
    /// </summary>
    void Update()
    {
        if (fall)
        {
            gravity += gravity * Time.deltaTime;
            transform.position -= new Vector3(0, gravity, 0);
        }
        //DetectBelow();
    }

    /// <summary>
    /// Stops the block from falling after it has hit the block below or the bottom of the scene
    /// </summary>
    /// <param name="collider2D">The block it has collided with</param>
    void OnTriggerEnter2D(Collider2D collider2D)
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
        int gridX = GetComponent<BlockColor>().gridX;
        int gridY = GetComponent<BlockColor>().gridY;
        GameObject[,] copyGrid = gridController.grid;
        if(gridY - 1 > -1 && copyGrid[gridY -1, gridX] == null)
        {
            fall = true;
        }
    }
}
