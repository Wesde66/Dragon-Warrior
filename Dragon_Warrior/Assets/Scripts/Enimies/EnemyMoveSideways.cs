using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSideways : MonoBehaviour
{
    [SerializeField] float damageAmount;
    [SerializeField] float speed;
    [SerializeField] float moveDistance;
    [SerializeField] Transform forward;
    [SerializeField] Transform backward;
    private bool movingLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingLeft)
        {
            if(transform.position.x >= forward.position.x)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }

        }
        else 
        { 
            if (transform.position.x <= backward.position.x)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<PlayerControl>().TakeDamage(damageAmount);
            }
        }
    }
}
