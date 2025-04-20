using System;
using UnityEngine;

public class spear : MonoBehaviour
{
    Rigidbody2D Hero;
    GameObject main;
    int x;
    private float damage;
    float timer = 0;
    public float TimeBetweenDamage = 0.75f;
    void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        main = transform.parent.gameObject;
        damage = main.GetComponent<enemy>().atk_dmg;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (! main.GetComponent<spearman>().isTired)
        {
            x = 0;
            if (main.GetComponent<enemy>().getDir()) x = 1;
            if (main.GetComponent<enemy>().isHsm == true) transform.rotation = Quaternion.Euler(0, 0, 180 * (1 - x) + (2 * x - 1) * 57.3f * Mathf.Asin((Hero.position.y - transform.position.y) / (Mathf.Pow(Hero.position.y - transform.position.y, 2) + Mathf.Pow(Hero.position.x - transform.position.x, 2))));
            else transform.rotation = Quaternion.Euler(0, 0, 180 * (1 - x) + (2 * x - 1) * 60);
            if ((! main.GetComponent<spearman>().isClimbing) && timer>=TimeBetweenDamage) GetComponent<Collider2D>().enabled = true;
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            GetComponent<Collider2D>().enabled = false;
            print("бам");
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<perso>().TakeDamage(damage);
            GetComponent<Collider2D>().enabled = false;
            timer = 0;
        }
    }
    public void stayTrue() {
        GetComponent<Collider2D>().enabled = true;
    }
}
