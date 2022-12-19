using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : LivingObject
{
    [SerializeField]
    LivingObject target;                    // 에너미가 추적할 플레이어 오브젝트
    Vector2 moveDir;                        // 에너미가 추적할 캐릭터의 방향

    //public ParticleSystem hitEffect;      // 피격 이펙트
    public ParticleSystem deadEffect;       // 사망 이펙트
    public AudioClip deathSound;            // 사망소리
    public AudioClip hitSound;              // 피격 소리
    public EnemyData enemyData;             // 에너미 데이터(이름, 공격력, 체력, 이동속도, 공격속도)

    Animator animator;
    AudioSource source;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    

    // 스크립터블 오브젝트로 데이터 받아오기
    public float damage = 20f;              // 공격력
    public float attackSpeed = 0.5f;        // 공격 간격
    float lastAttackTime;                  // 마지막으로 공격한 시간
    [SerializeField]
    float moveSpeed = 3f;                        // 적 이동속도

    void Awake()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        deadEffect.Stop();
    }
     protected override void OnEnable()
    {
        base.OnEnable();
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
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if (dead)
            return;
        if (hasTarget)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > 0)
            {
                moveDir = (target.transform.position - transform.position).normalized;
                Vector2 nextVec = moveDir * moveSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + nextVec);
                // 물리 속도가 이동에 영향을 주지 않도록 추가
                rb.velocity = Vector2.zero;
            }    
        }
        else
        {
            moveDir = Vector2.zero;
        }
    }

    private void LateUpdate()
    {
        if (moveDir.x != 0f)         // 캐릭터 방향에 따라 flip결정
            spriteRenderer.flipX = moveDir.x < 0;
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if(!dead) // 죽지 않았을때 데미지를 입으면
        {
            //// 공격받은 지점과 방향으로 파티클 효과 재생
            //hitEffect.transform.position = hitPoint;
            //hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //hitEffect.Play();

            // 피격음 재생
            source.PlayOneShot(hitSound);

            StartCoroutine(Hit());
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    IEnumerator Hit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = Color.white;
    }

    public override void Die()
    {
        base.Die();

        // 다른 AI를 방해하지 않도록 모든 콜라이더 비활성화
        Collider2D[] enemyColliders = GetComponents<Collider2D>();
        foreach(var col in enemyColliders)
        {
            col.enabled = false;
        }

        // 이동을 멈추고 Die애니메이션 실행 및 사망 소리 재생
        moveDir = Vector2.zero;
        animator.SetTrigger("Die");
        source.PlayOneShot(deathSound);
        deadEffect.Play();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!dead && Time.time >= lastAttackTime + attackSpeed)
        {
            LivingObject attackTarget = other.GetComponent<LivingObject>();

            if(attackTarget != null && attackTarget == target)
            {
                lastAttackTime = Time.time;

                // 상대방의 피격 위치와 피격 방향을 근삿값으로 계산
                Vector2 hitPoint = other.ClosestPoint(transform.position);
                Vector2 hitNormal = transform.position - other.transform.position;

                // 공격
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
           
        }
    }
}
