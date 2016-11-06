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
    }

    /// <summary>
    /// Stops the block from falling after it has hit the block below or the bottom of the scene
    /// </summary>
    /// <param name="collider2D">The block it has collided with</param>
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        fall = false;
        //TODO: move block outside of the block underneath
        Vector2 gridPlace = gridController.ConvertToGrid(transform.position.x, transform.position.y);
        Vector3 newPos = gridController.ConvertToWorld((int)gridPlace.x, (int)gridPlace.y);
        transform.position = newPos;
        gridController.addToGrid((int)gridPlace.y, (int)gridPlace.x, this.gameObject);
    }
}
