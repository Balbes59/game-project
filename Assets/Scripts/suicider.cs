using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class suicider : MonoBehaviour
{
    public float BOOM_rad = 25.0f;
    Rigidbody2D me;
    private bool suicide = false;
    private double timer = 0;
    private float x;
    private float y;

    void Start()
    {
        me = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (me.GetComponent<enemy>().isHsm) {
            x = me.GetComponent<enemy>().target.x - me.GetComponent<enemy>().Pos.x;
            y = me.GetComponent<enemy>().target.y - me.GetComponent<enemy>().Pos.y;
            if (x*x+y*y< BOOM_rad / 4)
            {
                suicide = true;
            }
        }
        if (suicide) {
            timer += Time.deltaTime;
            if (timer >= 1) BOOM();
        }
    }
    void BOOM() {
        x = me.GetComponent<enemy>().target.x - me.GetComponent<enemy>().Pos.x;
        y = me.GetComponent<enemy>().target.y - me.GetComponent<enemy>().Pos.y;
        if (x * x + y * y < BOOM_rad) Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(gameObject);
    }

}
