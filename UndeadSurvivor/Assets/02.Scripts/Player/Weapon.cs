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

    public Sprite[] weaponSprite;   // �⺻ ���� 3���� ��������Ʈ
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
