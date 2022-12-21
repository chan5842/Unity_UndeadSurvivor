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

        damage = 1f;       // ���� �Ŵ������� �����͸� �޾ƿ� �������� ����
    }

    private void OnEnable()
    {
        //rb.AddForce(tr.forward * Speed * Time.deltaTime);
        //tr.Translate(tr.forward);
       // StartCoroutine(BulletDeActive);
    }

    private void OnDisable()
    {
        tr.position = Vector2.zero;
        tr.rotation = Quaternion.identity;
        rb.Sleep();
    }
    void Update()
    {
        tr.Translate(tr.right * Time.deltaTime * Speed);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // ĳ���� �þ߷κ��� ����� �Ѿ� ��Ȱ��ȭ
        if(col.CompareTag("AREA"))
        {
            gameObject.SetActive(false);
        }
    }
}
