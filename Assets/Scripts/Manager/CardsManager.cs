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
            var obj = createInstance();//创建实例化cardobject。
            obj.SetActive(false);
            _instances.Push(obj);
        }
    }

    private GameObject createInstance()
    {
        var cardObject = Instantiate(cardPrefab, transform, true);
        return cardObject;
    }

    public GameObject getObject()
    {
        var obj = _instances.Count > 0 ? _instances.Pop() : createInstance();
        obj.SetActive(true);
        return obj;
    }

    public void returnObject(GameObject gameObj)
    {
        var pooledObject = gameObj.GetComponent<ManagedPoolObject>();
        Assert.IsNotNull(pooledObject);
        Assert.IsTrue(pooledObject.cardsManager == this);
        
        gameObj.SetActive(false);
        if (!_instances.Contains(gameObj))
        {
            _instances.Push(gameObj);
        }
    }
    public class ManagedPoolObject : MonoBehaviour
    {
        public CardsManager cardsManager;
    }
}
