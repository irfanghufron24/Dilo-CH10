﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControll : MonoBehaviour
{
    public float moveSpeed;
    public float moveConstraint;
    private Vector3 targetMove;

    private Rigidbody2D p_rigidbody;

    private void Awake()
    {
        p_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePos.x < moveConstraint && mousePos.x > -moveConstraint
                && mousePos.y < moveConstraint && mousePos.y > -moveConstraint)
            {
                targetMove = mousePos;
                targetMove.z = 0;
            }
        }

        if (transform.position != targetMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetMove, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.AddScore();
            gameObject.SetActive(false);
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
