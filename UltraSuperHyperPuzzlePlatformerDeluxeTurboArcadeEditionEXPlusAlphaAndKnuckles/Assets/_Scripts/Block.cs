using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour 
{
    /// <summary>
    /// The x-coordinate of the block on the grid array
    /// </summary>
    public int gridX;
    /// <summary>
    /// The y coordinat of the block on the grid array
    /// </summary>
    public int gridY;
    /// <summary>
    /// What type of object this block is
    /// </summary>
    public string type;

    /// <summary>
    /// Picks the color of the block and gets the grid index of the block
    /// </summary>
    void Start()
    {
        UpdateGridPlace();
    }

    /// <summary>
    /// Makes sure grid placement is correct
    /// </summary>
    void Update()
    {
        UpdateGridPlace();
    }

    /// <summary>
    /// Initializes the grid placement of each block
    /// </summary>
    private void UpdateGridPlace()
    {
        Vector2 gridPlace = gridController.ConvertToGrid(transform.position.x, transform.position.y);
        gridX = (int)gridPlace.x;
        gridY = (int)gridPlace.y;
    }
}
