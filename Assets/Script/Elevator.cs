using UnityEngine;


/// <summary>
/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// 
/// 
/// - A regler :
/// 
///
/// 
/// 
/// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>


public class Elevator : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    private Transform target;
    private int destPoint;
    private bool canMove = false;
    private float timer = 0;
    private bool timerOn = true;
    private bool elevatorStart = false;
    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {

        if (destPoint > 2)
        {
            elevatorStart = false;
        }

        if (timerOn)
        {
            timer += Time.deltaTime;
            canMove = false;

            if (timer > 1.5f)
            {
                timerOn = false;
                canMove = true;
                timer = 0f;
            }
        }

        if (canMove && elevatorStart)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);
        }

        if (Vector2.Distance(transform.position, target.position) < 0.05f && elevatorStart)
        {
            destPoint++;
            target = waypoints[destPoint];
            canMove = false;
            timerOn = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            elevatorStart = true;
        }
    }
}
