using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // 점프 힘 설정

    private void OnTriggerEnter(Collider other)
    //OnTriggerEnter = Unity에서 트리거 콜라이더에 어떤 객체가 닿았을 때 자동으로 호출
    {
        // 캐릭터에 Rigidbody가 있을 경우에만 실행
        Rigidbody rb = other.GetComponent<Rigidbody>();
        //트리거에 닿은 객체(other)에서 Rigidbody 컴포넌트를 가져옵니다
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            // 수직 방향으로 힘을 가함 (Y축 기준)
            // 기존 수직 속도 제거
            // 설명: Rigidbody의 기존 수직 속도(Y축) 를 0으로 초기화합니다.
            //수직 속도가 남아 있으면 점프 힘이 예측 불가능하게 섞이므로, 이 줄로 깨끗이 초기화하는 거예요.
            //수평 속도(XZ축)는 그대로 유지합니다.

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //설명: Rigidbody에 위쪽 방향(↑ Y축) 으로 jumpForce만큼의 힘을 줍니다.
            //ForceMode.Impulse는 순간적인 큰 힘을 주는 방식입니다(점프할 때 사용 적합).
        }
    }
}
