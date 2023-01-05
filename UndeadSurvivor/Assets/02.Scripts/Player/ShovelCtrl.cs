using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelCtrl : MonoBehaviour
{
    public float coolTime = 5f;
    float curTime;
    float startTime;
    bool isEnded = true;

    void Start()
    {
        
    }

    //void Update()
    //{
    //    if (isEnded)
    //        return;
    //    StartCoroutine(CheckCoolTime());
    //}

    //IEnumerator CheckCoolTime()
    //{
    //    curTime = Time.time - startTime;
    //    if(curTime < coolTime)
    //    {

    //    }
    //}
}
