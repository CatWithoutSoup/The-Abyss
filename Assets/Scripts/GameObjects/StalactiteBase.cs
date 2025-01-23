using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteBase : MonoBehaviour
{
    public GameObject fallingStalactite; // Дочерний объект, который падает
    public float shakeDuration = 1f;    // Длительность тряски перед падением
    public float fallSpeed = 5f;        // Скорость падения
    [SerializeField] private AudioSource fallSource;
    private bool isTriggered = false;  // Проверка, активирован ли триггер

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTriggered && other.CompareTag("Player"))
        {
            isTriggered = true;
            StartCoroutine(ShakeAndFall());
        }
    }

    private IEnumerator ShakeAndFall()
    {
        // Эффект тряски
        fallSource.Play();
        float elapsed = 0f;
        Vector3 originalPosition = fallingStalactite.transform.localPosition;

        while (elapsed < shakeDuration)
        {
            float xOffset = Random.Range(-0.1f, 0.1f);
            fallingStalactite.transform.localPosition = originalPosition + new Vector3(xOffset, 0, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fallingStalactite.transform.localPosition = originalPosition;

        // Активируем гравитацию для падающего сталактита
        Rigidbody2D rb = fallingStalactite.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f; // Включаем гравитацию
        }
    }
}
