using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    private AssetReference _currentScene;
    public AssetReference map;

    public void OnLoadRoomEvent(object data)
    {
        if (data != null && data is RoomTemplate)
        {
            var currentData = (RoomTemplate)data;
            Debug.Log(currentData.roomType);

            _currentScene = currentData.sceneToLoad;

            StartCoroutine(LoadAndUnloadScenes());
        }
        else
        {
            Debug.LogError("data is null or not a RoomTemplate instance.");
        }
    }

    private IEnumerator LoadAndUnloadScenes()
    {
        // Unload active scene
        yield return StartCoroutine(UnloadActiveScene());

        // Load new scene
        var loadOperation = _currentScene.LoadSceneAsync(LoadSceneMode.Additive);
        yield return loadOperation;

        if (loadOperation.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(loadOperation.Result.Scene);
        }
        else
        {
            Debug.LogError("Failed to load scene: " + _currentScene);
        }
    }

    private IEnumerator UnloadActiveScene()
    {
        var activeScene = SceneManager.GetActiveScene();
        var unloadOperation = SceneManager.UnloadSceneAsync(activeScene);
        while (!unloadOperation.isDone)
        {
            yield return null;
        }
    }
}


