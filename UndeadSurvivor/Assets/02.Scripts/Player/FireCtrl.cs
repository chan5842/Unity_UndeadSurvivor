using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    Transform firePos;      // 발사 위치
    
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
        //GameObject _bullet = GameManager.gameManager.GetBullet();
        //if (_bullet != null)
        //{
        //    _bullet.transform.position = firePos.position;
        //    //_bullet.transform.rotation = firePos.rotation;
        //    _bullet.SetActive(true);
        //}
    }
}
