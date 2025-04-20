using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class swordman : MonoBehaviour
{
    public float atk_rad = 5.0f;
    public float k = 1.5f;
    public float atk_dmg;
    Rigidbody2D me;
    private double timer = 0;
    private float x;
    private float y;
    private float a;
    private GameObject Hero;

    void Start()
    {
        me = GetComponent<Rigidbody2D>();
        Hero = GameObject.FindGameObjectWithTag("Player");
        atk_dmg = GetComponent<enemy>().atk_dmg;
    }

    void Update()
    {
        if (me.GetComponent<enemy>().isHsm)
        {
            SLAY();
            timer += Time.deltaTime;
        }
    }
    void SLAY()
    {
        x = me.GetComponent<enemy>().target.x - me.GetComponent<enemy>().Pos.x;
        y = me.GetComponent<enemy>().target.y - me.GetComponent<enemy>().Pos.y;
        if (x * x < atk_rad && y*y<=4 && timer>=1)
        {
            a = Convert.ToSingle(Math.Sqrt(x * x + y * y)); 
            Hero.GetComponent<perso>().TakeDamage(atk_dmg);
            Hero.GetComponent<Rigidbody2D>().AddForce(new Vector2(k * x / a / Time.deltaTime, k * y / a / Time.deltaTime));
            timer = 0;
        }
    }

}
