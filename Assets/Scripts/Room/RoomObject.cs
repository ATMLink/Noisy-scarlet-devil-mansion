using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RoomObject : MonoBehaviour
{
    [SerializeField] private RoomTemplate roomTemplate;
    [SerializeField] private SpriteRenderer roomIcon;

    public int column;
    public int line;
    
    public RoomState roomState;
    private RoomType _roomType;
    
    private AssetReference _sceneToLoad;

    private void Start()
    {
        SetInfo(column,line,roomTemplate);
    }

    public void SetInfo(int column, int line,RoomTemplate roomTemplate)
    {
        _roomType = roomTemplate.roomType;
        _sceneToLoad = roomTemplate.sceneToLoad;
        roomIcon.sprite = roomTemplate.roomImage;
    }

    private void OnMouseDown()
    {
        Debug.Log("clicked the room: "+_roomType);
    }
}
