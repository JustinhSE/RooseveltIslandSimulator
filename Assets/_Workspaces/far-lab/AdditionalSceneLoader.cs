using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditionalSceneLoader : MonoBehaviour {
    public string AdditionalSceneName;
    public bool loadAdditionalScene;
    private bool loaded;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        loaded = false;
    }
    void Update()
    {
        if (loadAdditionalScene)
        {
            if (!loaded && AppController.Instance.sceneSelected)
            {
                loaded = true;
                // Use a coroutine to load the Scene in the background
                Debug.Log("about to lead in the additional scene ");
                if (AdditionalSceneName.Length > 0)
                {
                    StartCoroutine(LoadYourAsyncScene());
                }
            }
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
       

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(AdditionalSceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Debug.Log("Loading the scene additionally was done");
    }
}
