using UnityEngine;
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
    /// <summary>
    /// The chosen character
    /// </summary>
    public static CharSelectControl.ChosenCharacter character = CharSelectControl.ChosenCharacter.Character1;
    /// <summary>
    /// The potential players to pick from
    /// </summary>
    public GameObject[] players;
    /// <summary>
    /// The current player on the screen
    /// </summary>
    GameObject player;
    /// <summary>
    /// The triangle peices used to level up
    /// </summary>
    public GameObject triangle;

    /// <summary>
    /// Sets grid width, grid height and starts timer
    /// </summary>
    void Start()
    {
        this.gridWidth = gridController.gridWidth;
        this.gridHeight = gridController.gridHeight;
        timer = timerStart;

        switch(character)
        {
            case CharSelectControl.ChosenCharacter.Character1:
                player = players[0];
                break;
            case CharSelectControl.ChosenCharacter.Character2:
                player = players[1];
                break;
            case CharSelectControl.ChosenCharacter.Character3:
                player = players[2];
                break;
        }

        //Inst(player);
    }

    /// <summary>
    /// Adds a block once the timer is finished and restarts the timer
    /// </summary>
    void Update()
    {
        bool isSpot = false;
        for (int i = 0; i < gridWidth; i++)
        {
            GameObject testBlock = gridController.grid[gridHeight - 1, i];
            if (testBlock == null)
            {
                isSpot = true;
            }
        }

        if (timer <= 0 && isSpot)
        {
            GetComponent<BoardCreation>().ChooseBlocks();
            timer = timerStart;
        }

        timer -= Time.deltaTime;
    }

    /// <summary>
    /// Removes Blocks from the screen
    /// </summary>
    void RemoveBlocks()
    {
        ArrayList matchBlocks = CheckBlocks(0, 0);
        for (int i = 0; i < matchBlocks.Count; i++)
        {
            int rand = Random.Range(1, 4);
            GameObject block = (GameObject)matchBlocks[i];
            gridController.removeFromGrid(block.GetComponent<Block>().gridY, block.GetComponent<Block>().gridX);
            Destroy(block.gameObject);
            AddTriangles(rand, block.transform.position);
        }
    }

    /// <summary>
    /// Adds Triangles to the Screen
    /// </summary>
    /// <param name="numb">The number of Triangles being added</param>
    /// <param name="placement">The place the triangle should spawn</param>
    void AddTriangles(int numb, Vector3 placement)
    {
        for(int i = 0; i < numb; i++)
        {
            Instantiate(triangle, placement, Quaternion.identity);
        }
    }

    /// <summary>
    /// Adds a player onto the screen
    /// </summary>
    /// <param name="player">The player game object being instantiated</param>
    //void Inst(GameObject player)
    //{
    //    GameObject newPlayer = (GameObject)Instantiate(player, playerPos, Quaternion.identity);
    //}

    //This method isn't complete (If time convert to ray casting)

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
            int gridX = block.GetComponent<Block>().gridX;
            int gridY = block.GetComponent<Block>().gridY;
            GameObject blockUp = BlockExists(gridX, gridY + 1, matchBlocks);
            GameObject blockDown = BlockExists( gridX, gridY - 1 ,matchBlocks);
            GameObject blockRight = BlockExists( gridX + 1, gridY, matchBlocks);
            GameObject blockLeft = BlockExists( gridX - 1, gridY, matchBlocks);

            if (blockUp != null && blockUp.GetComponent<BlockColor>().color == blockColor || blockUp != null && blockUp.GetComponent<Block>().type == "hazard")
            {
                matchBlocks.Add(copyGrid[gridY + 1, gridX]);
            }
            if (blockDown != null && blockDown.GetComponent<BlockColor>().color == blockColor || blockUp != null && blockUp.GetComponent<Block>().type == "hazard")
            {
                matchBlocks.Add(copyGrid[gridY - 1, gridX]);
            }
            if (blockRight != null && blockRight.GetComponent<BlockColor>().color == blockColor || blockUp != null && blockUp.GetComponent<Block>().type == "hazard")
            {
                matchBlocks.Add(copyGrid[gridY, gridX + 1]);
            }
            if (blockLeft != null && blockLeft.GetComponent<BlockColor>().color == blockColor || blockUp != null && blockUp.GetComponent<Block>().type == "hazard")
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
                int prevBlockX = prevBlock.GetComponent<Block>().gridX;
                int prevBlockY = prevBlock.GetComponent<Block>().gridY;
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
