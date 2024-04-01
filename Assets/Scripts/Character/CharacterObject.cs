using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObject : MonoBehaviour
{
    public CharacterTemplate characterTemplate;
    public RuntimeCharacter character;

    public void OnCharacterDead()
    {
        Debug.Log("enemy dead");
        if (character.hp.Value <= 0)// accurate hp
        {
            Debug.Log("enemy dead accurately executed");
            GetComponent<BoxCollider2D>().enabled = false;
            var numberOfChildObjects = transform.childCount;// not executed

            for (var i = numberOfChildObjects - 1; i >= 0; i--)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
