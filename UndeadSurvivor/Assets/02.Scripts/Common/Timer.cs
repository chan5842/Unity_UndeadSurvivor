using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;      // Ÿ�̸� UI
    float startTime;            // Ÿ�̸� ���� �ð�(0�� 0��)
    public float curTime;              // ���� �ð�
    public float maxTime = 300f;       // �ִ� �ð�(5��)
    int min;                    // ��
    int sec;                    // ��
    bool isEnded;               // Ÿ�̸� ���� ����

    PlayerDamage playerDamage;

    private void Awake()
    {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();
    }

    void OnEnable()
    {
        ResetTimer();           // ���۽� Reset��Ŵ
    }

    void FixedUpdate()          // ������ ���������� �ð��� �帣�� ��
    {
        if (isEnded)            // Ÿ�̸Ӱ� �����ٸ� �������� ����
            return;
        if (playerDamage.dead)
            return;
        CheckTimer();           // �ð� üũ
    }
    void CheckTimer()
    {
        curTime = Time.time - startTime;    // ���� �ð� ���ϱ�
        min = (int)curTime / 60;            // ��
        sec = (int)curTime % 60;            // ��
        if(curTime < maxTime)               // �ִ� �ð��� �������� �ʾҴٸ�
        {
            // Ÿ�̸� �ؽ�Ʈ�� ��:�� �� ����
            timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        }
        // �ð��� �� �Ǿ��ٸ�
        else if(!isEnded)
        {
            // Ÿ�̸� ���� �Լ� ����
            EndTimer();
        }
    }

    void EndTimer()
    {
        curTime = maxTime;  // ���� �ð� = �ִ� �ð�
        min = (int)curTime / 60;
        sec = (int)curTime % 60;
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        isEnded = true;     // Ÿ�̸Ӹ� ���� ���·� ����
    }

    void ResetTimer()
    {
        startTime = Time.time;
        curTime = 0f;       // ���� �ð� �ʱ�ȭ
        min = 0;
        sec = 0;
        timerText.text = min.ToString("00") + ":" + sec.ToString("00");
        isEnded = false;    // Ÿ�̸� ������� ���� ���·� ����
    }
}
