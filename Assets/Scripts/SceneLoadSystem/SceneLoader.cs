using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoadSystem
{
    public class SceneLoader : MonoBehaviour
    {
        public event Action<string> SceneLoaded; 

        [SerializeField, ScenePath] private string _loadingScenePath;
        private Coroutine _coroutine;
        
        public bool IsLoading { get; private set; }
        public float Progress { get; private set; }

        public void LoadScene(string path, bool dontShowLoading = false)
        {
            if (IsLoading)
            {
                Debug.LogWarning("Scene already loading");
                return;
            }
            _coroutine = StartCoroutine(LoadingRoutine(path, null, dontShowLoading));
        }
        
        public void LoadScene(string path, Action onLoaded, bool dontShowLoading = false)
        {
            if (IsLoading)
            {
                Debug.LogWarning("Scene already loading");
                return;
            }
            _coroutine = StartCoroutine(LoadingRoutine(path, onLoaded, dontShowLoading));
        }

        IEnumerator LoadingRoutine(string path, Action onLoaded, bool dontShowLoading)
        {
            IsLoading = true;
            if (!dontShowLoading)
            {
                SceneManager.LoadScene(_loadingScenePath);
            }

#if UNITY_EDITOR
            Debug.Log("Debug loading!");
            yield return new WaitForSeconds(5);
#endif
            var loadingSceneOperation = SceneManager.LoadSceneAsync(path);
            while (!loadingSceneOperation.isDone)
            {
                Progress = loadingSceneOperation.progress;
                yield return null;
            }
            SceneLoaded?.Invoke(path);
            onLoaded?.Invoke();
            IsLoading = false;
        }
    }
}