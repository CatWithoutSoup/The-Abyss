//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class BootsOrb : MonoBehaviour
//{
//    public UnityEvent BootsCollect;
//    private PlayerMovement pm;

//    private void Start()
//    {
//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        if (player != null)
//        {
//            pm = player.GetComponent<PlayerMovement>();
//            BootsCollect.AddListener(pm.UpdateDash);
//        }
//        else
//        {
//            Debug.LogError("Тег Player не найден");
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            BootsCollect.Invoke();
//            Destroy(gameObject);
//        }
//    }

//    public void TestMethod()
//    {
//        print("Boots Collected");
//    }
//}
