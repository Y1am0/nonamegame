using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerStats playerHealth;

    public float healthBonus = 2f;

    private void Awake()
    {
        playerHealth = FindFirstObjectByType<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //if health not max, add health on collision
            if (playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.currentHealth += healthBonus;
                playerHealth.healthBar.UpdateHealthBar(playerHealth.currentHealth, playerHealth.maxHealth);
                Destroy(gameObject); //destroy the health pickup

                Debug.Log("Health Picked Up! Current Health: " + playerHealth.currentHealth);

            }

        }
    }
}
