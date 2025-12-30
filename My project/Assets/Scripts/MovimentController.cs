
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
    private bool isJumping = false;

    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    { 
        // Inicia parado e olhando para baixo
        animator.SetFloat("Horizontal", 0f);
        animator.SetFloat("Vertical", -1f);
        animator.SetBool("isMoving", false);
        animator.SetBool("jump", false);
    }

    void Update()
    {
        // Bloqueia movimento enquanto está pulando
        //if (isJumping)
        //    return;

        moviment.x = Input.GetAxisRaw("Horizontal");
        moviment.y = Input.GetAxisRaw("Vertical");

        // ✅ Permitir movimento em 4 direções
        if (Mathf.Abs(moviment.x) > Mathf.Abs(moviment.y))
        {
            moviment.y = 0; // Prioriza horizontal
        }
        else
        {
            moviment.x = 0; // Prioriza vertical
        }

        isMoving = moviment != Vector2.zero;

        // Correr com SHIFT
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= 2;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= 2;

        // Pular com espaço
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(JumpAnimation());
        }
    }

    private IEnumerator JumpAnimation()
    {
        isJumping = true;
        animator.SetBool("jump", true);
        Debug.Log("Pulando (animação)!");

        // Duração da animação de pulo (ajuste conforme o tempo da animação)
        yield return new WaitForSeconds(0.6f);

        animator.SetBool("jump", false);
        isJumping = false;
    }

    void FixedUpdate()
    {
        if (!isJumping)
        {
            rd.velocity = moviment * speed;
        }
        else
        {
            rd.velocity = Vector2.zero; // Fica parado durante o pulo
        }
    }

    void LateUpdate()
    {
        animator.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            // Andando → usa a direção real
            animator.SetFloat("Horizontal", moviment.x);
            animator.SetFloat("Vertical", moviment.y);
        }
        else
        {
            // Parado → mantém última direção
            // (sem forçar olhar pra baixo)
        }
    }
}