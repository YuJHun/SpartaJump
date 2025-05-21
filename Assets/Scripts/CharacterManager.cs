using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //�̱��� �����
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)//�ν��Ͻ��� ��� �ִ���
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();

            }
            return _instance;
        }
    }

    //�ʵ� (Field)
    public Player _player;//�����͸� �����ϴ� ���� ����
    //�Ӽ�(Property) 
    public Player Player//�ܺο��� ������ �� �߰� ���� ����
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        //�ν��Ͻ��� ���������
        {
            //Awake��� �����ֱ� �Լ��� ȣ���� �Ǿ��ٴ°��� �̹� ���ӿ�����Ʈ�� script�� �پ��ִ� ���¿��� ������ �Ǿ��ٴ°� 
            //�׷��� ���� GameObject�� ���� �ʿ䰡 ����
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (_instance != this)//�������ִ� �ν��Ͻ��� �� �ڽ��� �ٸ��� ����� �ı�
            {
                Destroy(gameObject);
            }
        }
    }
}


