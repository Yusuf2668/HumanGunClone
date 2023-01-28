using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStateMachine : MonoBehaviour
{
    public WeaponState CurrentWeapon
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    [SerializeField] private WeaponState _currentState;
    [SerializeField] private GameObject _mainPlayer;
    [SerializeField] private CollisionHandler _collisionHandler;

    public List<GameObject> WeaponHumanList
    {
        get { return _weaponHumanList; }
        set { _weaponHumanList = value; }
    
    }

    [SerializeField] private List<GameObject> _weaponHumanList;


    private void Awake() => _weaponHumanList.Add(_mainPlayer);


    private void OnEnable()
    {
        EventManager.Instance.CollisionHuman += AddListToCollisionHuman;
    }

    private void Update()
    {
        RunStateMachine();
    }

    private void AddListToCollisionHuman(GameObject collisionHuman)
    {
        if (_collisionHandler.HumanCount <=
            _currentState.CountOfNextGun) //human toplama sınırına ulaşmış ve hala human topluyorken 
        {
            _weaponHumanList.Add(collisionHuman);
        }
    }

    private void RunStateMachine()
    {
        var weaponState = _currentState.RunState();
        if (weaponState != null)
        {
            _currentState = weaponState;
        }
    }
}