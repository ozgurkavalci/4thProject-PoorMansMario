using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed=1f;
    Rigidbody2D enemyRigidBody;
    
    private void Awake() 
    { 
        enemyRigidBody=GetComponent<Rigidbody2D>();
        
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyRigidBody.velocity=new Vector2(moveSpeed,0f);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        moveSpeed=-moveSpeed;
        FlipFacingEnemy();


    }

    void FlipFacingEnemy()
    {

        transform.localScale=new Vector2(-(Mathf.Sign(enemyRigidBody.velocity.x)),1f);
    }

}
