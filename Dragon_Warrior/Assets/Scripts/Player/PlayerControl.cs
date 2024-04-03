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
    Animator animator;
    ParticleSystem PlayerSmoke;
    
    
    

    [Header("Camera")]
    CameraMovement cameraMovement;
    [SerializeField] GameObject cameraOB;

    [Header("UI")]
    UIcanvasController UI;
    [SerializeField] GameObject UIGameObject;

    [Header("Projectiles")]
    public GameObject Fireball;
    public Transform FirePoint;

    [Header("Player Atributes")]
    [SerializeField] float jumpPower;
    [SerializeField] float speed;
    [SerializeField] float attackCooldown;
    
    
    bool canAttack;
    private float attackTimer;
    bool isGrounded = true;
    bool isFliped = false;
    Vector2 move;
    float walking;


    [SerializeField] float maxHealth;
    public float currentHealth { get; private set; }



    private void Start()
    {
        //Get components
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cameraMovement = cameraOB.GetComponent<CameraMovement>();
        UI = UIGameObject.GetComponent<UIcanvasController>();
        

        //Player atributes
        currentHealth = maxHealth;
        
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

    public void TakeDamage (float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, maxHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
        }
        else
        {
            animator.SetTrigger("Die");
        }
    }

    public void GiveHealth(float _health)
    {
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, maxHealth);
    }

    





}
