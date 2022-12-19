using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : LivingObject
{
    [SerializeField]
    LivingObject target;                    // ���ʹ̰� ������ �÷��̾� ������Ʈ
    Vector2 moveDir;                        // ���ʹ̰� ������ ĳ������ ����

    //public ParticleSystem hitEffect;      // �ǰ� ����Ʈ
    public ParticleSystem deadEffect;       // ��� ����Ʈ
    public AudioClip deathSound;            // ����Ҹ�
    public AudioClip hitSound;              // �ǰ� �Ҹ�
    public EnemyData enemyData;             // ���ʹ� ������(�̸�, ���ݷ�, ü��, �̵��ӵ�, ���ݼӵ�)

    Animator animator;
    AudioSource source;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    

    // ��ũ���ͺ� ������Ʈ�� ������ �޾ƿ���
    public float damage = 20f;              // ���ݷ�
    public float attackSpeed = 0.5f;        // ���� ����
    float lastAttackTime;                  // ���������� ������ �ð�
    [SerializeField]
    float moveSpeed = 3f;                        // �� �̵��ӵ�

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
        // �÷��̾ ã��
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<LivingObject>();
    }
    bool hasTarget  // Ÿ���� �����ϴ��� Ȯ���� ������Ƽ
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

    // ���� Ȱ��ȭ �Ǿ��� �� ���� ��
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
                // ���� �ӵ��� �̵��� ������ ���� �ʵ��� �߰�
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
        if (moveDir.x != 0f)         // ĳ���� ���⿡ ���� flip����
            spriteRenderer.flipX = moveDir.x < 0;
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if(!dead) // ���� �ʾ����� �������� ������
        {
            //// ���ݹ��� ������ �������� ��ƼŬ ȿ�� ���
            //hitEffect.transform.position = hitPoint;
            //hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
            //hitEffect.Play();

            // �ǰ��� ���
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

        // �ٸ� AI�� �������� �ʵ��� ��� �ݶ��̴� ��Ȱ��ȭ
        Collider2D[] enemyColliders = GetComponents<Collider2D>();
        foreach(var col in enemyColliders)
        {
            col.enabled = false;
        }

        // �̵��� ���߰� Die�ִϸ��̼� ���� �� ��� �Ҹ� ���
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

                // ������ �ǰ� ��ġ�� �ǰ� ������ �ٻ����� ���
                Vector2 hitPoint = other.ClosestPoint(transform.position);
                Vector2 hitNormal = transform.position - other.transform.position;

                // ����
                attackTarget.OnDamage(damage, hitPoint, hitNormal);
            }
           
        }
    }
}
