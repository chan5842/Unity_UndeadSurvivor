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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
