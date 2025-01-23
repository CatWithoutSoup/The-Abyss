using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrbDash : MonoBehaviour
{
    public UnityEvent OrbCollect;
    private PlayerMovement pm;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Collider2D Collider2D;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            pm = player.GetComponent<PlayerMovement>();
            OrbCollect.AddListener(pm.UpdateDash);
        }
        else
        {
            Debug.LogError("Тег Player не найден");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && pm.airDashesRemaining != 1)
        {
            OrbCollect.Invoke();
            StartCoroutine(RespawnOrb());
            //gameObject.SetActive(false); 
        }
    }
    private IEnumerator RespawnOrb()
    {
        SpriteRenderer.enabled = false;
        Collider2D.enabled = false;
        yield return new WaitForSeconds(5f);
        SpriteRenderer.enabled = true;
        Collider2D.enabled = true;
    }
    public void TestMethod()
    {
        print("Orb Collected");
    }
}
