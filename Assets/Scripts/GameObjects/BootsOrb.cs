//using UnityEngine;

//public class BootsOrb : MonoBehaviour
//{
//    public int additionalJumps = 1; // ���������� �������������� �������, ������� ��� ������

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        // ���������, ����� �� ���
//        if (collision.CompareTag("Player"))
//        {
//            PlayerMovement playerJump = collision.GetComponent<PlayerMovement>();
//            if (playerJump != null)
//            {
//                TestMethod();
//                // ����������� ���������� ��������� �������
//                playerJump.AddExtraJumps(additionalJumps);
//            }

//            // ���������� ������ ����� ��������
//            Destroy(gameObject);
//        }
//    }

//    public void TestMethod()
//    {
//        print("Boots Collected");
//    }
//}
