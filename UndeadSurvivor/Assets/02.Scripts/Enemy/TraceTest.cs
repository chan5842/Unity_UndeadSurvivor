using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceTest : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;

    [SerializeField]
    float moveSpeed = 3f;
    [SerializeField]
    float contactDistance = 0.5f;

    public bool follow = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    void FollowTarget()
    {
        if (Vector2.Distance(transform.position, target.position) > contactDistance && follow)
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        else
            rb.velocity = Vector2.zero;
    }

    void Update()
    {
        FollowTarget();
    }
}
