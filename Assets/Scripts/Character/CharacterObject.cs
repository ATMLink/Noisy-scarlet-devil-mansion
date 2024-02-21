using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    public CharacterTemplate characterTemplate;
    public RuntimeCharacter character;

    public void OnCharacterDead()
    {
        if (character.hp.Value <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            var numberOfChildObjects = transform.childCount;

            for (var i = numberOfChildObjects - 1; i >= 0; i--)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
