using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExp : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    public bool isCollect;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(Collect());
    }

    IEnumerator Collect()
    {
        while(true)
        {
            yield return 1f;
            if (isCollect)
                transform.position = Vector3.Lerp(transform.position, Player.transform.position, 0.05f);            
        }
    }
    void Update()
    {
        
    }
}
