using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class perso : MonoBehaviour
{
    public float MaxHealth = 10f;
    float currentHealth;
    private float lava_dmg = 1;

    void Start()
    {
        currentHealth = MaxHealth;

    }
    public void TakeDamage(float aboba)
    {
        currentHealth -= aboba;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        print("ÀÉ!");

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            TakeDamage(lava_dmg * Time.deltaTime);
        }
    }
}