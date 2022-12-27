using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataInfo;

public class PlayerDamage : LivingObject
{
    // UI
    public Image hpImage;       // 체력 UI

    // 이펙트
    public AudioClip deathClip; // 사망 소리
    public AudioClip[] hitClip; // 피격 소리 배열
    public AudioClip pickUpClip;// 아이템 줍줍 소리
    public ParticleSystem destroyEffect; // 사망시 터지는 이펙트

    Collider2D col;

    // 컴포넌트
    AudioSource source;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public GameObject Weapon;

    public int character_Level = 1;
    public int EXP = 0;
    public int[] needExp = { 0, 5, 10, 18, 29, 43, 60, 80, 103, 129, 158, 190 };
    public Image EXPImg;
    public Text LevelText;
    public AudioClip LevelUpClip;

    // 스크립트
    PlayerCtrl playerCtrl;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        playerCtrl = GetComponent<PlayerCtrl>();

        EXPImg.fillAmount = EXP / needExp[character_Level];
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
        base.RestoreHp(newHp * maxHP);  // 체력을 회복할때는 퍼센트 수치로 회복
        hpImage.fillAmount = hp / maxHP;
    }

    public void GainExp(int newExp)
    {
        EXP += newExp;
        // 필요 경험치를 초과했다면
        if(EXP >= needExp[character_Level])
        {
            int overExp = EXP - needExp[character_Level];   // 초과된 경험치 계산
            character_Level++;
            LevelText.text = "Level. " + character_Level;
            EXP = overExp;  // 현재 경험치는 초과된 경험치로 변경
            source.PlayOneShot(LevelUpClip);
        }

        EXPImg.fillAmount = (float)EXP / (float)needExp[character_Level];
    }

    public override void OnDamage(float damage, Vector2 hitPoint, Vector2 hitNormal)
    {
        if (!dead)  // 죽지 않았다면 데미지를 받을 때 2개 중 하나의 피격 소리 재생
            source.PlayOneShot(hitClip[Random.Range(0,1)]);
    
        base.OnDamage(damage, hitPoint, hitNormal);
        hpImage.fillAmount = hp / maxHP;
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
        if(Input.GetKeyDown(KeyCode.R))
        {
            hp -= 10;
            hpImage.fillAmount = hp / maxHP;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (dead)
            return;
        //if (other.CompareTag("ITEM"))
        //{
        //    Debug.Log("아이템 획득");
        //    //item획득 코드
        //    Item item = other.GetComponent<ItemData>().itemInfo;

        //    if (item != null)
        //    {
        //        Use(item);
        //        //source.PlayOneShot(itemPickupClip);
        //    }
        //    //Destroy(other.gameObject);
        //}
    }

    public void Use(Item item)
    {   
        switch (item.type)
        {
            case Item.ItemType.EXP:
                EXP += Mathf.FloorToInt(item.value);
                EXPImg.fillAmount = EXP / needExp[character_Level];
                break;
            case Item.ItemType.HEAL:
                RestoreHp(item.value);
                break;

            case Item.ItemType.MAGNET:
                // 자석 아이템 효과 발동(주변 반경 X m내의 모든 경험치를 빨아들임)
                break;
        }
        
    }
}
