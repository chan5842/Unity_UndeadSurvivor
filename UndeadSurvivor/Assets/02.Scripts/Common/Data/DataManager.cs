using System.Collections;
using System.Collections.Generic;

namespace DataInfo
{
    [System.Serializable]       // ����ȭ(�ν����� â���� ������ �ʴ°��� �� �� �ְ� ��)
    public class SpawnData
    {
        public int spriteType;  // ���� ����
        public float spawnTime; // ���� ����
        public int hp;          // ������ ü��
        public float moveSpeed; // ������ �̵��ӵ�
        public int damage;      // ������ ���ݷ�
    }

    [System.Serializable]
    public class Item
    {
        public enum ItemType   // ������ ����
        {
            EXP,
            HEAL,
            MAGNET
        }

        public int ID;          // ������ȣ
        public ItemType type;   // ����
        public string name;     // �̸�        
        public float value;     // ��(����ġ +1, ���׳� ����, �� ȸ���� ��)

    }
}
