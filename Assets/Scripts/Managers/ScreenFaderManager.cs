using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScreenFaderManager : MonoBehaviour
{
    public ScreenFader screenFader;

    AudioFader audioFader;

    // singelton
    public static ScreenFaderManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }
    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        screenFader.FadeOut();
        yield return new WaitForSeconds(screenFader.fadeDuration);
        SceneManager.LoadScene(sceneIndex);
    }

    public void GoToSceneAsync(int sceneIndex)
    {
        
         audioFader = FindFirstObjectByType<AudioFader>();
         if(audioFader != null)
         {
             audioFader.FadeOut();
         }
  
        StartCoroutine(GoToSceneRoutineAsync(sceneIndex));
    }
    IEnumerator GoToSceneRoutineAsync(int sceneIndex)
    {
        screenFader.FadeOut();
       AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
       operation.allowSceneActivation = false;

       float timer = 0f;

       while(timer <= screenFader.fadeDuration && !operation.isDone)
       {
           timer += Time.deltaTime;
           yield return null;
       }
       operation.allowSceneActivation = true;

    }
}
