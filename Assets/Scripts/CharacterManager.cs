using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    //싱글톤 만들기
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)//인스턴스가 비어 있더라도
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();

            }
            return _instance;
        }
    }

    //필드 (Field)
    public Player _player;//데이터를 저장하는 실제 변수
    //속성(Property) 
    public Player Player//외부에서 접근할 때 중간 제어 가능
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
        //인스턴스가 비어있을때
        {
            //Awake라는 생명주기 함수가 호출이 되었다는것은 이미 게임오브젝트로 script에 붙어있는 상태에서 실행이 되었다는것 
            //그래서 따로 GameObject를 만들 필요가 없다
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (_instance != this)//기존에있는 인스턴스와 내 자신이 다르면 현재거 파괴
            {
                Destroy(gameObject);
            }
        }
    }
}


