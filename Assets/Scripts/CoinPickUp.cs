using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpSfx;

    [SerializeField] int pointsForCoinPickUp=100;

    bool wasCollected=false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Necip" && !wasCollected)
        {
            wasCollected=true;
            FindObjectOfType<GameSession>().IncreaseScore(pointsForCoinPickUp);
            AudioSource.PlayClipAtPoint(coinPickUpSfx,Camera.main.transform.position);
            Destroy(gameObject);

        }
        
    }
}
