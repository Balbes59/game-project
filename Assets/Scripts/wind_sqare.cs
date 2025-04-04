using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class Wind_sqare : MonoBehaviour
{
    public int napr = 1; //по часовой, 1 - вверхи и по 45 градусов
    public float power = 100.0f;
    private Vector2 force;
    [SerializeField] private GameObject Hero;
    private Vector2 coords;
    private Vector2 size;
    private Vector2 target;
    private Vector2 co_min;
    private Vector2 co_max;
    [SerializeField] private GameObject[] objs;

    void Start()
    {
        coords = transform.position;
        print(coords);
        size = transform.localScale;
        print(size);
        co_min = new Vector2(coords.x - size.x / 2, coords.y - size.y / 2);
        co_max = new Vector2(coords.x + size.x / 2, coords.y + size.y / 2);
        objs = GameObject.FindGameObjectsWithTag("Enemy");
        Hero = GameObject.FindGameObjectWithTag("Player");
        switch (napr)
        {
            case 1: force = new Vector2(0, power); break;
            case 2: force = new Vector2(power * 0.71f, power * 0.71f); break;
            case 3: force = new Vector2(power, 0); break;
            case 4: force = new Vector2(power * 0.71f, -power * 0.71f); break;
            case 5: force = new Vector2(0, -power); break;
            case 6: force = new Vector2(-power * 0.71f, power * 0.71f); break;
            case 7: force = new Vector2(-power, 0); break;
            case 8: force = new Vector2(-power * 0.71f, -power * 0.71f); break;
            default: print("разраб алкаш"); break;
        }
    }


    void Update()
    {
        for (int i = 0; i < objs.Length; i++)
        {
            target = objs[i].transform.position;
            if ((target.x > co_min.x && target.x < co_max.x) && (target.y > co_min.y && target.y < co_max.y))
            {
                objs[i].GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
        target = Hero.transform.position;
        if ((target.x > co_min.x && target.x < co_max.x) && (target.y > co_min.y && target.y < co_max.y))
        {
            Hero.GetComponent<Rigidbody2D>().AddForce(force);
        }
    }
}
