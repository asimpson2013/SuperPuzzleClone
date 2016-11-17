using UnityEngine;
using System.Collections;

public class EnemyBulletMove : MonoBehaviour {
    /// <summary>
    /// LayerMask used in collision detection.
    /// Allows bullets to only collide with blocks and hazards.
    /// </summary>
    public LayerMask mask;
    /// <summary>
    /// The amount of damage the bullet does to a block.
    /// </summary>
    public int power = 1;
    public float speed = 5;

    gameController game;
    SpriteRenderer player;
    // Use this for initialization
    void Start() {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameController>();
        player = GameObject.FindGameObjectWithTag("Cannon").GetComponent<SpriteRenderer>();
        if (player.flipX) speed *= -1;
    }

    // Update is called once per frame
    void Update() {
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other) {
        
    }
}
