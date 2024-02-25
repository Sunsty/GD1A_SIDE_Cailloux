using System.Threading;
using UnityEngine;

public class WeakSpot : MonoBehaviour
{
    public GameObject objectToDestroy;
    public Enemy_Patrol graphics;
    public Animator animator;
    public Collider2D hitBox;

    private float timer = 0f;
    private bool timerOn = false;
    private void Update()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
            if (timer > 2f)
            {
                Destroy(objectToDestroy);
                timerOn = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            graphics.StopWalk();
            timerOn = true;
            animator.SetBool("isDead", true);
            hitBox.enabled = false;
        }
    }
}
