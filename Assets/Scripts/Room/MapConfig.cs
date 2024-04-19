using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RoomSelect/MapConfig",
    fileName = "MapConfig",
    order = 1)]
public class MapConfig : ScriptableObject
{
    public List<RoomBlueprint> roomBlueprints;
}

[Serializable]
public class RoomBlueprint
{
    public int min, max;
    public RoomType roomType;
}
