using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private Transform attackTransform; // Attack point
    [SerializeField] private float attackRange = 1.5f; // Attack range
    [SerializeField] private LayerMask attackableLayer; // LayerMask for enemies
    [SerializeField] private float damageAmount = 10f; // Attack damage

    public bool ShouldBeDamaging { get; private set; } = false;

    private List<IDamageable> iDamageables = new List<IDamageable>();

    private RaycastHit2D[] hits;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Attack input
        if (UserInput.instance.controls.Attack.Attack.WasPressedThisFrame())
        {
            Attack();
            anim.SetTrigger("attack");
        }
    }

    private void Attack()
    {
        //circle "hitbox" for attack
        hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer);

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

            if (iDamageable != null)
            {
                iDamageable.Damage(damageAmount);
            }
            else
            {

            }
        }
    }

    /*
    //coroutine that allows controlling when we can damage enemies
    public IEnumerator DamageWhileSlashIsActive()
    {
        ShouldBeDamaging = true;

        while (ShouldBeDamaging)
        {
            //this runs for as many frames as shouldBeDamaging is true
            hits = Physics2D.CircleCastAll(attackTransform.position, attackRange, transform.right, 0f, attackableLayer); //circle "hitbox" for attack

            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                if (iDamageable != null && !iDamageable.HasTakenDamage)
                {
                    iDamageable.Damage(damageAmount);
                    iDamageables.Add(iDamageable);
                }

            }

            yield return null; //this waits for one more frame, game freezes if this is not here

        }

        ReturnAttackablesToDamageable();

    }

    private void ReturnAttackablesToDamageable()
    {
        foreach (IDamageable damagedEntity in iDamageables)
        {
            damagedEntity.HasTakenDamage = false;
        }

        iDamageables.Clear();
    }

    */

    //hitbox visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTransform.position, attackRange);
    }

    #region Animation Triggers  
    public void ShouldBeDamagingToTrue()
    {
        ShouldBeDamaging = true;
    }

    public void ShouldBeDamagingToFalse()
    {
        ShouldBeDamaging = false;
    }

    #endregion
}