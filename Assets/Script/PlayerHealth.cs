using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthbar;
    public Transform player;

    private Vector2 spawnPoint = Vector2.zero;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        Debug.Log(spawnPoint);

        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(5);
        }

        if(currentHealth <= 0)
        {
            player.position = spawnPoint;
            currentHealth = maxHealth;
            healthbar.SetHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
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
