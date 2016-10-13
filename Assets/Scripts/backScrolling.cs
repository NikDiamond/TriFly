using UnityEngine;
using System.Collections;

public class backScrolling : MonoBehaviour {

    [SerializeField]
    float speed;
    [SerializeField]
    bool x = false;

    void Update () {
        float newOffset = speed * Time.time / 100 % 1.0f;

        Vector2 offset;
        if (!x)
            offset = new Vector2( newOffset, 0f );
        else
            offset = new Vector2( 0f, newOffset );

        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
