using UnityEngine;
using System.Collections;

/// <summary>
/// Controls the grid
/// </summary>
public class gridController : MonoBehaviour
{

    #region Static Variables
    /// <summary>
    /// refrence to the block sprite
    /// </summary>
    public GameObject block;
    /// <summary>
    /// The pixel measurement per one unit.
    /// </summary>
    static float perUnit;
    /// <summary>
    /// A two dimensional array that stores blocks in a grid
    /// </summary>
    public static GameObject[,] grid;
    /// <summary>
    /// The width of the grid by the number of blocks
    /// </summary>
    public static int gridWidth = 5;
    /// <summary>
    /// The height of the grid by the number of blocks
    /// </summary>
    public static int gridHeight = 5;
    /// <summary>
    /// The width of the block in meters
    /// </summary>
    static float blockWidth = 1;
    /// <summary>
    /// The height of the blocks in meters
    /// </summary>
    static float blockHeight = 10;
    /// <summary>
    /// Where the blocks start on the screen in the x direction
    /// </summary>
    static float startX = -2;
    /// <summary>
    /// Where the blocks start on the screen in the y direction
    /// </summary>
    static float startY = -2;
    #endregion

    #region public vars
    //Unsure if we will keep these so they are staying uncommented
    public int startWidth = 9;
    public int startHeight = 1;
    public float pixelsPerUnit = 32;
    public float pstartX = 0;
    public float pstartY = 0;
    public int pgridWidth = 0;
    public int pgridHeight = 0;
    #endregion

    /// <summary>
    /// Adds a grid to the scene
    /// </summary>
    void Start()
    {
        //Maybe get the height and width of the screen and calculate a grid width and height that way?
        gridWidth = pgridWidth;
        gridHeight = pgridHeight;
        perUnit = pixelsPerUnit;
        blockWidth = block.GetComponent<SpriteRenderer>().sprite.rect.size.x;
        blockHeight = block.GetComponent<SpriteRenderer>().sprite.rect.size.y;
        startX = pstartX;
        startY = pstartY;
        grid = new GameObject[gridHeight, gridWidth];
        CreateGrid();
    }

    /// <summary>
    /// Makes a grid of blocks and instantiates it
    /// </summary>
    void CreateGrid()
    {
        for (int y = 0; y < startHeight; y++)
        {
            for (int x = 0; x < startWidth; x++)
            {
                Vector3 placement = ConvertToWorld(x, y);
                GameObject newBlock = (GameObject)Instantiate(block, placement, Quaternion.identity);
                newBlock.GetComponent<FallBlock>().fall = false;
                grid[y, x] = newBlock;
            }
        }
    }

    /// <summary>
    /// Adds a block to the scene
    /// </summary>
    /// <param name="placement">The place the block spawns</param>
    public void InstBlock(Vector3 placement)
    {
        GameObject newBlock = (GameObject)Instantiate(block, placement, Quaternion.identity);
    }

    /// <summary>
    /// adds a block to the grid array
    /// </summary>
    /// <param name="y">The row that the array is being added to</param>
    /// <param name="x">The column that the array is being added to</param>
    /// <param name="block"></param>
    public static void addToGrid(int y, int x, GameObject block)
    {
        grid[y, x] = block;
    }

    public static void removeFromGrid(int Y, int X)
    {
        grid[Y, X] = null;
        GameObject[,] newGrid = new GameObject[gridHeight, gridWidth];
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (grid[y, x] != null)
                {
                    newGrid[y, x] = grid[y, x];
                }
            }
        }
        grid = newGrid;
    }

    #region Conversions
    /// <summary>
    /// Converts the grid coordinates to world coordinates
    /// </summary>
    /// <param name="X">x grid coordinate</param>
    /// <param name="Y">y grid coordinate</param>
    /// <returns>The world point equivalent to the grid point</returns>
    public static Vector3 ConvertToWorld(int X, int Y)
    {
        float worldX = startX + (X * (blockWidth / perUnit));
        float worldY = startY + (Y * (blockHeight / blockHeight));
        Vector3 worldCoord = new Vector3(worldX, worldY, 0);
        return worldCoord;
    }

    /// <summary>
    /// Converts the world coordinates to grid coordinates
    /// </summary>
    /// <param name="worldX">x world coordinate</param>
    /// <param name="worldY">y world coordinate</param>
    /// <returns>The grid point equivalent to the world point</returns>
    public static Vector2 ConvertToGrid(float worldX, float worldY)
    {
        int X = (int)Mathf.Ceil((worldX / (blockWidth / perUnit)) - startX);
        int Y = (int)Mathf.Ceil((worldY / (blockHeight / perUnit)) - startY);
        Vector2 gridCoord = new Vector2(X, Y);
        return gridCoord;
    }
    #endregion
}
