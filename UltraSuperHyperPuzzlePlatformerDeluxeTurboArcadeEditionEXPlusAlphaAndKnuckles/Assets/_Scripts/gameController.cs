﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls when and where blocks fall
/// </summary>
public class gameController : MonoBehaviour
{

    /// <summary>
    /// How long until blocks start to fall
    /// </summary>
    public float timerStart = 5;
    /// <summary>
    /// Actual countdown of how long until the next block will fall
    /// </summary>
    public float timer;
    /// <summary>
    /// The position of the player when instantiated
    /// </summary>
    public Vector3 playerPos;
    /// <summary>
    /// Width of the grid
    /// </summary>
    int gridWidth = 0;
    /// <summary>
    /// Height of the grid
    /// </summary>
    int gridHeight = 0;
    //Testing variable will delete later
    float prevPress = 0;

    /// <summary>
    /// Sets grid width, grid height and starts timer
    /// </summary>
    void Start()
    {
        this.gridWidth = gridController.gridWidth;
        this.gridHeight = gridController.gridHeight;
        timer = timerStart;
    }

    /// <summary>
    /// Adds a block once the timer is finished and restarts the timer
    /// </summary>
    void Update()
    {
        if (timer <= 0)
        {
            AddBlocks();
            timer = timerStart;
        }

        //Testing statment will delete later
        //if (Input.GetAxis("Submit") != prevPress && Input.GetAxis("Submit") > 0)
        //{
        //    ArrayList matchBlocks = CheckBlocks(0, 0);
        //    for (int i = 0; i < matchBlocks.Count; i++)
        //    {
        //        GameObject block = (GameObject)matchBlocks[i];
        //        gridController.removeFromGrid(block.GetComponent<BlockColor>().gridY, block.GetComponent<BlockColor>().gridX);
        //        Destroy(block.gameObject);
        //    }
        //}
        //prevPress = Input.GetAxis("Submit");
        timer -= Time.deltaTime;
    }

    /// <summary>
    /// Adds a block to a random x location off the screen
    /// </summary>
    void AddBlocks()
    {
        int rand = Random.Range(0, gridWidth);
        int Y = gridHeight + 5; //need to change this to a editable variable

        Vector3 placement = gridController.ConvertToWorld(rand, Y);
        GetComponent<gridController>().InstBlock(placement);
    }

    //This method isn't complete
    /// <summary>
    /// Checks surrounding blocks for matching colors
    /// </summary>
    /// <param name="X">The x component of the selected block</param>
    /// <param name="Y">The y component of the selected block</param>
    /// <returns>An array list of all matching blocks</returns>
    ArrayList CheckBlocks(int X, int Y)
    {
        ArrayList matchBlocks = new ArrayList();
        GameObject[,] copyGrid = gridController.grid;
        GameObject block = copyGrid[Y, X];
        string blockColor = null;

        blockColor = block.GetComponent<BlockColor>().color;
        matchBlocks.Add(block);
        //When introducing Hazards we will have to add another statement to damage those as well when a block around it is being hit.
        for (int i = 0; i <= matchBlocks.Count - 1; i++)
        {
            block = (GameObject)matchBlocks[i];
            int gridX = block.GetComponent<BlockColor>().gridX;
            int gridY = block.GetComponent<BlockColor>().gridY;
            GameObject blockUp = BlockExists(gridX, gridY + 1, matchBlocks);
            GameObject blockDown = BlockExists( gridX, gridY - 1 ,matchBlocks);
            GameObject blockRight = BlockExists( gridX + 1, gridY, matchBlocks);
            GameObject blockLeft = BlockExists( gridX - 1, gridY, matchBlocks);

            if (blockUp != null && blockUp.GetComponent<BlockColor>().color == blockColor)
            {
                matchBlocks.Add(copyGrid[gridY + 1, gridX]);
            }
            if (blockDown != null && blockDown.GetComponent<BlockColor>().color == blockColor)
            {
                matchBlocks.Add(copyGrid[gridY - 1, gridX]);
            }
            if (blockRight != null && blockRight.GetComponent<BlockColor>().color == blockColor)
            {
                matchBlocks.Add(copyGrid[gridY, gridX + 1]);
            }
            if (blockLeft != null && blockLeft.GetComponent<BlockColor>().color == blockColor)
            {
                matchBlocks.Add(copyGrid[gridY, gridX - 1]);
            }
        }
        return matchBlocks;
    }

    /// <summary>
    /// Checks to see if the block being checked is part of the grid and if the block is already part of the matchblocks array
    /// </summary>
    /// <param name="gridX">The grid x-position being checked</param>
    /// <param name="gridY">The grid y-position being checked</param>
    /// <param name="matchBlocks"></param>
    /// <returns>null if the block is already in the matchblocks array and the block being checked if the block isn't in the array</returns>
    GameObject BlockExists(int gridX, int gridY, ArrayList matchBlocks)
    {
        int YMax = gridController.gridHeight;
        int XMax = gridController.gridWidth;
        bool inArray = false;

        if (gridX < XMax && gridX > -1 && gridY < YMax && gridY > -1)
        {
            GameObject block = gridController.grid[gridY, gridX];
            for (int j = 0; j <= matchBlocks.Count - 1; j++)
            {
                GameObject prevBlock = (GameObject)matchBlocks[j];
                int prevBlockX = prevBlock.GetComponent<BlockColor>().gridX;
                int prevBlockY = prevBlock.GetComponent<BlockColor>().gridY;
                if (gridX == prevBlockX && !inArray)
                {
                    if (gridY == prevBlockY)
                    {
                        inArray = true;
                    }
                    
                }
            }

            if(!inArray)
            {
                return block;
            }
        }
        return null;
    }
}
