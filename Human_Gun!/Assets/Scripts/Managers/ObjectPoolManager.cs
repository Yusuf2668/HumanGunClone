using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private Transform _moneyPrefabParent;
    [SerializeField] private Transform _humanPrefabParent;

    private Queue<GameObject> _moneyPoolList;
    private Queue<GameObject> _humanPoolList;

    private void Awake()
    {
        PopulateMoneyPool();
        PopulateHumanPool();
    }

    private void OnEnable()
    {
        EventManager.Instance.MoneyCollected += FindNextMoneyObject;
    }

    private void OnDisable()
    {
        EventManager.Instance.MoneyCollected -= FindNextMoneyObject;
    }

    private void PopulateMoneyPool()
    {
        _moneyPoolList = new Queue<GameObject>();
        for (int i = 0; i < _moneyPrefabParent.transform.childCount; i++)
        {
            var _gameObject = _moneyPrefabParent.transform.GetChild(i).gameObject;
            _moneyPoolList.Enqueue(_gameObject);
        }
    }

    private void PopulateHumanPool()
    {
        _humanPoolList = new Queue<GameObject>();
        for (int i = 0; i < _humanPrefabParent.transform.childCount; i++)
        {
            var _gameObject = _humanPrefabParent.transform.GetChild(i).gameObject;
            _humanPoolList.Enqueue(_gameObject);
        }
    }

    private void FindNextMoneyObject()
    {
        var _gameObject = _moneyPoolList.Dequeue();
        _gameObject.SetActive(true);
        _moneyPoolList.Enqueue(_gameObject);
    }

    public GameObject GetNextHumanObject()
    {
        var _gameObject = _humanPoolList.Dequeue();
        _gameObject.SetActive(true);
        _humanPoolList.Enqueue(_gameObject);
        return _gameObject;
    }
}