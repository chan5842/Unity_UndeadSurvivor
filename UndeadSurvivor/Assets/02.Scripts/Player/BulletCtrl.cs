using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public enum BulletType
    {
        Normal,     // 총알(적중시 적이 조금 밀려난다)
        Shovel,     // 삽(일정 시간동안 캐릭터 주위를 공전하며 적에게 데미지)
        Fork,       // 삼지창(적중시 적을 많이 밀려나게 한다)
        Sickle      // 낫(위로 발사하여 포물선을 그리며 떨어지며 적중시 적이 조금 밀려난다)
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

        //damage = 10f;       // 게임 매니저에서 데이터를 받아와 데미지로 설정
    }

    private void OnEnable()
    {
        //rb.AddForce(tr.forward * Speed * Time.deltaTime);
        //tr.Translate(tr.forward);
       // StartCoroutine(BulletDeActive);
    }

    private void OnDisable()
    {
        // 비활성화시 초기화
        tr.position = Vector2.zero;
        tr.rotation = Quaternion.identity;
        if(type == BulletType.Normal)
            rb.Sleep(); // 물리 행동 멈춤
    }
    void Update()
    {
        if(type == BulletType.Normal)
            tr.Translate(Vector2.right * Time.deltaTime * Speed);    // 총알 앞 방향으로 계속 나아감
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // 캐릭터 시야로부터 벗어나면 총알 비활성화
        if(col.CompareTag("AREA"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 에너미에게 닿으면 데미지를 주며 즉시 비활성화
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

