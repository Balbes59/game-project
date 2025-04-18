using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeratack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public Animator animator;
    public int sword_damage = 2;
    
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetKey(KeyCode.E))
            {
                Attack();
                nextAttackTime = Time.time + 1f/attackRate;
            }
        }
        
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(sword_damage);
        }   
    }
    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
