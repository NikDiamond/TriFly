using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    KeyCode jumpKey;
    [SerializeField]
    float rotateSpeed;

    bool flying;
    float rotation;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            GameMaster.GameOver = true;
        }
        if (col.gameObject.tag == "floor")
        {
            flying = false;
        }
    }

    void Update()
    {
        if ((Input.touchCount > 0 || Input.GetKeyDown(jumpKey)) && !flying)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, 0f, rotation), rotateSpeed*10 * Time.deltaTime);
        if (transform.eulerAngles.z > 170) transform.eulerAngles = new Vector3(0f, 0f, 180f);
        if (transform.eulerAngles.z < 10) transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    public void Jump() {
        GetComponent<Rigidbody2D>().gravityScale = GetComponent<Rigidbody2D>().gravityScale * -1;
        flying = true;
        rotation += 180;
        rotation = rotation % 360;
    }

}
