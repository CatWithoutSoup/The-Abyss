using UnityEngine;

public class ResettableObject : MonoBehaviour
{
    private Vector3 initialPosition; // Начальная позиция объекта
    private Quaternion initialRotation; // Начальное вращение объекта
    private Rigidbody2D rb;

    private void Start()
    {
        // Сохраняем начальную позицию и вращение
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Если объект имеет Rigidbody2D, сохраняем ссылку
        rb = GetComponent<Rigidbody2D>();
    }

    public void ResetObject()
    {
        // Сбрасываем позицию и вращение
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Если объект имеет Rigidbody2D, сбрасываем его физическое состояние
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f; // Если нужно отключить падение
        }

        // Включаем объект, если он был выключен
        gameObject.SetActive(true);
    }
}