using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehaviour : MonoBehaviour
{
    public bool isHorizontal;
    public float NPCSpeed;
    Rigidbody rb;
    float modifier;
    Vector3 direction;
    public float NPCWaitTime;
    Transform target;
    public bool canMove;
    public GameManager Manager;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(GetClosestBall());
        if (isHorizontal) StartCoroutine(GetRangeX());
        else StartCoroutine(GetRangeZ());
        modifier = 0;
        canMove = true;
        //for some reason caching this var makes the game explode?
        Manager = GameManager.Instance;
    }
    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        switch (isHorizontal)
        {
            case true:
                direction = new Vector3(modifier * NPCSpeed, rb.velocity.y, rb.velocity.z);
                rb.velocity = direction;
                break;
            case false:
                direction = new Vector3(rb.velocity.x, rb.velocity.y, modifier * NPCSpeed);
                rb.velocity = direction;
                break;
        }
    }
    
    IEnumerator GetRangeX()
    {
        while (true)
        {
            yield return new WaitForSeconds(NPCWaitTime);
            if (target.position == Vector3.zero) modifier = 0;
            else if (target.position.x < transform.position.x) modifier = -1;
            else modifier = 1;
            Debug.Log(modifier);
        }
    }

    IEnumerator GetRangeZ()
    {
        while (true)
        {
            yield return new WaitForSeconds(NPCWaitTime);
            if (target.position == Vector3.zero) modifier = 0;
            else if (target.position.z < transform.position.z) modifier = -1;
            else modifier = 1;
            Debug.Log(modifier);
        }
    }


    IEnumerator GetClosestBall()
    {
        while (true)
        {
            target = GameManager.Instance.GetClosestBall(transform);
            yield return new WaitForSeconds(NPCWaitTime/2);
        }
    }
}
