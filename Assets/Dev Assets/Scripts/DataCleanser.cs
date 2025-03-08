using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCleanser : MonoBehaviour
{
    private void Start()
    {
        PlayerManager.Instance?.Reset();
    }
}
