using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnabler : MonoBehaviour
{
    public int playerId;
    Animator animator;
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        CharStatus.ActivateWall += ActivateTheWall;
    }

    private void OnDisable()
    {
        CharStatus.ActivateWall -= ActivateTheWall;
    }

    public void ActivateTheWall(int i)
    {
        if (playerId == i)
        {
            animator.SetTrigger("Wall");
        }
    }
}
