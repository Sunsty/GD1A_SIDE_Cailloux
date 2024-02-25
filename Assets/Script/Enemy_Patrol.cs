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


public class Enemy_Patrol : MonoBehaviour
{
    private bool canWalk = true;

    public float speed;
    public Transform[] waypoints;

    public int damageOnCollision = 20;

    public SpriteRenderer graphics;

    private Transform target;
    private int destPoint;
    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        if (canWalk)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if(Vector2.Distance(transform.position, target.position) < 0.3f)
            {
                destPoint = (destPoint + 1) % waypoints.Length;
                target = waypoints[destPoint];
                graphics.flipX = !graphics.flipX;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damageOnCollision);
        }
    }

    public void StopWalk()
    {
        canWalk = false;
    }
}
