using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RoomObject : MonoBehaviour
{
    public RoomTemplate roomTemplate;
    
    private SpriteRenderer roomIcon;

    private int _column =0;
    private int _line =0;
    
    public RoomState roomState;
    private RoomType _roomType;
    
    private AssetReference _sceneToLoad;

    public ObjectEventSO loadRoomEvent;

    private void Awake()
    {
        roomIcon = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetInfo(int column, int line,RoomTemplate roomTemplate)
    {
        this._column = column;
        this._line = line;
        this.roomTemplate = roomTemplate;

        roomIcon.sprite = roomTemplate.roomImage;
    }

    private void OnMouseDown()
    {
        // Debug.Log("clicked the room: "+_roomType);
        loadRoomEvent.RaisEvent(roomTemplate, this);
        // Debug.Log("event raised");
    }
}
