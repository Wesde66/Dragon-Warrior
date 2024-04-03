using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float attackCooldown;
    private bool isReadyToExplode = false;
    

    bool isMoving = true;

    BoxCollider2D boxCollider2D;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(Waittime());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMoving) 
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        

        if(isReadyToExplode)
        {
            animator.SetTrigger("ExplodeFireball");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            isMoving = false;
            animator.SetTrigger("ExplodeFireball");
            boxCollider2D.enabled = false;
            
        }
    }

    public void DestroyFireball()
    {
        
        Destroy(this.gameObject);
    }

    IEnumerator Waittime()
    {
        yield return new WaitForSeconds(attackCooldown);
        isReadyToExplode = true;
        isMoving = false;
    }


}
