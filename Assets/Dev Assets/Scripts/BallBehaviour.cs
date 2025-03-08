using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public Vector3 velocity;
    public float ballSpeed;

    private void OnEnable()
    {
        GameManager.DestroyBalls += Disintegrate;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.forward * ballSpeed, ForceMode.VelocityChange);
        GameManager.Instance.ballsInGame.Add(gameObject);
        StartCoroutine(Boost());
    }

    private void OnDisable()
    {
        GameManager.DestroyBalls -= Disintegrate;
        GameManager.Instance.ballsInGame.Remove(gameObject);
    }

    void Update()
    {
        velocity = rb.velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        float speed = velocity.magnitude;
        Vector3 direction = Vector3.Reflect(velocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * speed;
    }

    public IEnumerator Boost()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.5f);
            rb.AddForce(transform.forward * ballSpeed, ForceMode.Impulse);
        }
    }

    public void Disintegrate()
    {
        BallPoolManager.ReturnObject(gameObject);
    }
}
