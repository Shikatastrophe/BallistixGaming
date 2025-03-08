using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharStatus : MonoBehaviour
{
    public static event Action<int> ActivateWall;

    public int player;
    public Animator animatorChar;
    public Animator animatorShield;
    public bool isAI;
    public MovementHandler movementHandler;
    public IABehaviour IA;
    public List<ParticleSystem> particleSystems;
    public GameObject explosion;
    public GameObject mech;

    private void OnEnable()
    {
        GameManager.OnDefeat += Die;
    }

    private void OnDisable()
    {
        GameManager.OnDefeat -= Die;
    }

    void Die(int ply)
    {
        if (ply == player)
        {
            foreach (ParticleSystem p in particleSystems)
            {
                p.Stop();
            }
            StartCoroutine(Explosion());
            animatorChar.SetTrigger("Die");
            //Tbh, had I planned this better, I could've used inheritance to not have to do this, but hindsight is 20/20
            if (isAI)
            {
                IA.canMove = false;
            }
            else
            {
                movementHandler.canMove = false;
            }
        }
    }

    public IEnumerator Explosion()
    {
        animatorShield.SetTrigger("Death");
        yield return new WaitForSeconds(1);
        explosion.SetActive(true);
        mech.SetActive(false);
        yield return new WaitForSeconds(1);
        ActivateWall?.Invoke(player);
    }
}
