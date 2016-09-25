using UnityEngine;
using System.Collections;

public class BestScore : MonoBehaviour {
    
	void Start () {
        GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("score", 0).ToString();
    }
}
