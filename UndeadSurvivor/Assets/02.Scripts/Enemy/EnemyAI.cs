using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    LivingObject target;                    // 에너미가 추적할 플레이어 오브젝트

    public ParticleSystem hitEffect;        // 피격 이펙트
    public AudioClip deathSound;            // 사망소리
    public AudioClip hitSound;              // 피격 소리

    Animator animator;
    AudioSource source;
    SpriteRenderer spriteRenderer;

    // 스크립터블 오브젝트로 데이터 받아오기
    public float damage = 20f;              // 공격력
    public float attackSpeed = 0.5f;        // 공격 간격
    float lastAttackTime;                   // 마지막으로 공격한 시간

    private void OnEnable()
    {
        // 플레이어를 찾음
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingObject>();
    }
    bool hasTarget  // 타겟이 존재하는지 확인할 프로퍼티
    {
        get
        {
            if (target != null && !target.dead)
            {
                return true;
            }
            return false;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
