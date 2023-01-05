using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCtrl : MonoBehaviour
{
    public Vector2 inputVec;
    Rigidbody2D rb;
    SpriteRenderer spriter;
    Animator animator;
    public float moveSpeed;
    [SerializeField]
    Collider2D[] cols;

    [SerializeField]
    LayerMask expLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        expLayer = LayerMask.NameToLayer("EXP");
    }

    // InputSystem을 사용하므로 필요 없어짐
    //void Update()
    //{
    //    inputVec.x = Input.GetAxisRaw("Horizontal");
    //    inputVec.y = Input.GetAxisRaw("Vertical");
    //}

    private void FixedUpdate()
    {
        //rb.AddForce(inputVec);  // 물리적으로 힘을 가함
        //rb.velocity = inputVec; // 속도 제어

        // 물리적인 이동 방법
        // 대각선 이동시 정규화 해주지 않으면 이동속도가 달라지기 떄문에 
        // 반드시 필요한 작업
        Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec); // 위치 이동
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);
        // 좌우 키를 눌렀을 때 스프라이트 뒤집을건지 확인 하는 함수
        if (inputVec.x != 0)
            spriter.flipX = inputVec.x < 0;
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    public void MagnetExp()
    {
        int layerMask = 1 << expLayer;
        cols = Physics2D.OverlapCircleAll(transform.position, 100f, layerMask);
        foreach (var col in cols)
        {
            col.gameObject.GetComponent<ItemExp>().isCollect = true;
        }
    }
}
