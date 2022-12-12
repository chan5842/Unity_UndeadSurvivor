using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : LivingObject
{
    [SerializeField]
    LivingObject target;                    // 에너미가 추적할 플레이어 오브젝트
    [SerializeField]
    Transform targetTr;                     // 플레이어 오브젝트 트랜스폼

    public ParticleSystem hitEffect;        // 피격 이펙트
    public AudioClip deathSound;            // 사망소리
    public AudioClip hitSound;              // 피격 소리

    Animator animator;
    AudioSource source;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    // 스크립터블 오브젝트로 데이터 받아오기
    public float damage = 20f;              // 공격력
    public float attackSpeed = 0.5f;        // 공격 간격
    float lastAttackTime;                   // 마지막으로 공격한 시간
    float moveSpeed;                        // 적 이동속도

    void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        // 플레이어를 찾음
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingObject>();
        targetTr = target.GetComponent<Transform>();
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

    // 최초 활성화 되었을 때 가질 값
    public void Setup(float newHp, float newDamage, float newSpeed, Sprite newSprite)
    {
        initHp = newHp;
        hp = initHp;
        damage = newDamage;
        moveSpeed = newSpeed;
        spriteRenderer.sprite = newSprite;
    }

    private void Start()
    {
        //StartCoroutine(UpdatePath());
    }

    private void Update()
    {
        if(rb.velocity.x != 0f)        
            spriteRenderer.flipX = rb.velocity.x < 0;

        //MoveToPlayer();
    }
    void FixedUpdate()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (dead)
            return;
        if (hasTarget)
        {
            if (Vector2.Distance(transform.position, targetTr.position) > 0)
                transform.position =
                    Vector2.MoveTowards(transform.position, targetTr.position, moveSpeed * Time.fixedDeltaTime);
        }
        //else
        //    rb.velocity = Vector2.zero;
    }
}
