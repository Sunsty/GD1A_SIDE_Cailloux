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


public class Platform_Patrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;

    private Transform target;
    private int destPoint;
    private bool canMove = true;
    private float timer = 0;
    private bool timerOn = false;
    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
            canMove = false;

            if (timer > 0.5f)
            {
                timerOn = false;
                canMove = true;
                timer = 0f;
            }
        }

        if (canMove)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }

        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            canMove = false;
            timerOn = true;
        }
    }
}
