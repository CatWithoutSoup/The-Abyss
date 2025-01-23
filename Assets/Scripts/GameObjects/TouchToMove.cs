using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToMove : MonoBehaviour
{
    public Transform pointA;        // ������ �����
    public Transform pointB;        // ������ �����
    public float speedToB = 4f;     // �������� �������� � ����� B
    public float speedToA = 2f;     // �������� �������� � ����� A
    public float waitTime = 0.5f;   // ����� �������� ����� ���������
    private Vector3 targetPosition; // ������� ����
    private bool isMoving = false;  // ���� ��������
    private bool playerOnPlatform = false; // ���� ���������� ������ �� ���������
    private float currentSpeed;     // ������� �������� ���������

    private void Start()
    {
        targetPosition = pointB.position; // ��������� ���� � ����� B
        currentSpeed = speedToB;          // ���������� �������� � ����� B
    }

    private void Update()
    {
        if (isMoving)
        {
            // ������� ��������� � ������� �������
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

            // �������� �� ���������� ����� B
            if (targetPosition == pointB.position && Vector3.Distance(transform.position, pointB.position) < 0.1f)
            {
                isMoving = false; // ���������� ��������
                StartCoroutine(ReturnToStartAfterDelay()); // ������ ������ ��� ��������
            }
            // �������� �� ���������� ����� A
            else if (targetPosition == pointA.position && Vector3.Distance(transform.position, pointA.position) < 0.1f)
            {
                isMoving = false; // ���������� ��������
            }
        }
        else if (playerOnPlatform && !isMoving) // ���� ����� �� ��������� � ��� �� ���������
        {
            ActivatePlatform(); // ������������ ���������
        }
    }

    private void ActivatePlatform()
    {
        targetPosition = pointB.position; // ���������� ���� ��� ����� B
        currentSpeed = speedToB;          // ���������� �������� ��� �������� � ����� B
        isMoving = true;                  // ������ ��������
    }

    private IEnumerator ReturnToStartAfterDelay()
    {
        yield return new WaitForSeconds(waitTime); // ��������� 0.5 �������
        targetPosition = pointA.position;          // ���������� ���� ��� ����� A
        currentSpeed = speedToA;                   // ���������� �������� ��� �������� � ����� A
        isMoving = true;                           // ������ �������� �����
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true; // ����� ��������� �� ���������
            collision.transform.SetParent(transform); // �������� �������� ������ � ����������

            if (!isMoving) // ���� ��������� �� ��������
            {
                ActivatePlatform();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true; // ����� ���������� ���������� �� ���������
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false; // ����� ������� ���������
            collision.transform.SetParent(null); // �������� ��������� �� ���������
        }
    }
}