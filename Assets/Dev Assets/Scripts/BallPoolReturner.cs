using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPoolReturner : MonoBehaviour
{
    public int BallLayer;
    public int Player;
    GameManager manager;


    public void Start()
    {
        manager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == BallLayer)
        {
            BallPoolManager.ReturnObject(other.gameObject);
            manager.LoseLife(Player);
        }
    }
}
