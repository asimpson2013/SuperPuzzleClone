using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float gravity = 10;
    public float speed = 10;
    public float jumpHeight = 15;
    public float groundDetectDistance = 1;

    public LayerMask layerMask;

    Rigidbody2D body;
    Animator anim;
    SpriteRenderer sprite;
    bool grounded = true;
    bool canJump = true;
    float ySpeed;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update()
    {
        grounded = false;
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxisRaw("Vertical");

        Vector2 position = transform.position;


        
      
        if (!grounded)
        {
            ySpeed -= gravity * Time.deltaTime;
            if (ySpeed < -gravity) ySpeed = -gravity;
        }
        else ySpeed = 0;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDetectDistance, layerMask);
        if (hit.collider != null)
        {
            position.y = hit.transform.position.y + .9f;
            grounded = true;
            ySpeed = 0;
            canJump = true;
        }
        if (moveV > 0 && canJump)
        {
            canJump = false;
            ySpeed = jumpHeight;
        }
        position.y += ySpeed * Time.deltaTime;
        position.x += moveH * speed * Time.deltaTime;
        transform.position = position;

        if (moveH < 0) sprite.flipX = true;
        if (moveH > 0) sprite.flipX = false;
        anim.SetBool("Moving", Input.GetAxisRaw("Horizontal")!=0) ;
        anim.SetBool("Grounded", grounded);
    }
    
}
