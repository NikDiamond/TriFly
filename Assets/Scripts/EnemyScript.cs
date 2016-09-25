using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    float speed;
	void Start () {
        speed = GetComponent<Rigidbody2D>().velocity.x;
	}
	
	void FixedUpdate ()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(speed, GetComponent<Rigidbody2D>().velocity.y, 0f);
    }

    void Update() {
        if (transform.position.y > 30 || transform.position.y < -30) Destroy(gameObject);
    }
}
