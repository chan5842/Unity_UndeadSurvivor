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

    public Sprite[] weaponSprite;   // �⺻ ���� 3���� ��������Ʈ
    public SpriteRenderer weaponRenderer;


    void Start()
    { 
    
    }

    
    void Update()
    {
        // ���� ���콺 ���⿡ �°� ȸ��
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;
        // �������� �ѱ��� ���Ұ�� flipY
        if (Mathf.Abs(transform.rotation.z) > 0.6f)
            weaponRenderer.flipY = true;
        else
            weaponRenderer.flipY = false;
    }
}
