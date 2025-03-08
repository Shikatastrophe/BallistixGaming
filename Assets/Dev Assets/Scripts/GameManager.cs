using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<int> OnDefeat;
    public static event Action DestroyBalls;
    public static event Action<int,int> UpdateNumber;

    private static GameManager instance;

    public GameObject[] Players;

    public GameObject[] NPCs;

    public bool isAIGame;

    public static GameManager Instance { get { return instance; } }

    public List<GameObject> ballsInGame = new List<GameObject>();

    public List<int> playersInGame = new List<int>();

    public Transform DefaultPosition;
    Transform tMin;
    float minDist;
    public int playerLifes;

    public bool canSpawn;

    public PlayerLives lives = new PlayerLives();

    public GameObject winnerText;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        lives.P1Lives = playerLifes;
        lives.P2Lives = playerLifes;
        lives.P3Lives = playerLifes;
        lives.P4Lives = playerLifes;

        playersInGame.Add(1);
        playersInGame.Add(2);
        playersInGame.Add(3);
        playersInGame.Add(4);
        canSpawn = true;

        if (!isAIGame)
        {
            for(int i = 0; i < PlayerManager.Instance.numberOfPlayers; i++)
            {
                Players[i].SetActive(true);
            }
            for (int i = 0; i < PlayerManager.Instance.numberOfNPCS; i++)
            {
                NPCs[i].SetActive(true);
            }
        }

        UpdateNumber?.Invoke(1, playerLifes);
        UpdateNumber?.Invoke(2, playerLifes);
        UpdateNumber?.Invoke(3, playerLifes);
        UpdateNumber?.Invoke(4, playerLifes);
    }


    public Transform GetClosestBall(Transform pos)
    {
        tMin = null;
        minDist = Mathf.Infinity;

        if(ballsInGame.Count == 0) return DefaultPosition;

        foreach (var item in ballsInGame)
        {
            float dist = Vector3.Distance(item.transform.position, transform.position);
            if (dist < minDist)
            {
                tMin = item.transform;
                minDist = dist;
            }
        }
        return tMin;
    }

    public void LoseLife(int player)
    {
        switch (player)
        {
            //could've cached the value to not keep referencing it but hindsight is 20/20
            case 1:
                if (lives.P1Lives <= 0) return;
                lives.P1Lives--;
                UpdateNumber?.Invoke(player, lives.P1Lives);
                if (lives.P1Lives == 0)
                {
                    OnDefeat?.Invoke(player);
                    TakePlayerOut(player);
                }
                break;
            case 2:
                if (lives.P2Lives <= 0) return;
                lives.P2Lives--;
                UpdateNumber?.Invoke(player, lives.P2Lives);
                if (lives.P2Lives == 0)
                {
                    OnDefeat?.Invoke(player);
                    TakePlayerOut(player);
                }
                break;
            case 3:
                if (lives.P3Lives <= 0) return;
                lives.P3Lives--;
                UpdateNumber?.Invoke(player, lives.P3Lives);
                if (lives.P3Lives == 0)
                {
                    OnDefeat?.Invoke(player);
                    TakePlayerOut(player);
                }
                break;
            case 4:
                if (lives.P4Lives <= 0) return;
                lives.P4Lives--;
                UpdateNumber?.Invoke(player, lives.P4Lives);
                if (lives.P4Lives == 0)
                {
                    OnDefeat?.Invoke(player);
                    TakePlayerOut(player);
                }
                break;
        }
    }


    public void TakePlayerOut(int ply)
    {
        playersInGame.Remove(playersInGame.Find(p => p == ply));
        CheckForWinner();
    }

    public void CheckForWinner()
    {
        if (playersInGame.Count == 1)
        {
            canSpawn = false;
            DestroyBalls?.Invoke();
            winnerText.SetActive(true);
        }
    }
}
