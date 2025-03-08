using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerManager : MonoBehaviour
{
    public Animator[] Animators;
    int index;
    private void Start()
    {
        index = 0;
        //PlayerManager.Instance.Reset();
    }
    public void ActivatePlayer()
    {
        Animators[index].SetTrigger("Select");
        PlayerManager.Instance.UpdatePlayers();
        index++;
    }
}
