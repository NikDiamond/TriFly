using UnityEngine;
using System.Collections;

public class backScrolling : MonoBehaviour {

    [SerializeField]
    float speed;

	void Update () {
        float newOffset = speed * Time.time / 100 % 1.0f;
        Vector2 offset = new Vector2( newOffset, 0f );
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
