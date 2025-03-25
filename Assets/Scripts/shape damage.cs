using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shapedamage : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public float attackRate = 0.01f;
    float nextAttackTime = 0f;
    public int sword_damage = 2;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.anyKeyDown)
            {
                Attack1();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }
    void Attack1()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(sword_damage);
        }
    }
}
