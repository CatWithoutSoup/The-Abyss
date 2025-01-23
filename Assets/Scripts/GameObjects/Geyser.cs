using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private float force = 10f;       // Сила толчка
    [SerializeField] private float interval = 1.5f;  // Интервал между толчками
    private bool isPlayerInside = false;             // Проверка, находится ли персонаж внутри гейзера
    private Rigidbody2D playerRb;                    // Rigidbody персонажа
    [SerializeField] private AudioSource geyserSound;
    [SerializeField] private GameObject geyserParticle;
    [SerializeField] private Transform particleSpawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            playerRb = collision.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            playerRb = null;
        }
    }

    private void Start()
    {
        // Запуск периодического толчка
        InvokeRepeating(nameof(PushPlayer), 0f, interval);
    }

    private void PushPlayer()
    {
        // Если персонаж находится внутри гейзера
        if (isPlayerInside && playerRb != null)
        {
            geyserSound.Play();
            Instantiate(geyserParticle, particleSpawn.position, Quaternion.identity);
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            // Определяем направление (куда персонаж смотрит)
            Vector2 pushDirection = new Vector2(0, 1); // Толчок по горизонтали

            // Применяем силу толчка
            playerRb.AddForce(pushDirection * force, ForceMode2D.Impulse);
        }
    }
}
