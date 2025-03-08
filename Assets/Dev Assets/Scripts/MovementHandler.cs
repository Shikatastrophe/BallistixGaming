using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    public int playerId;
    float playerHorizontal, playerVertical;
    bool canShield;
    //bool movesHorizontally;
    Rigidbody rb;
    public bool canMove;

    [Header("Speed and Cooldown Variables")]
    public float speed;

    private void OnEnable()
    {
        InputHandler.OnMove += Move;
        //InputHandler.OnAbility += Ability;
    }

    private void OnDisable()
    {
        InputHandler.OnMove -= Move;
        //InputHandler.OnAbility -= Ability;
    }

    void Start()
    {
        canShield = true;
        rb = GetComponent<Rigidbody>();
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        Vector3 direction = new Vector3(playerHorizontal * speed, rb.velocity.y, playerVertical * speed);
        //rb.AddForce(direction,ForceMode.Force);
        rb.velocity = direction;
    }

    void Move(int idHandler, float inputHorizontal, float inputVertical)
    {
        if (idHandler == playerId)
        {
            playerHorizontal = inputHorizontal;
            playerVertical = inputVertical;
        }
    }

    //No time to implement unfortunately
    void Ability(int idHandler)
    {
        if(canShield && canMove)
        {

        }
    }
}
