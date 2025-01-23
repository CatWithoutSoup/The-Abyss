using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteBase : MonoBehaviour
{
    public GameObject fallingStalactite; // �������� ������, ������� ������
    public float shakeDuration = 1f;    // ������������ ������ ����� ��������
    public float fallSpeed = 5f;        // �������� �������
    [SerializeField] private AudioSource fallSource;
    private bool isTriggered = false;  // ��������, ����������� �� �������

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
        // ������ ������
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

        // ���������� ���������� ��� ��������� ����������
        Rigidbody2D rb = fallingStalactite.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1f; // �������� ����������
        }
    }
}
