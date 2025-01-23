using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalactiteFall : MonoBehaviour
{
    public bool becomesPlatform = false; // Если true, сталактит становится платформой
    public GameObject platform;         // Объект платформы (если задано)
    public GameObject breakEffect;      // Эффект разрушения (если сталактит ломается)
    private PlayerMovement player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (becomesPlatform && platform != null)
            {
                platform.SetActive(true); // Включаем платформу
                Destroy(gameObject);      // Удаляем сталактит
            }
            else
            {
                if (breakEffect != null)
                {
                    Instantiate(breakEffect, transform.position, Quaternion.identity);
                }
                Destroy(gameObject); // Удаляем сталактит
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Логика убийства игрока
            Debug.Log("Player Killed");
            Die(); // Временная логика удаления игрока
        }

    }
    public void Die()
    {
        if (player != null)
        {
            player.enabled = false;
        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = false;

        StartCoroutine(ReloadScene(0.5f));
    }
    private IEnumerator ReloadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //gameObject.SetActive(true);
    }
}
