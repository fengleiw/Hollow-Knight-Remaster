using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Worm : EnemyController
{
    [SerializeField] GameObject DetectedField;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject worm;
    float timer;
    public bool detected;
    public bool rightSide;
    public float wormSpeed = 10f;
    public static Worm Instance;
    protected override void Start()
    {
        base.Start();
        ChangeState(EnemyStates.Worm_Appear);
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void UpdateEnemyState()
    {
        /*if(health <= 0)
        {
            Death(0.5f);
        }*/
        switch (GetCurrentEnemyState)
        {
            case EnemyStates.Worm_Appear:
                ChangeState(EnemyStates.Worm_Charger);
                break;

            case EnemyStates.Worm_Charger:
                rb.velocity = new Vector2(wormSpeed, 0);

                break;

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
