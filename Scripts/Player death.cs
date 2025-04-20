using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdeath : MonoBehaviour
{
    public int MaxHealth = 10;
    int currentHealth;

    void Start()
    {
        currentHealth = MaxHealth;

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
}
