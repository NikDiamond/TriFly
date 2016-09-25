using UnityEngine;
using System.Collections;

public class backScrolling : MonoBehaviour {

    [SerializeField]
    float speed;

	void Update () {
        Vector2 offset = new Vector2(speed/100*Time.time, 0f);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
	}
}
