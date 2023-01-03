using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    GameObject Player;
    public PlayerCtrl playerCtrl;
    public PlayerDamage playerDamage;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        playerCtrl = Player.GetComponent<PlayerCtrl>();
        playerDamage = Player.GetComponent<PlayerDamage>();
    }

    void Update()
    {
        
    }
}
