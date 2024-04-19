using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneLoadManager : MonoBehaviour
{
    // private AssetReference _currentScene;
    // public AssetReference map;
    //
    public void OnLoadRoomEvent(object data)
    {
        if (data != null && data is RoomTemplate)
        {
            var currentData = (RoomTemplate)data;
            Debug.Log(currentData.roomType);
        }
        else
        {
            Debug.LogError("data is null or not a RoomTemplate instance.");
        }

    }
    
}
