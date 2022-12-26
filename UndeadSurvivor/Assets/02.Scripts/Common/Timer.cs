using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;      // 타이머 UI
    float startTime;            // 타이머 시작 시간(0분 0초)
    public float curTime;              // 현재 시간
    public float maxTime = 300f;       // 최대 시간(5분)
    int min;                    // 분
    int sec;                    // 초
    bool isEnded;               // 타이머 종료 여부

    PlayerDamage playerDamage;

    private void Awake()
    {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();
    }

    void OnEnable()
    {
        ResetTimer();           // 시작시 Reset시킴
    }

    void FixedUpdate()          // 고정된 프레임으로 시간을 흐르게 함
    {
        if (isEnded)            // 타이머가 종료됬다면 실행하지 않음
            return;
        if (playerDamage.dead)
            return;
        CheckTimer();           // 시간 체크
    }
    void CheckTimer()
    {
        curTime = Time.time - startTime;    // 현재 시간 구하기
        min = (int)curTime / 60;            // 분
        sec = (int)curTime % 60;            // 초
        if(curTime < maxTime)               // 최대 시간에 도달하지 않았다면
        {
            // 타이머 텍스트를 분:초 로 변경
            timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        }
        // 시간이 다 되었다면
        else if(!isEnded)
        {
            // 타이머 종료 함수 실행
            EndTimer();
        }
    }

    void EndTimer()
    {
        curTime = maxTime;  // 현재 시간 = 최대 시간
        min = (int)curTime / 60;
        sec = (int)curTime % 60;
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        isEnded = true;     // 타이머를 종료 상태로 변경
    }

    void ResetTimer()
    {
        startTime = Time.time;
        curTime = 0f;       // 현재 시간 초기화
        min = 0;
        sec = 0;
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        isEnded = false;    // 타이머 종료되지 않은 상태로 변경
    }
}
