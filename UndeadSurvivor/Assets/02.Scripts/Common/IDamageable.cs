using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �����̴� ��� ������Ʈ�� �������� �޴´�
// ���� �������̽��� �����Ͽ� ����Ѵ�.
public interface IDamageable
{
    void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal);
}

