using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTraps : MonoBehaviour
{
    [SerializeField] PlayerControl playerControl;
    [SerializeField] float damage;
    // Start is called before the first frame update
    void Start()
    {
        
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
                playerControl.TakeDamage(damage);
            }
        }
    }
}
