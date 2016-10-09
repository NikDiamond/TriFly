using UnityEngine;
using System.Collections;

public class lightScript : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("1");
        if (coll.gameObject.tag == "Player") {
            Debug.Log("2");
            Color c = GetComponent<SpriteRenderer>().color;
            c.a = 255f;
            GetComponent<SpriteRenderer>().color = c;
        }
    }
}
