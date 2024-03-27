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
    SpriteRenderer spriteRenderer;
    Animator animator;
    [SerializeField] float jumpPower;
    [SerializeField] float speed;

    bool isGrounded = true;
    bool isFliped = false;

    Vector2 move;
    [SerializeField] float walking;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
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

    }



    public void OnMove(InputValue input)
    {
        Debug.Log("walking");
        Vector2 m = input.Get<Vector2>();
        walking = m.x * speed;
        
        
        
        
    }
    public void OnJump()
    {
        if (isGrounded)
        {
            
            rigidbody2d.velocity = Vector3.up * jumpPower;
            isGrounded = false;
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.transform.tag == "Ground")
            {
                isGrounded = true ;
                
            }
        }
    }

    private void FlipPlayer()
    {
        if (walking < 0 && !isFliped)
        {
            Debug.Log("rotate");
            
            transform.Rotate(0,-180, 0, Space.World);
            isFliped = true ;
        }

        if (walking > 0 && isFliped)
        {
            Debug.Log("rotate");
            
            transform.Rotate(0,180,0, Space.World);
            isFliped = false ;
        }
    }

    





}
