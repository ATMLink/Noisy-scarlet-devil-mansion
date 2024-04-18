using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum roomType
{
    normalEnemyRoom = 0,
    eliteEnemyRoom = 1,
    restRoom = 2,
    shopRoom = 3,
    bossRoom = 4,
    eventRoom = 5
    
}
[CreateAssetMenu(menuName = "RoomSelect/Room",
    fileName = "Room",
    order = 0)]
public class RoomTemplate : ScriptableObject
{
    public Sprite roomImage;
    public roomType roomType;
}
