using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    [Header("room setting")]
    public MapConfig mapConfig;
    

    [Header("positions")]
    private float _screenHeight;
    private float _screenWidth;
    private float _columnWidth;
    private Vector3 _generatePoint;
    private float _border;

    [Header("prefabs")] 
    public LineRenderer linePrefab;
    public RoomObject roomPrefab;

    private List<RoomObject> _rooms = new();
    private List<LineRenderer> _lines = new();
    private void Awake()
    {
        _screenHeight = Camera.main.orthographicSize * 2;
        _screenWidth = _screenHeight * Camera.main.aspect;
    
        _columnWidth = _screenWidth / (mapConfig.roomBlueprints.Count+1);
    }

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        _border = 4;

        List<RoomObject> previousColumnRoomObjects = new();
        
        for (int column = 0; column < mapConfig.roomBlueprints.Count; column++)
        {
            var roomBlueprint = mapConfig.roomBlueprints[column];
            var amount = Random.Range(roomBlueprint.min, roomBlueprint.max);
            var startHeight = _screenHeight / 2 - _screenHeight / (amount + 1);

            List<RoomObject> currentColumnRoomObjects = new();
            
            for (int i = 0; i < amount; i++)
            {
                var newPosition = new Vector3(-_screenWidth / 2 + _border, startHeight + i * (_screenHeight / (amount + 1)), 0);
                var room = Instantiate(roomPrefab, newPosition, Quaternion.identity, transform);
                
                _rooms.Add(room);
                currentColumnRoomObjects.Add(room);
            }

            if (previousColumnRoomObjects.Count > 0)
            {
                CreateConnections(previousColumnRoomObjects, currentColumnRoomObjects);
            }
    
            _border += _columnWidth;
            previousColumnRoomObjects = currentColumnRoomObjects;
        }
    }

    public void CreateConnections(List<RoomObject> previousRooms, List<RoomObject> currentRooms)// connect previous column, current column
    {
        HashSet<RoomObject> connectedCurrentRooms = new();
        foreach (var previousRoom in previousRooms)
        {
            var targetRoom = ConnectToRandomRoom(previousRoom, currentRooms);
            connectedCurrentRooms.Add(targetRoom);
        }

        foreach (var currentRoom in currentRooms)
        {
            if (!connectedCurrentRooms.Contains(currentRoom))
            {
                ConnectToRandomRoom(currentRoom, previousRooms);
            }
        }
    }

    private RoomObject ConnectToRandomRoom(RoomObject roomObject, List<RoomObject> currentRooms)
    {
        RoomObject targetRoom;

        targetRoom = currentRooms[Random.Range(0, currentRooms.Count)];

        var line = Instantiate(linePrefab, transform);
        line.SetPosition(0, roomObject.transform.position);
        line.SetPosition(1, targetRoom.transform.position);
        
        _lines.Add(line);
        return targetRoom;
    }

    [ContextMenu(itemName:"ReGenerateRoom")]
    public void ReGenerateRoom()
    {
        foreach (var room in _rooms)
        {
            Destroy(room.gameObject);
        }

        foreach (var line in _lines)
        {
            Destroy(line.gameObject);
        }
        
        _rooms.Clear();
        _lines.Clear();
        
        Generate();
    }
}
