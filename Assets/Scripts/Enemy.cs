using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy : MonoBehaviour
{
    public Vector2 target;
    public Vector2 Pos;
    public float base_speed = 0.8f;
    private GameObject Hero;
    Rigidbody2D me;
    private float delta = 0;
    private bool napr = true;
    public bool isHsm = false;
    private bool hjp = true;
    private bool jump = false;
    public float air_frict = 1.1f;
    public float hsmDistance = 10.0f;
    public float visionK = 1.5f;
    public bool ableToJump = true;
    public float atk_dmg = 5.0f;
    public float patrol_dist = 300;
    private float timer;
    public float waitTime = 3;
    float speed;

    private void Start()
    { 
        Hero = GameObject.FindGameObjectWithTag("Player");
        me = GetComponent<Rigidbody2D>();
        speed = base_speed;
    }
    void Update()
    {
        timer += Time.deltaTime;
        target = Hero.transform.position;
        Pos = transform.position;
        if (isHsm)
        {
            if (target.x > Pos.x)
            {
                me.AddForce(new Vector2(speed , 0));
                napr = true;
            }
            if (target.x < Pos.x)
            {
                me.AddForce(new Vector2(-speed , 0));
                napr = false;
            }
            if ((target.x - Pos.x) > hsmDistance * visionK || (target.x - Pos.x) < -hsmDistance * visionK) isHsm = false;
            if (ableToJump && jump && hjp && target.y > Pos.y + 0.2f)
            {
                me.AddForce(new Vector2(0, 0.5f / Time.deltaTime));
                jump = false;
                hjp = false;
            }
            else hjp = target.y - 0.2f <= Pos.y;
        }
        else
        {
            if (napr)
            {
                hjp = true;
                if (Pos.x < target.x && Pos.x - target.x > -hsmDistance) isHsm = true;
                else if (delta < patrol_dist)
                {
                    if (timer > waitTime) me.AddForce(new Vector2(0.4f * speed , 0));
                    else me.AddForce(new Vector2(-19 * air_frict * me.velocity.x, -1 * air_frict * me.velocity.y));
                }
                else
                {
                    napr = false;
                    timer = 0;
                }
            }
            else
            {
                hjp = true;
                if (Pos.x > target.x && Pos.x - target.x < hsmDistance) isHsm = true;
                else if (delta > -patrol_dist)
                {
                    if (timer > waitTime) me.AddForce(new Vector2(-0.4f * speed , 0));
                    else me.AddForce(new Vector2(-19 * air_frict * me.velocity.x, -1 * air_frict * me.velocity.y));
                }
                else
                {
                    napr = true;
                    timer = 0;
                }
            }
        }
        delta += me.velocity.x;
        me.AddForce(new Vector2(-1 * air_frict * me.velocity.x, -1 * air_frict * me.velocity.y));
        if (napr) transform.rotation = Quaternion.Euler(0, 0, 0);
        else transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            me.AddForce(new Vector2( 0 ,  20 ));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jump = true;
            speed = base_speed;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            jump = true;
            speed = base_speed * 0.67f;
        }
    }
    public bool getDir() {
        return napr;
    }
}