using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectRetainer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
