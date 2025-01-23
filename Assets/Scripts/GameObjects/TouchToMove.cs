using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToMove : MonoBehaviour
{
    public Transform pointA;        // Первая точка
    public Transform pointB;        // Вторая точка
    public float speedToB = 4f;     // Скорость движения к точке B
    public float speedToA = 2f;     // Скорость движения к точке A
    public float waitTime = 0.5f;   // Время ожидания перед возвратом
    private Vector3 targetPosition; // Текущая цель
    private bool isMoving = false;  // Флаг движения
    private bool playerOnPlatform = false; // Флаг нахождения игрока на платформе
    private float currentSpeed;     // Текущая скорость платформы

    private void Start()
    {
        targetPosition = pointB.position; // Начальная цель — точка B
        currentSpeed = speedToB;          // Установить скорость к точке B
    }

    private void Update()
    {
        if (isMoving)
        {
            // Двигаем платформу к целевой позиции
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

            // Проверка на достижение точки B
            if (targetPosition == pointB.position && Vector3.Distance(transform.position, pointB.position) < 0.1f)
            {
                isMoving = false; // Остановить движение
                StartCoroutine(ReturnToStartAfterDelay()); // Начать таймер для возврата
            }
            // Проверка на достижение точки A
            else if (targetPosition == pointA.position && Vector3.Distance(transform.position, pointA.position) < 0.1f)
            {
                isMoving = false; // Остановить движение
            }
        }
        else if (playerOnPlatform && !isMoving) // Если игрок на платформе и она не двигается
        {
            ActivatePlatform(); // Активировать платформу
        }
    }

    private void ActivatePlatform()
    {
        targetPosition = pointB.position; // Установить цель как точку B
        currentSpeed = speedToB;          // Установить скорость для движения к точке B
        isMoving = true;                  // Начать движение
    }

    private IEnumerator ReturnToStartAfterDelay()
    {
        yield return new WaitForSeconds(waitTime); // Подождать 0.5 секунды
        targetPosition = pointA.position;          // Установить цель как точку A
        currentSpeed = speedToA;                   // Установить скорость для движения к точке A
        isMoving = true;                           // Начать движение назад
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true; // Игрок находится на платформе
            collision.transform.SetParent(transform); // Персонаж движется вместе с платформой

            if (!isMoving) // Если платформа не движется
            {
                ActivatePlatform();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true; // Игрок продолжает находиться на платформе
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false; // Игрок покинул платформу
            collision.transform.SetParent(null); // Отвязать персонажа от платформы
        }
    }
}