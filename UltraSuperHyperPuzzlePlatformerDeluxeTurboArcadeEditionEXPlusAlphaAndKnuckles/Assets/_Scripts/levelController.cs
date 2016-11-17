using UnityEngine;
using System.Collections;

/// <summary>
/// Controlls the leveling of the character
/// </summary>
public class levelController : MonoBehaviour {

    /// <summary>
    /// Current level of the player
    /// </summary>
    public static int levelCount = 1;
    /// <summary>
    /// The amount of triangles needed to level up
    /// </summary>
    public static float expAmount = 300;
    /// <summary>
    /// The current experience of the player
    /// </summary>
    public static int currExp = 0;
    /// <summary>
    /// The maximum level of the player
    /// </summary>
    public static int maxLevel = 4;
	
	/// <summary>
	/// Adds a level when the 
	/// </summary>
	void Update () 
    {
	    if (levelCount != maxLevel)
        {
            AddLevel();
        }
	}

    /// <summary>
    /// Adds a level to the character
    /// </summary>
    void AddLevel()
    {
        if (currExp >= expAmount)
        {
            levelCount++;
            expAmount = Mathf.Pow(expAmount, 2f);
            currExp = currExp - (int)expAmount;
        }
    }

    /// <summary>
    /// Adds experience to the current experience
    /// </summary>
    /// <param name="Amt">The amount of exp being added</param>
    public static void AddExp(int Amt)
    {
        currExp += Amt;
    }
}
