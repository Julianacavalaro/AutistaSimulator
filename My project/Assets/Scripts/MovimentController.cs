using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentController : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rd;
    private Vector2 moviment;
    private Animator animator;

    void Awake()
        {
        Debug.Log("AWAKE do objeto: " + gameObject.name +
                      " | InstanceID: " + GetInstanceID());
        rd = GetComponent<Rigidbody2D>(); //peguei o componente
        animator = GetComponent<Animator>();
    }
    /* 
       private void OnEnable() 
       {
           Debug.Log("ON ENABLE do objeto: " + gameObject.name +
                     " | InstanceID: " + GetInstanceID());
       }
       private void OnDisable()
       {
           Debug.Log("ON DISABLE do objeto: " + gameObject.name +
                     " | InstanceID: " + GetInstanceID());
       }*/
    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("START do objeto: " + gameObject.name +
                  " | InstanceID: " + GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("UPDATE do objeto: " + gameObject.name +
                  " | InstanceID: " + GetInstanceID());
        moviment.x = Input.GetAxis("Horizontal");
        moviment.y = Input.GetAxis("Vertical");

        Debug.Log("Horizontal: " + moviment.x + " | Vertical: " + moviment.y);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            speed *= 2;
  
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {

            speed /= 2;
        }

    }
    private void FixedUpdate()
    {
        Debug.Log("FIXED UPDATE do objeto: " + gameObject.name +
                  " | InstanceID: " + GetInstanceID());
        rd.velocity = moviment * speed;

    }

    private void LateUpdate()
    {
        Debug.Log("LATE UPDATE do objeto: " + gameObject.name +
                  " | InstanceID: " + GetInstanceID());
        animator.SetFloat("Horizontal", moviment.x);
        animator.SetFloat("Vertical", moviment.y);
      //  animator.SetFloat("Speed", moviment.sqrMagnitude);
    }
}
