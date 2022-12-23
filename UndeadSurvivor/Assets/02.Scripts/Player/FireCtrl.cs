using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    public Transform firePos;       // �߻� ��ġ
    public Transform target;        // �߻� Ÿ��(����� �����̸� �ڵ����� ���� ��������� ���� �߻���)
    public float attackSpeed;       // ���� �ӵ�(���⸶�� �ٸ�)
    float timer;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (GetComponent<PlayerDamage>().dead)
            return;

        timer += Time.fixedDeltaTime;
        if(timer > attackSpeed) // ���ݼӵ� �������� ���� �߻�
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
