using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearman : MonoBehaviour
{
    public bool isTired = false;
    public float tiredTime = 2.0f;
    public float atkTime = 4.0f;
    private float timer = 0;
    enemy me;
    public bool isClimbing = false;
    private float damage;
    private float x, y, a;
    public float k = 750.0f;
    private GameObject Hero;
    void Start()
    {
        me = gameObject.GetComponent<enemy>();
        damage = GetComponent<enemy>().atk_dmg;
        Hero = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (me.isHsm == true && isTired == false && timer >= atkTime)
        {
            timer -= atkTime;
            isTired = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else if (isTired == true) {
            if (timer >= tiredTime)
            {
                timer -= tiredTime;
                isTired = false;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall")) isClimbing = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer = 0;
            isTired = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            collision.gameObject.GetComponent<perso>().TakeDamage(damage);
            x = me.GetComponent<enemy>().target.x - me.GetComponent<enemy>().Pos.x;
            y = me.GetComponent<enemy>().target.y - me.GetComponent<enemy>().Pos.y;
            a = Convert.ToSingle(Math.Sqrt(x * x + y * y));
            Hero.GetComponent<Rigidbody2D>().AddForce(new Vector2(k * x / a, k * y / a));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            isClimbing = false;
            gameObject.transform.Find("spear").gameObject.GetComponent<spear>().stayTrue();
        }
    }
}
