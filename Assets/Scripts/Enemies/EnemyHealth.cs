using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float currentHealth, maxHealth = 100f;

    public bool HasTakenDamage { get; set; }

    [SerializeField] FloatingHealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void Damage(float damageAmount)
    {
        HasTakenDamage = true;

        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }


}
