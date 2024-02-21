using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;
    public Transform player;

    private bool takeDmg;
    private bool timerOn = false;
    public bool canTakeDmg = true;
    public float timerDmg = 0f;

    private int dmg;
    private Vector2 spawnPoint = Vector2.zero;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (takeDmg && canTakeDmg)
        {

        timerOn = true;
        currentHealth -= dmg;
        healthbar.SetHealth(currentHealth);
        takeDmg = false;

        }

        if (timerOn)
        {
            canTakeDmg = false;
            timerDmg += Time.deltaTime;
            if (timerDmg > 1f)
            {
                canTakeDmg = true;
                timerOn = false;
                timerDmg = 0f;
            }
        }

        if (takeDmg)
        {
            takeDmg = false;
        }

        if (currentHealth <= 0)
        {

        player.position = spawnPoint;
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth);

        }
    }

    public void TakeDamage(int damage)
    {
        dmg = damage;
        takeDmg = true;
    }

    public void DeathZone()
    {
        currentHealth = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            spawnPoint = collision.gameObject.transform.position;
            Destroy(collision.gameObject);
        }
    }
}
