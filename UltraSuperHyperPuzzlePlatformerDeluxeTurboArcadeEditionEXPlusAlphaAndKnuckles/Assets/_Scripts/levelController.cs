using UnityEngine;
using System.Collections;

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
	
	// Update is called once per frame
	void Update () 
    {
	    if (levelCount != maxLevel)
        {

        }
	}

    void AddLevel()
    {
        if (currExp >= expAmount)
        {
            levelCount++;
            expAmount = Mathf.Pow(expAmount, 2f);
        }
    }

    void AddExp(int Amt)
    {
        currExp += Amt;
    }
}
