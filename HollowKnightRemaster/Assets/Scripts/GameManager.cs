using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public string transitionedFromScene;
    public Vector2 platformingReSpawnPoint;

    public Vector2 respawnPoint;
    [SerializeField] Bench bench;
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        } else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        bench = FindObjectOfType<Bench>();
    }

    public void RespawnPlayer()
    {
        if(bench != null)
        {
            if (bench.interacted)
            {
                respawnPoint = bench.transform.position;
            }
            else
            {
                respawnPoint = platformingReSpawnPoint;
            }
        }
        else
        {
            respawnPoint = platformingReSpawnPoint;
        }

        PlayerController.instance.transform.position = respawnPoint;
        StartCoroutine(UIManager.Instance.DeActivateDeathScreen());
        PlayerController.instance.Respawned();
    }
 
}
