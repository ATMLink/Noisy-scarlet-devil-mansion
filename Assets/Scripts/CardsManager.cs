using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CardsManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public int size;

    private readonly Stack<GameObject> _instances = new Stack<GameObject>();

    private void Awake()
    {
        Assert.IsNotNull(cardPrefab);
    }

    public void initialize()
    {
        for(int i = 0; i < size; i++)
        {
            var obj = createInstance();//����ʵ����cardobject��
            obj.SetActive(false);
            _instances.Push(obj);
        }
    }

    private GameObject createInstance()
    {
        var cardObject = Instantiate(cardPrefab, transform, true);
        return cardObject;
    }
}
