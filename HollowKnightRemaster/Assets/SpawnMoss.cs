using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMoss : MonoBehaviour
{
    
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject Moss;
    public bool detected;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (detected && GameObject.FindWithTag("Moss") == null)
        {
            Instantiate(Moss);
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detected = false;
        }
    }


}
