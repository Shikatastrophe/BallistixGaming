using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BallSpawner : MonoBehaviour
{
    public GameObject Ball;
    public float SpawnFrequency;
    public GameManager Manager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBall());
        Manager = GameManager.Instance;
    }

    IEnumerator SpawnBall()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnFrequency);
            if (Manager.ballsInGame.Count <= 10 && Manager.canSpawn)
            {
                BallPoolManager.SpawnObject(Ball,transform.position,transform.rotation);
            }
        }
    }
}
