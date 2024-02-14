using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    protected CharacterObject player;
    protected List<CharacterObject> enemies;

    public virtual void Initialize(CharacterObject playerParameter, List<CharacterObject> enemiesParameter)
    {
        player = playerParameter;
        enemies = enemiesParameter;
    }
}
