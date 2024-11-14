using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerStats playerStats;
    public int damage = 1;


    private void OnCollisionEnter2D(Collision2D collision)
    {

        print("Collision Detected");
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.TakeDamage(damage);
        }
    }
}
