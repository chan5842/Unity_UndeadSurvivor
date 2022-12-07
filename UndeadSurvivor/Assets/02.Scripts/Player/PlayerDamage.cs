using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : LivingObject
{
    public Image hpImage;       // 체력 UI

    public AudioClip deathClip; // 사망 소리
    public AudioClip hitClip;   // 피격 소리
    public AudioClip pickUpClip;// 아이템 줍줍 소리

    AudioSource source;
    Animator animator;

    PlayerCtrl playerCtrl;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerCtrl = GetComponent<PlayerCtrl>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        hpImage.gameObject.SetActive(true);
        hpImage.fillAmount = 1f;

        playerCtrl.enabled = true;
    }

    public override void RestoreHp(float newHp)
    {
        base.RestoreHp(newHp);
        hpImage.fillAmount = hp / initHp;
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if (!dead)
            source.PlayOneShot(hitClip);
        base.OnDamage(damage, hitPoint, hitNormal);
        hpImage.fillAmount = hp / initHp;
    }

    public override void Die()
    {
        base.Die();

        hpImage.gameObject.SetActive(false);
        source.PlayOneShot(deathClip);
        animator.SetTrigger("Die");

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
