using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public int numberOfPlayers;
    public int numberOfNPCS;
    public static PlayerManager Instance {  get { return instance; } }

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

        Reset();
    }

    public void UpdatePlayers()
    {
        numberOfNPCS--;
        numberOfPlayers++;
    }

    public void Reset()
    {
        numberOfNPCS = 4;
        numberOfPlayers = 0;
    }
}
