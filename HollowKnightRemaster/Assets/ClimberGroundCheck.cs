using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimberGroundCheck : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            
        }
    }
}
