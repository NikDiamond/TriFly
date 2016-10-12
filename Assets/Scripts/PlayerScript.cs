using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    [SerializeField]
    KeyCode jumpKey;
    [SerializeField]
    float rotateSpeed;
    [SerializeField]
    bool godMode = false;
    [SerializeField]
    GameObject top_light;
    [SerializeField]
    GameObject bot_light;

    bool flying;
    float rotation;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "enemy")
        {
            if (godMode) return;
            GameMaster.GameOver = true;
        }
        if (col.gameObject.tag == "floor")
        {
            flying = false;
        }
    }

    void Update()
    {
        if (Input.touchCount > 0 || Input.GetKeyDown(jumpKey))
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

    public void Jump()
    {
        if (flying) return;
        GetComponent<Rigidbody2D>().gravityScale = GetComponent<Rigidbody2D>().gravityScale * -1;

        Color full = top_light.gameObject.GetComponent<SpriteRenderer>().color;
        full.a = 255f;
        Color trans = full;
        trans.a = 0f;

        if (GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            top_light.gameObject.GetComponent<SpriteRenderer>().color = full;
            bot_light.gameObject.GetComponent<SpriteRenderer>().color = trans;
        }
        else
        {

            top_light.gameObject.GetComponent<SpriteRenderer>().color = trans;
            bot_light.gameObject.GetComponent<SpriteRenderer>().color = full;
        }

        flying = true;
        rotation += 180;
        rotation = rotation % 360;
    }

}
