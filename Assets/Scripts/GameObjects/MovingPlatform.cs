using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // Первая точка
    public Transform pointB; // Вторая точка
    public float speed = 2f; // Скорость движения платформы

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = pointB.position; // Сначала двигаемся ко второй точке
    }

    private void Update()
    {
        // Двигаем платформу к целевой позиции
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Меняем направление, если достигли точки
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}