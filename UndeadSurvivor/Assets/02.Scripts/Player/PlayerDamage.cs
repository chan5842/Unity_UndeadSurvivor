using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : LivingObject
{
    public Image hpImage;       // 체력 UI

    public AudioClip deathClip; // 사망 소리
    public AudioClip[] hitClip; // 피격 소리 배열
    public AudioClip pickUpClip;// 아이템 줍줍 소리

    public ParticleSystem destroyEffect; // 사망시 터지는 이펙트

    Collider2D col;

    AudioSource source;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public GameObject Weapon;

    PlayerCtrl playerCtrl;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        playerCtrl = GetComponent<PlayerCtrl>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        hpImage.gameObject.SetActive(true);
        hpImage.fillAmount = 1f;

        destroyEffect.Stop();
        playerCtrl.enabled = true;
    }

    public override void RestoreHp(float newHp)
    {
        base.RestoreHp(newHp);
        hpImage.fillAmount = hp / initHp;
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if (!dead)  // 죽지 않았다면 데미지를 받을 때 2개 중 하나의 피격 소리 재생
            source.PlayOneShot(hitClip[Random.Range(0,1)]);
    
        base.OnDamage(damage, hitPoint, hitNormal);
        hpImage.fillAmount = hp / initHp;
        StartCoroutine(OnHit());
        
    }

    IEnumerator OnHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.color = Color.white;
    }

    public override void Die()
    {
        base.Die();

        col.enabled = false;
        hpImage.gameObject.SetActive(false);
        Weapon.SetActive(false);
        source.PlayOneShot(deathClip);
        animator.SetTrigger("Dead");
        destroyEffect.Play();

        playerCtrl.enabled = false;
    }
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {   
            // item획득 코드
            //IItem item = other.GetComponent<IItem>();

            //if (item != null)
            //{
            //    item.Use(gameObject);
            //    source.PlayOneShot(itemPickupClip);
            //}
        }
    }
}
