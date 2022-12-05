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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // InputSystem�� ����ϹǷ� �ʿ� ������
    //void Update()
    //{
    //    inputVec.x = Input.GetAxisRaw("Horizontal");
    //    inputVec.y = Input.GetAxisRaw("Vertical");
    //}

    private void FixedUpdate()
    {
        //rb.AddForce(inputVec);  // ���������� ���� ����
        //rb.velocity = inputVec; // �ӵ� ����

        // �������� �̵� ���
        // �밢�� �̵��� ����ȭ ������ ������ �̵��ӵ��� �޶����� ������ 
        // �ݵ�� �ʿ��� �۾�
        Vector2 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextVec); // ��ġ �̵�
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);
        // �¿� Ű�� ������ �� ��������Ʈ ���������� Ȯ�� �ϴ� �Լ�
        if (inputVec.x != 0)
            spriter.flipX = inputVec.x < 0;
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}