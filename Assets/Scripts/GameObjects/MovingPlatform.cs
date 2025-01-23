using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA; // ������ �����
    public Transform pointB; // ������ �����
    public float speed = 2f; // �������� �������� ���������

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = pointB.position; // ������� ��������� �� ������ �����
    }

    private void Update()
    {
        // ������� ��������� � ������� �������
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ������ �����������, ���� �������� �����
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