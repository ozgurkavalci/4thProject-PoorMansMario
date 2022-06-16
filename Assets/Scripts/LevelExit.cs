using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    

    [SerializeField] float sceneLoadingTime=1f;

    private void OnTriggerEnter2D(Collider2D other) 
    {
       if(other.tag=="Necip")
       {
        StartCoroutine(LoadTheNextLevel());
       }
    }

    IEnumerator LoadTheNextLevel()
    {
        yield return new WaitForSecondsRealtime(sceneLoadingTime);

        int currentSceneNumber=SceneManager.GetActiveScene().buildIndex; 
        
        
        int nextSceneIndex= currentSceneNumber+1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings)
        {

            nextSceneIndex=0;
        }
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
               

    }
}
