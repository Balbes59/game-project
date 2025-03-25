using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    private float speed = 5f;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputVector.x = 1f;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputVector.x = -1f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputVector.y = 1f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputVector.y = -1f;
        }
        rb.MovePosition(rb.position + inputVector * Time.fixedDeltaTime * speed);
    }
}