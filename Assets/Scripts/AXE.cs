using System;
using UnityEngine;

public class AXE : MonoBehaviour
{
    float dist;
    float dmgTimer = 0;
    float timeUntillDmg = 0.5f;
    float damage;
    public float k = 150;
    float napr;
    float time;
    void Start()
    {
        dist = Mathf.Sqrt(transform.localPosition.x * transform.localPosition.x + transform.localPosition.y * transform.localPosition.y);
        if (GetComponentInParent<enemy>()) damage = GetComponentInParent<enemy>().atk_dmg;
        else if (GetComponentInParent<BOSS>()) damage = GetComponentInParent<BOSS>().atk_dmg;
    }

    void Update()
    {
        dmgTimer += Time.deltaTime;
        if (dmgTimer>=timeUntillDmg) GetComponent<Collider2D>().enabled = true;
    }
    public void rotateTo(float angle) {
        transform.rotation = Quaternion.Euler(0, 0, 90-angle);
        transform.localPosition = new Vector3(dist*Mathf.Cos(angle / 57.4f), dist*Mathf.Sin(angle / 57.4f), 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        GetComponent<Collider2D>().enabled = false;
        dmgTimer = 0;
    }
    public void SLAY()
    {
        Vector2 target = GameObject.FindGameObjectWithTag("Player").GetComponent<perso>().transform.position;
        float x = target.x - transform.position.x;
        float y = target.y - transform.position.y;
        float a = Convert.ToSingle(Math.Sqrt(x * x + y * y));
        GameObject.FindGameObjectWithTag("Player").GetComponent<perso>().TakeDamage(damage);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().AddForce(new Vector2(k * x / a, k * y / a));
    }
}
