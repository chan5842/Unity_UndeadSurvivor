using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Data", menuName = "Scriptable/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;    // 이름
    public float damage;        // 공격력
    public float hp;            // 체력
    public float speed;         // 이동속도
    public float attackSpeed;   // 공격속도(주기)
    public string comment;      // 설명
}
