using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

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


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;
    public Transform player;
    public SpriteRenderer sprite;

    private bool takeDmg;
    private bool timerOn = false;
    public bool canTakeDmg = true;
    public float timerDmg = 0f;

    public bool timerDeathOn = false;
    public float timerDeath = 0f;
    public Image blackScreen;
    public bool timerScreenBackOn = false;
    public float timerScreenBack = 0f;

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
            if (timerDmg%0.25f >= 0.12f)
            {
                sprite.color = new Color(255, 255, 255, 0);
            }
            else
            {
                sprite.color = new Color(255, 255, 255, 255);
            }
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

        if (currentHealth <= 0 && !timerDeathOn)
        {
            timerDeathOn = true;
            timerDeath = -1f;
            GetComponent<PlayerMovement>().Die();
        }

        if (timerDeathOn)
        {
            timerDeath += Time.deltaTime;

            blackScreen.color = new Color(0, 0, 0, timerDeath*2);

            if (timerDeath > 0.5f)
            {
                timerDeathOn = false;
                timerDeath = 0f;
                player.position = spawnPoint;
                currentHealth = maxHealth;
                healthbar.SetHealth(currentHealth);
                timerScreenBackOn = true;
            }
        }

        if(timerScreenBackOn)
        {
            timerScreenBack += Time.deltaTime;

            blackScreen.color = new Color(0, 0, 0, 1f - timerScreenBack);

            if (timerScreenBack > 1f)
            {
                timerScreenBackOn = false;
                timerScreenBack = 0f;
                blackScreen.color = new Color(0, 0, 0, 0);
                GetComponent<PlayerMovement>().Live();
            }

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
