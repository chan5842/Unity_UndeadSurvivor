using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        AK47,
        M4,
        SHOTGUN
    }

    public WeaponType curWeapon = WeaponType.AK47;

    public Sprite[] weaponSprite;   // 기본 무기 3가지 스프라이트
    public SpriteRenderer weaponRenderer;


    void Start()
    { 
    
    }

    
    void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        if (Mathf.Abs(transform.rotation.z) > 0.6f)
            weaponRenderer.flipY = true;
        else
            weaponRenderer.flipY = false;
    }
}
