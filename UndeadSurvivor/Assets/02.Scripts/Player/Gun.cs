using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum GunType
    {
        AK47,
        M4,
        SHOTGUN
    }

    public GunType curWeapon = GunType.AK47;

    public Sprite[] weaponSprite;   // 기본 무기 3가지 스프라이트
    public SpriteRenderer weaponRenderer;


    void Start()
    { 
    
    }

    
    void Update()
    {
        // 총을 마우스 방향에 맞게 회전
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        // 왼쪽으로 총구가 향할경우 flipY
        if (Mathf.Abs(transform.rotation.z) > 0.6f)
            weaponRenderer.flipY = true;
        else
            weaponRenderer.flipY = false;
    }
}
