using System;

[Flags]
public enum RoomType
{
    normalEnemyRoom = 1,
    eliteEnemyRoom = 2,
    restRoom = 4,
    shopRoom = 8,
    treasureRoom = 16,
    bossRoom = 32,
    eventRoom = 64,
    bossRoom2 = 128,
    bossRoom3 = 256
    
}

public enum RoomState
{
    locked,
    visited,
    attainable
}