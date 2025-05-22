using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // ���� �� ����

    private void OnTriggerEnter(Collider other)
    //OnTriggerEnter = Unity���� Ʈ���� �ݶ��̴��� � ��ü�� ����� �� �ڵ����� ȣ��
    {
        // ĳ���Ϳ� Rigidbody�� ���� ��쿡�� ����
        Rigidbody rb = other.GetComponent<Rigidbody>();
        //Ʈ���ſ� ���� ��ü(other)���� Rigidbody ������Ʈ�� �����ɴϴ�
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            // ���� �������� ���� ���� (Y�� ����)
            // ���� ���� �ӵ� ����
            // ����: Rigidbody�� ���� ���� �ӵ�(Y��) �� 0���� �ʱ�ȭ�մϴ�.
            //���� �ӵ��� ���� ������ ���� ���� ���� �Ұ����ϰ� ���̹Ƿ�, �� �ٷ� ������ �ʱ�ȭ�ϴ� �ſ���.
            //���� �ӵ�(XZ��)�� �״�� �����մϴ�.

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //����: Rigidbody�� ���� ����(�� Y��) ���� jumpForce��ŭ�� ���� �ݴϴ�.
            //ForceMode.Impulse�� �������� ū ���� �ִ� ����Դϴ�(������ �� ��� ����).
        }
    }
}
