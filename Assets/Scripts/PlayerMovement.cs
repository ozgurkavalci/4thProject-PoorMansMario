using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed=10f;
    [SerializeField] float jumpSpeed=5f;
    [SerializeField] float climbSpeed=3f;

    [SerializeField] GameObject arrow;

    [SerializeField] Transform bow;

    CapsuleCollider2D myCapsuleCollider2D;
    Vector2 moveInput;
    Rigidbody2D myRigidBody;

    BoxCollider2D myBoxCollider2D;

    Animator myAnimator;



    
    
    public bool necipCanMove=true;

    public bool jumpOnlyOnceWhenYouFallOnHazards=true;

    public bool jumpOnlyOnceByEnemy=true;



    private void Awake() 
    {
        myCapsuleCollider2D=GetComponent<CapsuleCollider2D>();
        myBoxCollider2D=GetComponent<BoxCollider2D>();
         myRigidBody=GetComponent<Rigidbody2D>();
         myAnimator=GetComponent<Animator>();
         
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        HazardDeath();
        if(necipCanMove)
        {

          Run();
          FlipSprite();
          ClimbLadder();
          
         if(!Input.GetKey(KeyCode.Mouse0))
         {
            myAnimator.SetBool("isShooting",false);

         }        

        }
    }

    
    
    void OnTriggerEnter2D(Collider2D other) 
    {
          
            if(jumpOnlyOnceByEnemy)
            {
                     if(other.tag=="Enemies")
                     {
                          necipCanMove=false;
                          myRigidBody.velocity=new Vector2(0f,25f);
                          jumpOnlyOnceByEnemy=false;
                
                           myAnimator.SetTrigger("Dying");
                           FindObjectOfType<GameSession>().ProcessPlayerDeath();
                     }

            }
    }
      
    
    void OnFire(InputValue value)
    {
                   
     if(value.isPressed && necipCanMove && !myAnimator.GetBool("isClimbing"))
     {  Instantiate(arrow,bow.position,transform.rotation);
            
         myAnimator.SetBool("isShooting",true);
                                           
     }
                      
    }

    void OnMove(InputValue value)
    {
        moveInput=value.Get<Vector2>();
        
        
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {

        if(!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {return;}

        if(necipCanMove)
        {

            if(value.isPressed)
            {
              myRigidBody.velocity+=new Vector2(0f,jumpSpeed);
            }
        }

        
        
    }

    void Run()
    {
        
        Vector2 playerVelocity=new Vector2(moveInput.x*runSpeed,myRigidBody.velocity.y);

        myRigidBody.velocity=playerVelocity;
        
        if(Mathf.Abs(myRigidBody.velocity.x)>Mathf.Epsilon)
        {

           myAnimator.SetBool("isRunning",true);
        }
        else
        {
            myAnimator.SetBool("isRunning",false);
        }

    }

    void FlipSprite()
    {       
       bool playerHasHorizontalSpeed=Mathf.Abs(myRigidBody.velocity.x)>Mathf.Epsilon;
 
       if(playerHasHorizontalSpeed)
       {
          transform.localScale=new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
       }      
    }

    void ClimbLadder()
    {
        
        if(!myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {myAnimator.SetBool("isClimbing",false);
        myRigidBody.gravityScale=8f;
        
          return;}
        
        else
        {
            Vector2 climbVelocity=new Vector2(myRigidBody.velocity.x,moveInput.y*climbSpeed);
             myRigidBody.gravityScale=0f;

             myRigidBody.velocity=climbVelocity;
             if(Mathf.Abs(climbVelocity.y)>Mathf.Epsilon)
             {
               myAnimator.SetBool("isClimbing",true);

             }
             else
             {
                 myAnimator.SetBool("isClimbing",false);
             }
                 
             
        }
              
    }

    void HazardDeath()
    {
        
       if(jumpOnlyOnceWhenYouFallOnHazards)
       {
        if(myBoxCollider2D.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {

            necipCanMove=false;

            myRigidBody.velocity=new Vector2(0f,30f);
                
            myAnimator.SetTrigger("Dying");
            jumpOnlyOnceWhenYouFallOnHazards=false;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }

       }
    }


}
