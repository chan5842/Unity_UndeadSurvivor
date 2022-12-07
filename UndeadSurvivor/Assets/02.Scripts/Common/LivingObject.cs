using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 모든 살아있는 오브젝트가 상속받을 스크립트
public class LivingObject : MonoBehaviour, IDamageable
{
    public float initHp;                             // 시작시 체력
    public float hp { get; protected set; }          // 현재 체력
    public bool dead { get; protected set; }         // 사망 상태
    public event Action onDeath;                     // 사망 시 발동할 이벤트

    protected virtual void OnEnable()
    {
        dead = false;
        hp = initHp;
    }

    // 피격
    public virtual void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        hp -= damage;

        if(hp <= 0 && !dead)
        {
            Die();
        }
    }

    // 사망
    public virtual void Die()
    {
        // 이벤트에 등록된 함수가 있다면 실행
        if(onDeath != null)
        {
            onDeath();
        }
        dead = true;
    }

    // 체력 회복
    public virtual void RestoreHp(float newHp)
    {
        if (dead)
            return;

        hp += newHp;
    }

}
