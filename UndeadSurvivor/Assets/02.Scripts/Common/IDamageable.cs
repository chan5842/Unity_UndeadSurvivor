using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 살아 움직이는 모든 오브젝트는 데미지를 받는다
// 따라서 인터페이스로 선언하여 사용한다.
public interface IDamageable
{
    void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal);
}

