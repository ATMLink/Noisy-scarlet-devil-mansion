using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;


[CreateAssetMenu(menuName = "RoomSelect/Room",
    fileName = "Room",
    order = 0)]
public class RoomTemplate : ScriptableObject
{
    public Sprite roomImage;
    public roomType roomType;
    public AssetReference sceneToLoad;
}
