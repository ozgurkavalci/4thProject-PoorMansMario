using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   [SerializeField] float arrowSpeed=7f;
   Rigidbody2D arrowRigidBody;
   PlayerMovement player;
   float xSpeed;

   void Awake()
   {
       arrowRigidBody=GetComponent<Rigidbody2D>();
       player=FindObjectOfType<PlayerMovement>();

   }

    void Start()
    {
        xSpeed=player.transform.localScale.x*arrowSpeed;
        
    }

    
    void Update()
    {
        arrowRigidBody.velocity=new Vector2(xSpeed,0f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag=="Enemies")
        {
        Destroy(other.gameObject);
        }
        if(other.tag=="Bouncy")
        {return;}

        Destroy(gameObject);


    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag=="Bouncy")
        {
            return;
        }
        else
        {Destroy(gameObject);}
    }
}
