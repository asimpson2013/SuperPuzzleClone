﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Decides the color of the block and stores information of the block
/// </summary>
public class BlockColor : MonoBehaviour {

    /// <summary>
    /// Stores the possible colors the block can be
    /// </summary>
    public Color[] blockColor = new Color[3] { Color.magenta, Color.green, Color.blue };
    /// <summary>
    /// A reference to the color the block is
    /// </summary>
    public string color;
    public int gridX;
    public int gridY;

    /// <summary>
    /// Picks the color of the block and gets the grid index of the block
    /// </summary>
    void Start()
    {
        Vector2 gridPlace = gridController.ConvertToGrid(transform.position.x, transform.position.y);
        gridX = (int)gridPlace.x;
        gridY = (int)gridPlace.y;
        PickColor();
    }

    /// <summary>
    /// Chooses a random number and changes the color of the block to the corrisponding index of the array
    /// </summary>
    private void PickColor()
    {
        int rand = Random.Range(0, 3);
        GetComponent<SpriteRenderer>().color = blockColor[rand];
        if (blockColor[rand] == Color.magenta)
            color = "pink";
        if (blockColor[rand] == Color.green)
            color = "green";
        if (blockColor[rand] == Color.blue)
            color = "blue";
    }
}
