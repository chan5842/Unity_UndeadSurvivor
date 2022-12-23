using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public Transform firePos;       // 발사 위치
    public Transform target;        // 발사 타겟(모바일 게임이며 자동으로 가장 가까운적을 향해 발사함)
    public float attackSpeed;       // 공격 속도(무기마다 다름)
    float timer;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (GetComponent<PlayerDamage>().dead)
            return;

        timer += Time.fixedDeltaTime;
        if(timer > attackSpeed) // 공격속도 간격으로 총을 발사
        {
            timer = 0f;
            Fire();
        }
            

    }

    public void Fire()
    {
        GameObject _bullet = GameManager.instance.GetBullet();
        if (_bullet != null)
        {
            _bullet.transform.position = firePos.position;
            _bullet.transform.rotation = firePos.rotation;
            _bullet.SetActive(true);
        }
    }
}
