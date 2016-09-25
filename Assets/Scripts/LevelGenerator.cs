using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    float speed = 4f,
          distance = 1f;
    [SerializeField]
    int maxStreak;

    [SerializeField]
    GameObject[] enemies;

    [SerializeField]
    GameObject SpawnPointTop;

    [SerializeField]
    GameObject SpawnPointBottom;

    private GameObject enemyPrefab;
    private GameObject lastObject;
    private Vector3 botPos, topPos;
    int botStreak;
    int topStreak;
    void Start () {
        botPos = SpawnPointBottom.transform.position;
        topPos = SpawnPointTop.transform.position;
    }
	
	void Update () {

        //creating enemies
        spawn();
    }

    void spawn()
    {
        int enemy = Random.Range(0, enemies.Length);
        enemyPrefab = enemies[enemy];

        if (Random.value > 0.5)
        {
            if (lastObject == null || Mathf.Abs(lastObject.transform.position.x - botPos.x) >= distance)
                SpawnBottom();
        }
        else
        {
            if (lastObject == null || Mathf.Abs(lastObject.transform.position.x - topPos.x) >= distance)
                SpawnTop();
        }
    }

    void SpawnTop()
    {
        topStreak++;
        botStreak = 0;
        if(topStreak > maxStreak)
        {
            SpawnBottom();
            return;
        }

        lastObject = (GameObject)Instantiate(enemyPrefab, new Vector3(topPos.x, topPos.y, 0f), Quaternion.identity);
        lastObject.GetComponent<Rigidbody2D>().gravityScale = -1 * lastObject.GetComponent<Rigidbody2D>().gravityScale;
        lastObject.GetComponent<Rigidbody2D>().transform.Rotate(new Vector3(0f, 0f, 180));
        lastObject.transform.parent = gameObject.transform;
        lastObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0f, 0f);
    }

    void SpawnBottom()
    {
        topStreak = 0;
        botStreak++;
        if (botStreak > maxStreak)
        {
            SpawnTop();
            return;
        }

        lastObject = (GameObject)Instantiate(enemyPrefab, new Vector3(botPos.x, botPos.y, 0f), Quaternion.identity);
        lastObject.transform.parent = gameObject.transform;
        lastObject.GetComponent<Rigidbody2D>().velocity = new Vector3(-speed, 0f, 0f);
    }
}
