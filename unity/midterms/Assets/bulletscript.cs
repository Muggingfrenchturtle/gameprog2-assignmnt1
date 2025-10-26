using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    public Vector2 shootDirectionFromGun;

    public Rigidbody2D rigidbooty;

    public float colorValue; //color is changed by the player during instantiation. this is just to handle its interactions with enemies

    // Start is called before the first frame update
    void Start()
    {
        rigidbooty = GetComponent<Rigidbody2D>();
        rigidbooty.velocity = shootDirectionFromGun; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("player"))
        {
            Destroy(gameObject); //just destroy the bullet once it gets contact with any trigger collider that isnt the player
        }
    }

    

}
