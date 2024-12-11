using Unity.Mathematics;
using UnityEngine;
using Cinemachine;
using System.Linq;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform respawnPoint;
    public GameObject playerPrefab;
    public CinemachineVirtualCameraBase cam;

    private ResettableObject[] resettableObjects;

    private void Awake()
    {
        instance = this;
        resettableObjects = FindObjectsOfType<ResettableObject>();
    }
    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }
    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject player = Instantiate(playerPrefab, respawnPoint.position, quaternion.identity);
        cam.Follow = player.transform;
        ResetAllObjects();
    }
    private void ResetAllObjects()
    {
        foreach (var obj in resettableObjects)
        {
            obj.ResetObject();
        }
    }
}
