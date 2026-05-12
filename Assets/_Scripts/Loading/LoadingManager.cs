using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    private float _fakeProgress = 0f;

    public Slider progressBar;
    public string nextSceneName;
    public float loadingTime = 3f;

    void Start()
    {
        LoadSceneWithLoading(nextSceneName);
    }

    public void ShowLoading(System.Action callback = null)
    {
        callback?.Invoke();
    }

    public void LoadSceneWithLoading(string sceneName)
    {
        ShowLoading(() =>
        {
            StartCoroutine(LoadSceneAsync(sceneName));
        });
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        float timer = 0f;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;

            if (_fakeProgress < 0.95f)
            {
                _fakeProgress = Mathf.Clamp01(timer / loadingTime * 0.95f);
                progressBar.value = _fakeProgress;
            }
            if (operation.progress >= 0.9f && timer >= loadingTime)
            {
                _fakeProgress = 1f;
                progressBar.value = 1f;

                yield return new WaitForSeconds(0.2f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}