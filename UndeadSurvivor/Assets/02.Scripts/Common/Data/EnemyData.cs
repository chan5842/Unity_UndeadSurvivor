using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;    // �̸�
    public float damage;        // ���ݷ�
    public float hp;            // ü��
    public float speed;         // �̵��ӵ�
    public float attackSpeed;   // ���ݼӵ�(�ֱ�)
    public string comment;      // ����
}
