using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives=3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int score=0;

    private void Awake() 
    {
        int numberGameSessions=FindObjectsOfType<GameSession>().Length;
        if(numberGameSessions>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        scoreText.text=score.ToString();
        livesText.text=playerLives.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives>1)
        {
             StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    public void IncreaseLifeCount(int lifeAdded)
    {
       if(playerLives<3)
       {
           playerLives++;
           livesText.text=playerLives.ToString();
       }
       
    
    }

    public void IncreaseScore(int pointsToAdd)
    {

        score+=pointsToAdd;
         scoreText.text=score.ToString();
    }

    IEnumerator ResetGameSession()
    {
        
        yield return new WaitForSecondsRealtime(2f);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    IEnumerator TakeLife()
    {
       playerLives--;

       yield return new WaitForSecondsRealtime(1f);

       int currentSceneIndex=SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(currentSceneIndex);
       livesText.text=playerLives.ToString();


    }

}
