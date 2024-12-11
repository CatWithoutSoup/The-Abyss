using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsTrigger : MonoBehaviour
{
    [Header("Текст Подсказки")]
    [TextArea(3, 10)]
    [SerializeField] private string message;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TipsManager.displayTipEvent?.Invoke(message);

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TipsManager.disableTipEvent?.Invoke();
        }
    }
}
