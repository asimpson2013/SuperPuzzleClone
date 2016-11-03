using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float speed = 10;
    public float jumpHeight = 15;
    public float groundDetectDistance = 1;

    public LayerMask layerMask;
    Rigidbody2D body;

    bool canJump = true;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxisRaw("Vertical");
        
        if (moveV > 0 && canJump) {
            canJump = false;
            body.velocity += new Vector2(0, jumpHeight);
        }
        body.AddForce(Vector2.right * moveH * speed);
        //Debug.DrawLine(transform.position, new Vector3(body.position.x, body.position.y * groundDetectDistance, 0));
        if(Physics2D.Raycast(body.position, Vector2.down, groundDetectDistance, layerMask)) {
            print("Grounded");
            canJump = true;
        }

        
	}
}
