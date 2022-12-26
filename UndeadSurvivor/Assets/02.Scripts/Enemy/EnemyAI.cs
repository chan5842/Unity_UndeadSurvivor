using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : LivingObject
{
    [SerializeField]
    LivingObject target;                    // 에너미가 추적할 플레이어 오브젝트
    Vector2 moveDir;                        // 에너미가 추적할 캐릭터의 방향

    public ParticleSystem deadEffect;       // 사망 이펙트
    public AudioClip deathSound;            // 사망소리
    public AudioClip hitSound;              // 피격 소리

    // 컴포넌트
    Animator animator;
    AudioSource source;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public RuntimeAnimatorController[] animCon; // 애니메이터 지정


    // 데이터 받아오기
    public float damage;              // 공격력
    public float attackSpeed = 0.5f;  // 공격 간격
    public float lastAttackTime;      // 마지막으로 공격한 시간
    public float moveSpeed;           // 적 이동속도


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
        target = GameManager.instance.playerCtrl.GetComponent<LivingObject>();
        dead = false;
        hp = maxHP;
    }
    public void Init(SpawnData data)
    {
        // 애니메이터 지정
        animator.runtimeAnimatorController =  animCon[data.spriteType];
        moveSpeed = data.moveSpeed;
        maxHP = data.hp;
        hp = maxHP;
        damage = data.damage;
    }

    private void OnDisable()
    {
        // 모든 콜라이더를 검색하여 활성화(사망시 비활성화 되었기 때문에 다시 활성화)
        Collider2D[] enemyColliders = GetComponents<Collider2D>();
        foreach (var col in enemyColliders)
        {
            col.enabled = true;
        }
        // 오브젝트 풀링으로 비활성화 되었기 때문에 오브젝트 위치를 초기화
        transform.position = Vector2.zero;
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

    //// 최초 활성화 되었을 때 가질 값
    //public void Setup(float newHp, float newDamage, float newSpeed, Sprite newSprite)
    //{
    //    base.maxHP = newHp;
    //    hp = base.maxHP;
    //    damage = newDamage;
    //    moveSpeed = newSpeed;
    //    spriteRenderer.sprite = newSprite;
    //}

    private void Start()
    {
    }

    void FixedUpdate()
    {
        MoveToPlayer();
    }

    // 플레이어를 추적하는 함수
    void MoveToPlayer()
    {
        if (dead)   // 죽었다면 실행 X
            return;
        if (hasTarget)  // 타겟이 있다면(플레이어가 죽지 않았다면)
        {
            // 플레이어와의 거리가 0보다 크다면
            if (Vector2.Distance(transform.position, target.transform.position) > 0)
            {
                moveDir = (target.transform.position - transform.position).normalized; // 방향
                Vector2 nextVec = moveDir * moveSpeed * Time.fixedDeltaTime;           // 이동 벡터
                rb.MovePosition(rb.position + nextVec);                                // 이동
                rb.velocity = Vector2.zero;     // 물리 속도가 이동에 영향을 주지 않도록 추가
            }    
        }
        else // 타겟이 없다면(플레이어가 죽었다면)
        {
            moveDir = Vector2.zero;     // 이동 방향은 0f;
            rb.velocity = Vector2.zero; // 이동 속도 값도 0f;
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
            //animator.SetTrigger("Hit");
            StartCoroutine(Hit());
        }
        base.OnDamage(damage, hitPoint, hitNormal);
    }

    IEnumerator Hit()
    {
        // 0.5초 빨개졌다 원래 색으로 돌아옴
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
        animator.SetBool("Dead", true);
        source.PlayOneShot(deathSound);
        deadEffect.Play();

        UIManager.instance.InKillCount();

        StartCoroutine(EnemyDie());       
    }

    IEnumerator EnemyDie()
    {
        // 2초후 에너미 오브젝트 비활성화
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        // 죽지 않았고 공격간격마다 데미지
        if (!dead && Time.time >= lastAttackTime + attackSpeed)
        {
            LivingObject attackTarget = col.collider.GetComponent<LivingObject>();

            if (attackTarget != null && attackTarget == target)
            {
                lastAttackTime = Time.time;

                // 상대방의 피격 위치와 피격 방향을 근삿값으로 계산
                Vector2 hitPoint = col.collider.ClosestPoint(transform.position);
                Vector2 hitNormal = transform.position - col.collider.transform.position;

                // 공격
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }

        }
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if(!dead && Time.time >= lastAttackTime + attackSpeed)
    //    {
    //        LivingObject attackTarget = other.GetComponent<LivingObject>();

    //        if(attackTarget != null && attackTarget == target)
    //        {
    //            lastAttackTime = Time.time;

    //            // 상대방의 피격 위치와 피격 방향을 근삿값으로 계산
    //            Vector2 hitPoint = other.ClosestPoint(transform.position);
    //            Vector2 hitNormal = transform.position - other.transform.position;

    //            // 공격
    //            attackTarget.OnDamage(damage, hitPoint, hitNormal);
    //        }
           
    //    }
    //}
}
