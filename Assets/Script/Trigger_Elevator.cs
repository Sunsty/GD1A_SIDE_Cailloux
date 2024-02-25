
using UnityEngine;

public class Trigger_Elevator : MonoBehaviour
{
    public Platform_Patrol elevator;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            elevator.enabled = true;
            Destroy(gameObject);
        }
    }
}
