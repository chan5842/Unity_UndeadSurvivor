using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    Rigidbody2D rb;
    Transform tr;
    public float Speed = 10f;
    public float damage;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = transform;

        //damage = 10f;       // ���� �Ŵ������� �����͸� �޾ƿ� �������� ����
    }

    private void OnEnable()
    {
        //rb.AddForce(tr.forward * Speed * Time.deltaTime);
        //tr.Translate(tr.forward);
       // StartCoroutine(BulletDeActive);
    }

    private void OnDisable()
    {
        // ��Ȱ��ȭ�� �ʱ�ȭ
        tr.position = Vector2.zero;
        tr.rotation = Quaternion.identity;
        rb.Sleep(); // ���� �ൿ ����
    }
    void Update()
    {
        tr.Translate(Vector2.right * Time.deltaTime * Speed);    // �Ѿ� �� �������� ��� ���ư�
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // ĳ���� �þ߷κ��� ����� �Ѿ� ��Ȱ��ȭ
        if(col.CompareTag("AREA"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���ʹ̿��� ������ �������� �ָ� ��� ��Ȱ��ȭ
        if(other.CompareTag("ENEMY"))
        {
            other.GetComponent<EnemyAI>().OnDamage(damage, transform.position, transform.position.normalized);
            gameObject.SetActive(false);
        }
    }
}
