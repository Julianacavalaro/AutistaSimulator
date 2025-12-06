using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rd;
    private Vector2 moviment;
    private Animator animator;
    private bool isMoving = false;

    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        // garante que o Animator já comece na direção certa
        animator.SetFloat("Horizontal", 0f);
        animator.SetFloat("Vertical", -1f);  // baixo
        animator.SetBool("isMoving", false);
    }
    void Update()
    {
        moviment.x = Input.GetAxis("Horizontal");
        moviment.y = Input.GetAxis("Vertical");

        if (moviment.x != 0 || moviment.y != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 2;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= 2;
    }

    void FixedUpdate()
    {
        rd.velocity = moviment * speed;
    }

    void LateUpdate()
    {
        if (isMoving)
        {
            // andando → usa a direção real
            animator.SetFloat("Horizontal", moviment.x);
            animator.SetFloat("Vertical", moviment.y);
        }
        else
        {
            // parado → sempre olhando para baixo (MoveDown)
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", -1f);
        }

        animator.SetBool("isMoving", isMoving);
    }
}