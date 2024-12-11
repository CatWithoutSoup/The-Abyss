using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public PlayerMovement player;
    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            Die();
            //gameObject.SetActive(false);
            //Destroy(gameObject);
            //LevelManager.instance.Respawn();
        }
    }
    private void Die()
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
