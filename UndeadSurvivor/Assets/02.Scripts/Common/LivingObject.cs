using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// ��� ����ִ� ������Ʈ�� ��ӹ��� ��ũ��Ʈ
public class LivingObject : MonoBehaviour, IDamageable
{
    public float initHp;                             // ���۽� ü��
    public float hp { get; protected set; }          // ���� ü��
    public bool dead { get; protected set; }         // ��� ����
    public event Action onDeath;                     // ��� �� �ߵ��� �̺�Ʈ

    protected virtual void OnEnable()
    {
        dead = false;
        hp = initHp;
    }

    // �ǰ�
    public virtual void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        hp -= damage;

        if(hp <= 0 && !dead)
        {
            Die();
        }
    }

    // ���
    public virtual void Die()
    {
        // �̺�Ʈ�� ��ϵ� �Լ��� �ִٸ� ����
        if(onDeath != null)
        {
            onDeath();
        }
        dead = true;
    }

    // ü�� ȸ��
    public virtual void RestoreHp(float newHp)
    {
        if (dead)
            return;

        hp += newHp;
    }

}
