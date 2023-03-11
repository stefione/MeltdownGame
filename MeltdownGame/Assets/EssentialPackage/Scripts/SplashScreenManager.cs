using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SplashScreenManager : MonoBehaviour
{
    public string nextSceneName;
    public float loadSceneBackupDelay;
    private void Start()
    {
        StartCoroutine(LoadSceneBackupCoroutine());
    }

    IEnumerator LoadSceneBackupCoroutine()
    {
        yield return new WaitForSeconds(loadSceneBackupDelay);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
