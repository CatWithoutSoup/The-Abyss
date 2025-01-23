//using UnityEngine;

//public class BootsOrb : MonoBehaviour
//{
//    public int additionalJumps = 1; // Количество дополнительных прыжков, которое даёт объект

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        // Проверяем, игрок ли это
//        if (collision.CompareTag("Player"))
//        {
//            PlayerMovement playerJump = collision.GetComponent<PlayerMovement>();
//            if (playerJump != null)
//            {
//                TestMethod();
//                // Увеличиваем количество доступных прыжков
//                playerJump.AddExtraJumps(additionalJumps);
//            }

//            // Уничтожаем объект после поднятия
//            Destroy(gameObject);
//        }
//    }

//    public void TestMethod()
//    {
//        print("Boots Collected");
//    }
//}
