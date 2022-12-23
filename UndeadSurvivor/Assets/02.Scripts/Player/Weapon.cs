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
    public SpriteRenderer[] weaponRenderer;

    void Start()
    {
        
    }

    
    void Update()
    {
        //switch(curWeapon)
        //{
        //    case WeaponType.AK47:
        //        weaponRenderer[0].enabled = true;
        //        break;
        //}
    }
}
