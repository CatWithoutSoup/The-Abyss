using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] private float force = 10f;       // ���� ������
    [SerializeField] private float interval = 1.5f;  // �������� ����� ��������
    private bool isPlayerInside = false;             // ��������, ��������� �� �������� ������ �������
    private Rigidbody2D playerRb;                    // Rigidbody ���������
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
        // ������ �������������� ������
        InvokeRepeating(nameof(PushPlayer), 0f, interval);
    }

    private void PushPlayer()
    {
        // ���� �������� ��������� ������ �������
        if (isPlayerInside && playerRb != null)
        {
            geyserSound.Play();
            Instantiate(geyserParticle, particleSpawn.position, Quaternion.identity);
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0);
            // ���������� ����������� (���� �������� �������)
            Vector2 pushDirection = new Vector2(0, 1); // ������ �� �����������

            // ��������� ���� ������
            playerRb.AddForce(pushDirection * force, ForceMode2D.Impulse);
        }
    }
}
