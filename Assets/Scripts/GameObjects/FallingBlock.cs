using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    private float fallDelay = 0.5f; // Задержка перед началом падения
    private float disappearDelay = 0.7f; // Время до исчезновения после падения
    private float fallSpeed = 2f; // Скорость падения
    private bool isFalling = false; // Контроль состояния блока

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, касается ли блок объекта с тегом "Player"
        if (collision.gameObject.CompareTag("Player") && !isFalling)
        {
            StartCoroutine(StartFalling());
        }
    }

    private IEnumerator StartFalling()
    {
        // Дрожание блока перед падением
        Vector3 originalPosition = transform.position;
        float shakeIntensity = 0.1f;
        for (float t = 0; t < fallDelay; t += Time.deltaTime)
        {
            transform.position = originalPosition + (Vector3)(Random.insideUnitCircle * shakeIntensity);
            yield return null;
        }

        transform.position = originalPosition;

        // Включаем падение
        isFalling = true;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = fallSpeed;
        // Ждем перед удалением блока
        yield return new WaitForSeconds(disappearDelay);

        gameObject.SetActive(false);
        rb.gravityScale = 0f;
        collider.isTrigger = false;
    }
}

