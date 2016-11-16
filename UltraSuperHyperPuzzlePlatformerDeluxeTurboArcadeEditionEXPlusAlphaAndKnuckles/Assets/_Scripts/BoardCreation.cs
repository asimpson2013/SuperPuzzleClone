using UnityEngine;
using System.Collections;

/// <summary>
/// Chooses what falls next
/// </summary>
public class BoardCreation : MonoBehaviour 
{
    /// <summary>
    /// A random integer
    /// </summary>
    int die1;
    /// <summary>
    /// A random integer
    /// </summary>
    int die2;
    /// <summary>
    /// Width of the grid
    /// </summary>
    int gridWidth = 0;
    /// <summary>
    /// Height of the grid
    /// </summary>
    int gridHeight = 0;
    /// <summary>
    /// A chosen game object
    /// </summary>
    GameObject prefabObject;
    /// <summary>
    /// Type of block
    /// </summary>
    string type;
    /// <summary>
    /// For when the computer chooses the pattern option
    /// </summary>
    bool isPattern = false;
    /// <summary>
    /// How many times the Pattern has dropped
    /// </summary>
    int PatternDrop;
    #region prefabs
    /// <summary>
    /// block prefab
    /// </summary>
    public GameObject block;
    /// <summary>
    /// star prefab
    /// </summary>
    public GameObject star;
    /// <summary>
    /// gem prefab
    /// </summary>
    public GameObject gem;
    /// <summary>
    /// crate prefab
    /// </summary>
    public GameObject crate;
    /// <summary>
    /// treasure prefab
    /// </summary>
    public GameObject treasure;
    /// <summary>
    /// cannon prefab
    /// </summary>
    public GameObject cannon;
    /// <summary>
    /// dynamite prefab
    /// </summary>
    public GameObject dynamite;
    /// <summary>
    /// spikeBlock prefab
    /// </summary>
    public GameObject spikeBlock;
    #endregion

    /// <summary>
    /// Sets gridWidth and Height
    /// </summary>
    void Start()
    {
        this.gridWidth = gridController.gridWidth;
        this.gridHeight = gridController.gridHeight;
    }

    /// <summary>
    /// Chooses whether a normal block falls or a special block falls
    /// </summary>
    public void ChooseBlocks()
    {
        die1 = Random.Range(0, 100);
        if (isPattern)
        {
            AddPattern();
        }
        else if (die1 <= 70)
        {
            AddObj(block);
            type = "block";
        }
        else
        {
            ChooseSpecial();
        }
    }

    /// <summary>
    /// chooses whether a pattern, powerup or hazard falls
    /// </summary>
    void ChooseSpecial()
    {
        die1 = Random.Range(0, 6);
        die2 = Random.Range(0, 6);
        //if level 1 just choose powerup
        int totalDice = die1 + die2;
        if (totalDice <= 3)
        {
            isPattern = true;
            AddPattern();
        }
        else if (totalDice > 3 && totalDice < 9)
        {
            type = "hazard";
            ChooseHazard();
        }
        else
        {
            type = "powerup";
            ChoosePowerUp();
        }

    }

    /// <summary>
    /// Chooses which hazard will fall
    /// </summary>
    void ChooseHazard()
    {
        die1 = Random.Range(0, 6);
        die2 = Random.Range(0, 6);
        int totalDice = die1 + die2;
        if (totalDice <= 3)
        {
            AddObj(dynamite);
        }
        else if (totalDice > 3 && totalDice < 9)
        {
            AddObj(spikeBlock);
        }
        else
        {
            AddObj(cannon);
        }
    }

    /// <summary>
    /// Chooses which Power up will fall
    /// </summary>
    void ChoosePowerUp()
    {
        die1 = Random.Range(0, 6);
        die2 = Random.Range(0, 6);
        int totalDice = die1 + die2;
        if (totalDice <= 3)
        {
            AddStar();
        }
        if (totalDice > 3 && totalDice < 9)
        {
            ChooseChests();
        }
        else
        {
            AddObj(gem);
        }
    }

    /// <summary>
    /// Chooses which chest falls
    /// </summary>
    void ChooseChests()
    {
        die1 = Random.Range(0, 100);
        if (die1 < 80)
        {
            AddObj(crate);
        }
        else
        {
            AddObj(treasure);
        }
    }

    /// <summary>
    /// Adds an object to the screen
    /// </summary>
    public void AddObj(GameObject prefabObj)
    {
        int rand = Random.Range(0, gridWidth);
        int Y = gridHeight - 1;
        GameObject block = gridController.grid[Y, rand];
        while (block != null)
        {
            if (rand == gridWidth - 1) rand = 0;
            rand++;
            block = gridController.grid[Y, rand];
        }
        Vector3 placement = gridController.ConvertToWorld(rand, Y);
        InstSprite(prefabObj, placement);
        
    }

    /// <summary>
    /// Adds a block in a pattern rather than just one block.
    /// Picks a random place for a block not to fall then adds a 
    /// line of blocks to the screen.
    /// </summary>
    public void AddPattern()
    {
        int rand = Random.Range(0, gridWidth);
        int Y = gridHeight - 1;

        for (int i = 0; i < gridWidth; i++)
        {
            if (i != rand)
            {
                Vector3 placement = gridController.ConvertToWorld(i, Y);
                InstSprite(block, placement);
            }
        }

        if (PatternDrop == 4)
        {
            isPattern = false;
            PatternDrop = 0;
        }
        PatternDrop++;
    }

    /// <summary>
    /// Adds a Star to the screen.
    /// </summary>
    public void AddStar()
    {
        Vector3 placement = new Vector3(0, 0, 0); //needs to be changed for different heights of the grid
        Instantiate(star, placement, Quaternion.identity);
    }

    /// <summary>
    /// Adds a block to the scene
    /// </summary>
    /// <param name="placement">The place the block spawns</param>
    public void InstSprite(GameObject sprite, Vector3 placement)
    {
        GameObject newSprite = (GameObject)Instantiate(sprite, placement, Quaternion.identity);
        newSprite.GetComponent<Block>().type = this.type;
    }
}
