using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {
    /// <summary>
    /// A reference to the player GameObect.
    /// Used for tracking position and orientation.
    /// </summary>
    GameObject player;

    /// <summary>
    /// A reference to a bullet GameObject.
    /// The bullet will be instantiated when the Fire button is pressed.
    /// </summary>
    public GameObject bullet;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) Destroy (gameObject);
        if (player.GetComponent<SpriteRenderer>().flipX) {
            transform.position = player.transform.position - new Vector3(.5f, 0, -1);
            transform.localScale = new Vector3(-1, 1, 1);
        }else {
            transform.position = player.transform.position + new Vector3(.5f, 0, 1);
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetButtonDown("Fire1")) {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }
}
