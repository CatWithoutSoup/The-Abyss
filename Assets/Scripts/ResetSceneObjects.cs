using UnityEngine;

public class ResettableObject : MonoBehaviour
{
    private Vector3 initialPosition; // ��������� ������� �������
    private Quaternion initialRotation; // ��������� �������� �������
    private Rigidbody2D rb;

    private void Start()
    {
        // ��������� ��������� ������� � ��������
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // ���� ������ ����� Rigidbody2D, ��������� ������
        rb = GetComponent<Rigidbody2D>();
    }

    public void ResetObject()
    {
        // ���������� ������� � ��������
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // ���� ������ ����� Rigidbody2D, ���������� ��� ���������� ���������
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = 0f; // ���� ����� ��������� �������
        }

        // �������� ������, ���� �� ��� ��������
        gameObject.SetActive(true);
    }
}