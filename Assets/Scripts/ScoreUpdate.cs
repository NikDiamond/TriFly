using UnityEngine;
using System.Collections;

public class ScoreUpdate : MonoBehaviour {
    
	void Update () {
        GetComponent<UnityEngine.UI.Text>().text = GameMaster.Score.ToString();
	}

}
