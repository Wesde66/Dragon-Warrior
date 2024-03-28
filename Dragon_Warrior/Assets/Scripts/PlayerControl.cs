using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class PlayerControl : MonoBehaviour
{

    Rigidbody2D rigidbody2d;
    CameraMovement cameraMovement;
    [SerializeField] GameObject cameraOB;

    public GameObject Fireball;
    public Transform FirePoint;
    
    Animator animator;
    [SerializeField] float jumpPower;
    [SerializeField] float speed;
    [SerializeField] float attackCooldown;
    [SerializeField]bool canAttack;

    private float attackTimer;

    bool isGrounded = true;
    bool isFliped = false;

    Vector2 move;
    float walking;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cameraMovement = cameraOB.GetComponent<CameraMovement>();
        
    }

    private void Update()
    {
        cameraMovement.DirectionPlayer(walking);
        // Move player left and right

        Vector2 pos = transform.position;

        pos.x += walking * Time.deltaTime;
        transform.position = pos;

        //transform.Translate(move);
        // Animation for walking
        if (walking != 0)
        {
            animator.SetBool("WalkingAnim", true);
            
        }
        else
        {
            animator.SetBool("WalkingAnim", false);
        }

        FlipPlayer();
        attackTimer += Time.deltaTime;
        if (attackCooldown < attackTimer)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

    }



    public void OnMove(InputValue input)
    {
        
        Vector2 m = input.Get<Vector2>();
        walking = m.x * speed;
        
        
        
        
    }
    public void OnJump()
    {
        if (isGrounded)
        {
            animator.SetBool("Jumping", true);
            rigidbody2d.velocity = Vector3.up * jumpPower;
            isGrounded = false;
        }
        

    }
    public void OnFireAttack()
    {
        
        if(isGrounded && canAttack)
        {
            attackTimer = 0;
            animator.SetTrigger("AttackFireball");
            //Instantiate fire ball
            Instantiate(Fireball, FirePoint.position, transform.rotation);
        }
    }
        

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.transform.tag == "Ground")
            {
                isGrounded = true ;
                animator.SetBool("Jumping", false );
                
            }
        }
    }

    private void FlipPlayer()
    {
        if (walking < 0 && !isFliped)
        {
            
            
            transform.Rotate(0,-180, 0, Space.World);
            isFliped = true ;
        }

        if (walking > 0 && isFliped)
        {
            
            
            transform.Rotate(0,180,0, Space.World);
            isFliped = false ;
        }
    }

    





}
