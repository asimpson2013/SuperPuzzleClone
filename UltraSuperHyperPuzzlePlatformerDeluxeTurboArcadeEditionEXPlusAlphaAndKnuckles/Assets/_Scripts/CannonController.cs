using UnityEngine;
using System.Collections;

public class CannonController : MonoBehaviour {
    /// <summary>
    /// The bullet this cannon will shoot
    /// </summary>
    public GameObject bullet;
    /// <summary>
    /// The amount of time between shots in seconds
    /// </summary>
    public float shootTimer = 4;
    /// <summary>
    /// A reference to the sprite renderer
    /// </summary>
    private SpriteRenderer sprite;
    /// <summary>
    /// The actual timer for shooting bullets.
    /// </summary>
    private float timer;
	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        if (transform.position.x > 0) sprite.flipX = true;
        timer = shootTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > 0) sprite.flipX = true;
        timer -= Time.deltaTime;
        
        if(timer <= 0) {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timer = shootTimer;
        }
        print(timer);
	}
}
