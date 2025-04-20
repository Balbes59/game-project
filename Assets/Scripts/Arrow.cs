using System;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float damage;
    float x;
    public float timeToON = 0.3f;
    float timer = 0;
    bool first = true;
    public float baseSpeed = 50.0f;
    public float angle = 0.17f;
    public float timeToDestroy = 5.0f;
    void Start()
    {
        damage = GameObject.FindWithTag("Enemy").GetComponent<enemy>().atk_dmg;
        GetComponent<Collider2D>().enabled = false;
        baseSpeed *= 0.033f;
        Vector2 force = rotateUp(GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().position - GetComponent<Rigidbody2D>().position, angle, baseSpeed);
        GetComponent<Rigidbody2D>().AddForce(force / Time.deltaTime);
        Destroy(gameObject, timeToDestroy);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (GetComponent<Rigidbody2D>().velocity.x < 0) x = 180;
        else x = 0;
        if (GetComponent<Rigidbody2D>().velocity.y != 0) transform.rotation = Quaternion.Euler(0, 0, x + (float)Math.Ceiling((180 / Math.PI) * Math.Atan(GetComponent<Rigidbody2D>().velocity.y/GetComponent<Rigidbody2D>().velocity.x)));
        if (!GetComponent<Collider2D>().enabled && timer >= timeToON && first) {
            first = false;
            GetComponent<Collider2D>().enabled = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("character")){
            collision.gameObject.GetComponent<perso>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Collider2D>().enabled = false;
        }
    }
    Vector2 rotateUp(Vector2 vect, float ang, float len)
    {
        vect.Normalize();
        if (vect.x * vect.x < Mathf.Sin(ang) * Mathf.Sin(ang) && vect.y > 0) return new Vector2(len, 0);
        if (vect.x > 0)
        {
            float deg = Mathf.Acos(vect.y) - ang;
            return new Vector2(Mathf.Sin(deg) * len, Mathf.Cos(deg) * len);
        }
        else
        {
            float deg = Mathf.Acos(vect.y) - ang;
            return new Vector2(-Mathf.Sin(deg) * len, Mathf.Cos(deg) * len);
        }
    }
}
