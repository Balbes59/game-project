using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : MonoBehaviour
{
    Rigidbody2D obj;

    private bool jump = true;
    private bool spaceold = false;
    private bool napr = true;
    private bool qold = false;
    private float dashtime = 2.0f;
    public float movespeed = 7.0f;
    public float dashdist = 700;
    public float jump_h = 350f;
    public float movespeed_d = 7.0f;
    public float dashdist_d = 700;
    public float jump_h_d = 350f;
    public float movespeed_l = 3.5f;
    public float dashdist_l = 500;
    public float jump_h_l = 230f;
    public float air_frict = 0.1f;
    public float hits = 10;

    private void Awake()
    {
        obj = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
            if (Input.GetKey(KeyCode.Space) && jump && !spaceold)
            {
                spaceold = true;
                jump = false;
                obj.AddForce(new Vector2(0, jump_h));
            }
            if (!Input.GetKey(KeyCode.Space)) { spaceold = false; }
            if (Input.GetKey(KeyCode.A))
            {
                obj.AddForce(new Vector2(-movespeed, 0));
                napr = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                obj.AddForce(new Vector2(movespeed, 0));
                napr = true;
            }
            if (Input.GetKey(KeyCode.Q) && dashtime > 2.0)
            {
                if (!qold)
                {
                    if (napr) obj.AddForce(new Vector2(dashdist, 0));
                    else obj.AddForce(new Vector2(-dashdist, 0));
                    qold = true;
                    dashtime = 0;
                }
            }
            else qold = false;
            dashtime += Time.deltaTime;
            obj.AddForce(new Vector2(-1 * air_frict * obj.velocity.x, -1 * air_frict * obj.velocity.y));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            movespeed = movespeed_l;
            dashdist = dashdist_l;
            jump_h = dashdist_l;
            jump = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            movespeed = movespeed_d;
            dashdist = dashdist_d;
            jump_h = dashdist_d;
            jump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            jump = true;
        }
    }
}