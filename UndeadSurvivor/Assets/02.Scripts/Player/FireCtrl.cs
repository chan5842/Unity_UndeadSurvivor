using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    Transform firePos;      // 발사 위치
    public Transform target;       // 발사 타겟(모바일 게임이며 자동으로 가장 가까운적을 향해 발사함)
    
    void Start()
    {
        firePos = transform.GetChild(5).transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
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
