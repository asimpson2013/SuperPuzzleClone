using UnityEngine;
using System.Collections;

public class Triangle : MonoBehaviour {

    /// <summary>
    /// The amount of gravity added
    /// </summary>
    public float gravity = .01f;
    /// <summary>
    /// If the block has landed on another block yet or not
    /// </summary>
    public bool fall = true;
    /// <summary>
    /// The layer that the block is on
    /// </summary>
    public LayerMask layerMask;
    /// <summary>
    /// The place that the triangle should move if a block falls on top of it
    /// </summary>
    public float yplace;
    /// <summary>
    /// How many seconds the triangle lasts before disappearing
    /// </summary>
    public float lifeTime;

	/// <summary>
	/// Sets the x position of the triangle
	/// </summary>
	void Start () 
    {
        float rand = 0;
        Vector2 minPlacement = gridController.ConvertToGrid(transform.position.x - .5f, 0);
        Vector2 maxPlacement = gridController.ConvertToGrid(transform.position.x + .5f, 0);
        if (minPlacement.x < 0)
        {
            rand = Random.Range(0, .5f);
        }
        if (maxPlacement.x > gridController.gridWidth - 1)
        {
            rand = Random.Range(-.5f, 0);
        }
        else
        {
            rand = Random.Range(-.5f, .5f);
        }
        transform.position += new Vector3(rand, 0, 0);
	}
	
	/// <summary>
	/// Makes the object fall every frame
	/// </summary>
	void Update () 
    {
        if (fall)
        {
            gravity += gravity * Time.deltaTime;
            transform.position -= new Vector3(0, gravity, 0);
        }
        DetectBelow();
        DetectAbove();
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    /// <summary>
    /// Figures out if the object should fall or not
    /// </summary>
    /// <param name="collider2D">The collision object</param>
    void BlockCollision(Collider2D collider2D)
    {
        fall = false;
    }

    /// <summary>
    /// Detects if there is anything under the triangle
    /// </summary>
    void DetectBelow()
    {
        Vector2 bottom = new Vector2(transform.position.x, transform.position.y - .1f);
        RaycastHit2D hit = Physics2D.Raycast(bottom, Vector2.down, .03f, layerMask);
        Vector3 dis = new Vector3(0, -.005f, 0);

        if (hit.collider == null)
        {
            fall = true;
        }
        else
        {
            BlockCollision(hit.collider);
        }
    }

    /// <summary>
    /// Finds if there is anything coming from above and moves if there is
    /// </summary>
    void DetectAbove()
    {
        Vector2 top = new Vector2(transform.position.x, transform.position.y + .1f);
        RaycastHit2D hit = Physics2D.Raycast(top, Vector2.up, .03f, layerMask);
        Vector2 dis = new Vector3(0, -.005f, 0);

        if (hit.collider != null)
        {
            transform.position = new Vector3(transform.position.x, yplace, transform.position.z);
        }
    }
}
