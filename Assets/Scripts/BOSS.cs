using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BOSS : MonoBehaviour
{
    float air_frict = 0.15f;
    public float atk_dmg = 7;
    GameObject weapon;
    float timer = 0;
    float timeBetweenAttacks = 1;
    public bool attacking = false;
    float atk_num = 0;
    GameObject Hero;
    public float distToDash = 400;
    public float timeUntillAttackInDash = 0.5f;
    public float rageDuration = 4;
    public float jumpH = 400;
    public float speed = 5;
    public bool isActive = false;
    float mass;
    float x;
    void Start()
    {
        weapon = transform.GetChild(0).gameObject;
        Hero = GameObject.FindGameObjectWithTag("Player");
        mass = GetComponent<Rigidbody2D>().mass;
    }

    void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenAttacks && !attacking)
            {
                attacking = true;
                timer = 0;
                atk_num = UnityEngine.Random.Range(1, 2);
                print(atk_num);
            }
            if (Vector2.Distance(Hero.transform.position, transform.position) > distToDash)
            {
                atk_num = 3;
            }
            if (attacking)
            {
                switch (atk_num)
                {
                    case 1:
                        Attack1(timer);
                        break;
                    case 2:
                        Attack1(timer);
                        break;
                    case 3:
                        Attack1(timer);
                        break;
                }
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * air_frict * GetComponent<Rigidbody2D>().velocity.x, -1 * air_frict * GetComponent<Rigidbody2D>().velocity.y));
        }
    }
    void turnAXE(float angle) {
        weapon.GetComponent<AXE>().rotateTo(angle);
    }
    void Attack1(float time) {
        float napr = ((time / 0.5f) - (time / 0.5f) % 1) % 2;
        turnAXE(180 * napr + 2 * (time % 0.5f) * 180 * (1 - 2 * napr));
        chasing();
        if (time >= rageDuration)
        {
            attacking = false;
            timer = 0;
        }
    }
    void Attack2(float time) { }
    void Dash(float time) { }
    public void activate() { isActive = true; }
    void chasing() {
        x = -1;
        if (Hero.transform.position.x > transform.position.x) x = 1;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(speed * GetComponent<Rigidbody2D>().mass * x, 0));
        transform.rotation = Quaternion.Euler(0, 180*(1-2*x), 0);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            timer = 0;
            attacking = true;
            atk_num = 3;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            weapon.GetComponent<AXE>().SLAY();
        }
    }
}

