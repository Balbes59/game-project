using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class bowman : MonoBehaviour
{
    Rigidbody2D me;
    Rigidbody2D Hero;
    private float timer = 0;
    public float aimTime = 0.5f;
    public float shotCD = 2.5f;
    private bool isAbleToShot = false;
    public float shotDistance = 13.0f;
    public float k = 0.1f;
    private bool isAbleToStop = false;
    public GameObject arrow;
    void Start()
    {
        me = GetComponent<Rigidbody2D>();
        Hero = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MOVING();
        if (me.GetComponent<enemy>().isHsm && Vector2.Distance(me.position, Hero.position) < shotDistance)
        {
            STOP();
            timer += Time.deltaTime;
            if (isAbleToShot && timer >= aimTime)
            {
                timer -= aimTime;
                isAbleToShot = false;
                SHOT();
            }
            else if (!isAbleToShot && timer >= shotCD) {
                timer -= shotCD;
                isAbleToShot = true;
            }
        }
        else if (!isAbleToShot) {
            timer += Time.deltaTime;
            if (timer >= shotCD) isAbleToShot = true;
        }
    }

    void STOP() {
        if (isAbleToStop) { GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition; }
    }
    void MOVING() {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void SHOT() {
        Instantiate(arrow, transform.position, transform.rotation);
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        isAbleToStop = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isAbleToStop = false;
    }
}