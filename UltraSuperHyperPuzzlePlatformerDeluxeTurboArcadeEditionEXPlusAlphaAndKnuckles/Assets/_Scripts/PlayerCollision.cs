using UnityEngine;
using System.Collections;

/// <summary>
/// The collision of the player
/// </summary>
public class PlayerCollision : MonoBehaviour 
{
    /// <summary>
    /// The experience added by a triangle piece
    /// </summary>
    public int triExp = 5;
    /// <summary>
    /// The experience added by a gem
    /// </summary>
    public int gemExp = 20;

    /// <summary>
    /// What to do when the player collides with another collider
    /// </summary>
    /// <param name="collision2D">The collider object</param>
	void OnCollisionEnter2D(Collision2D collision2D)
    {
        if(collision2D.gameObject.tag == "triangle")
        {
            levelController.AddExp(triExp);
        }
        if(collision2D.gameObject.tag == "gem")
        {
            levelController.AddExp(gemExp);
        }
    }
}
