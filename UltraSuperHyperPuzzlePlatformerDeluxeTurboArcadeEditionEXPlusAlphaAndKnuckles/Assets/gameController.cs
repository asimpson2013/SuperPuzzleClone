using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls when and where blocks fall
/// </summary>
public class gameController : MonoBehaviour {

    /// <summary>
    /// How long until blocks start to fall
    /// </summary>
    public float timerStart = 5;
    /// <summary>
    /// Actual countdown of how long until the next block will fall
    /// </summary>
    public float timer;
    /// <summary>
    /// Width of the grid
    /// </summary>
    int gridWidth = 0;
    /// <summary>
    /// Height of the grid
    /// </summary>
    int gridHeight = 0;

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
	void Update () 
    {
        if (timer <= 0)
        {
            AddBlocks();
            timer = timerStart;
        }
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
        GameObject block = gridController.grid[Y, X];
        GameObject blockUp = gridController.grid[Y + 1, X];
        GameObject blockDown = gridController.grid[Y - 1, X];
        GameObject blockLeft = gridController.grid[Y, X - 1];
        GameObject blockRight = gridController.grid[Y, X + 1];
        string blockColor = gridController.grid[Y, X].GetComponent<BlockColor>().color;

        matchBlocks.Add(block);
        //When introducing Hazards we will have to add another statement here to damage those as well when a block around it is being hit.
        //This also needs to check more than just the surrounding blocks
        if (blockUp.GetComponent<BlockColor>().color == blockColor)
        {
            matchBlocks.Add(blockUp);
        }
        if (blockDown.GetComponent<BlockColor>().color == blockColor)
        {
            matchBlocks.Add(blockDown);
        }
        if (blockLeft.GetComponent<BlockColor>().color == blockColor)
        {
            matchBlocks.Add(blockLeft);
        }
        if (blockRight.GetComponent<BlockColor>().color == blockColor)
        {
            matchBlocks.Add(blockRight);
        }
        return matchBlocks;
    }
}
