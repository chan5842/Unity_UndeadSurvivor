using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public enum BulletType
    {
        Normal,     // �Ѿ�(���߽� ���� ���� �з�����)
        Shovel,     // ��(���� �ð����� ĳ���� ������ �����ϸ� ������ ������)
        Fork,       // ����â(���߽� ���� ���� �з����� �Ѵ�)
        Sickle      // ��(���� �߻��Ͽ� �������� �׸��� �������� ���߽� ���� ���� �з�����)
    };

    Rigidbody2D rb;
    Transform tr;
    public float Speed = 10f;
    public float damage;
    public BulletType type;

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
        if(type == BulletType.Normal)
            rb.Sleep(); // ���� �ൿ ����
    }
    void Update()
    {
        if(type == BulletType.Normal)
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
            if(type == BulletType.Normal)
                gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ENEMY"))
        {
            col.gameObject.GetComponent<EnemyAI>().OnDamage(damage, transform.position, transform.position.normalized);
            //gameObject.SetActive(false);
        }
    }
}

