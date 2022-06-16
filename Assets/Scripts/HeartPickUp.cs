using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSfx;
    [SerializeField] int pointsForHeartPickUp=1;

    bool wasCollected=false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Necip" && !wasCollected)
        {
            wasCollected=true;
            FindObjectOfType<GameSession>().IncreaseLifeCount(pointsForHeartPickUp);
            AudioSource.PlayClipAtPoint(coinPickUpSfx,Camera.main.transform.position);
            Destroy(gameObject);

        }
        
    }
}
