using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
            {
                StartCoroutine(LoadSceneRoutine(operation));
            }

            yield return null;
        }
    }

    IEnumerator LoadSceneRoutine(AsyncOperation operation)
    {
        yield return new WaitForSeconds(3f);
        operation.allowSceneActivation = true;
    }
}
