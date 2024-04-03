using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePot : MonoBehaviour
{
    [SerializeField] GameObject fire;
    BoxCollider2D boxCollider2D;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
       animator = GetComponent<Animator>();
       boxCollider2D = GameObject.FindWithTag("FirePot").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player")
            {
                
                boxCollider2D.enabled = true;
                animator.SetBool("ActivateFirePot", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player")
            {

                StartCoroutine(ResetTrap());
            }
        }
    }

    IEnumerator ResetTrap()
    {
        yield return new WaitForSeconds(2);
        boxCollider2D.enabled = false;
        animator.SetBool("ActivateFirePot", false);
    }
}
