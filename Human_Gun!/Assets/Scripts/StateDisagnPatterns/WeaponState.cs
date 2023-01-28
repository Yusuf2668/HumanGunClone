using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public abstract class WeaponState : MonoBehaviour
{
    public int CountOfNextGun
    {
        get { return _countOfNextGun; }
    }

    [Header("State")] [SerializeField] private WeaponState _nextState;
    [SerializeField] private WeaponState _previousState;
    [SerializeField] private WeaponStateMachine _weaponStateMachine;

    [Header("Referances")] [SerializeField]
    private GameObject _gunBullet;

    [SerializeField] private Transform _fireTriggerHuman;
    [SerializeField] private Transform _player;
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private TriggerHandler _triggerHandler;


    [SerializeField] private List<Vector3> _stateHumanPosition;
    [SerializeField] private List<Vector3> _stateHumanRotation;
    [SerializeField] private List<HumanColors> _humanColorsList;
    [SerializeField] private List<HumanAnim> _humanAnimList;
    [SerializeField] private List<Transform> _humanParentObjectList;

    [SerializeField] private int _countOfNextGun;
    [SerializeField] private int _countOfPreviousGun;

    [SerializeField] private ObjectPoolManager _objectPoolManager;

    [SerializeField] private bool _startState;

    enum HumanColors
    {
        Yellow,
        Black,
        Red,
        Blue
    }

    enum HumanAnim
    {
        Pose01,
        Pose02,
        Pose03
    }

    private void OnEnable()
    {
        EventManager.Instance.TriggerObstacle += DropHuman;
        EventManager.Instance.CollisionStone += CollisionStone;
        EventManager.Instance.AddHumanForWeapon += CreateHumanForWeapon;
        EventManager.Instance.DropHuman += DropHuman;
    }

    private void OnDisable()
    {
        EventManager.Instance.TriggerObstacle -= DropHuman;
        EventManager.Instance.CollisionStone -= CollisionStone;
        EventManager.Instance.AddHumanForWeapon -= CreateHumanForWeapon;
        EventManager.Instance.DropHuman -= DropHuman;
    }


    public abstract WeaponState RunState();

    public  WeaponState Run(float _fireTimeCount)
    {
        if (_startState &&
            _weaponStateMachine.WeaponHumanList.Count >= 1)
        {
            SetHumansPositionOfNewState();
            _startState = false;
        }
        var collisionHandlerHumanCount = _collisionHandler.HumanCount;
        
        if (collisionHandlerHumanCount <= _countOfPreviousGun && _previousState != null)
        {
            return _previousState;
        }

        if (collisionHandlerHumanCount > _countOfNextGun && _nextState != null)
        {
            _startState = true;
            return _nextState;
        }

        if (_weaponController.LookAtTarget(_player) && collisionHandlerHumanCount > 1)
        {
            Fire(_fireTriggerHuman, _fireTimeCount);
        }
        else
        {
            _gunBullet.SetActive(false);
        }

        var collisionHandlerCollectedObject = _collisionHandler.CollectedHuman;
        if (collisionHandlerCollectedObject == null)
        {
            return this;
        }

        if (collisionHandlerHumanCount > _countOfNextGun) //human toplama sınırına ulaşmış ve hala human topluyorken 
        {
            CollectHuman(collisionHandlerCollectedObject);
            _collisionHandler.CollectedHuman = null;
            return this;
        }
        var animTrigger = _humanAnimList[collisionHandlerHumanCount].ToString();
        var colorName = _humanColorsList[collisionHandlerHumanCount].ToString();
        var parentObject = _humanParentObjectList[collisionHandlerHumanCount];
        AddHumanForWeapon(collisionHandlerCollectedObject, parentObject,
            collisionHandlerHumanCount, colorName, animTrigger);
        _collisionHandler.CollectedHuman = null;
        return this;
    }


    private void CreateHumanForWeapon(int humanCount)
    {
        if (_weaponStateMachine.CurrentWeapon != this)
        {
            return;
        }

        EventManager.Instance.OnPlayerAnimatorTrigger(nameof(StringType.HumanAnimatorTriggers.Pose03));
        for (int i = 0; i < humanCount; i++)
        {
            _collisionHandler.HumanCount++;
            var _collisionHandlerHumanCount = _collisionHandler.HumanCount;
            if (_collisionHandlerHumanCount <= _countOfNextGun)
            {
                var _humanObject = _objectPoolManager.GetNextHumanObject();
                AddHumanForWeapon(_humanObject,
                    _humanParentObjectList[_collisionHandlerHumanCount], _collisionHandlerHumanCount,
                    _humanColorsList[_collisionHandlerHumanCount].ToString(),
                    _humanAnimList[_collisionHandlerHumanCount].ToString());
            }
        }
    }

    private void AddHumanForWeapon(GameObject triggerHandlerCollectedObject,
        Transform parentObject, int triggerHandlerHumanCount, string colorName,
        string animTrigger) //İnsanları toplayıp silah yaptığımız method
    {
        EventManager.Instance.OnCollisionHuman(triggerHandlerCollectedObject);
        triggerHandlerCollectedObject.GetComponent<HumanController>()
            .CollectForGun(_stateHumanPosition[triggerHandlerHumanCount],
                _stateHumanRotation[triggerHandlerHumanCount], parentObject
                , colorName, animTrigger);
        _collisionHandler.CollectedHuman = null;
    }
    
    private void DropHuman()
    {
        var weaponStateMachineHumanList = _weaponStateMachine.WeaponHumanList;

        if (_weaponStateMachine.CurrentWeapon == this && weaponStateMachineHumanList.Count > 1)
        {
            var dropHuman = weaponStateMachineHumanList[weaponStateMachineHumanList.Count - 1];
            var dropHumanTransform = dropHuman.transform;
            var dropHumanPos = dropHuman.transform.position;
            _collisionHandler.HumanCount--;
            weaponStateMachineHumanList.Remove(dropHuman);
            dropHumanTransform.SetParent(null);
            dropHuman.GetComponent<Rigidbody>().isKinematic = false;
            dropHumanTransform.DOJump(
                new Vector3(dropHumanPos.x + Random.Range(-1, 1), dropHumanPos.y + 0.5f,
                    dropHumanPos.z + Random.Range(-1, 1)), 1, 1, 0.5f);
        }
        else if (!_triggerHandler.OnBonusState && weaponStateMachineHumanList.Count < 2)
        {
            EventManager.Instance.OnLoseGame();
        }
        else if (_triggerHandler.OnBonusState && weaponStateMachineHumanList.Count < 2)
        {
            EventManager.Instance.OnFinishGame();
        }
    }


    private void CollisionStone()//Stone çarpıp geri gitmesini ve adam bırakması
    {
        DropHuman();
        _player.DOMoveZ(_player.position.z - 0.5f, 0.5f);
    }

    private void SetHumansPositionOfNewState() //tekrar state e girerse o state e  göre human pos ları ayarlaması için
    {
        var humanList = _weaponStateMachine.WeaponHumanList;
        var humanListCount = _weaponStateMachine.WeaponHumanList.Count;
        for (int i = 1; i < humanList.Count; i++)
        {
            if (i >= _stateHumanPosition.Count)
            {
                return;
            }

            humanList[i].GetComponent<HumanController>().CollectForGun(_stateHumanPosition[i],
                _stateHumanRotation[i],
                _humanParentObjectList[i], _humanColorsList[i].ToString(), _humanAnimList[i].ToString());
        }
    }

    private void Fire(Transform _fireTriggerHuman, float _fireTimeCount)
    {
        if (!_gunBullet.activeInHierarchy)
        {
            _gunBullet.SetActive(true);
        }

        if (_fireTriggerHuman.localPosition.z == 0f)
        {
            _fireTriggerHuman.DOLocalMoveZ(-0.5f, _fireTimeCount)
                .OnComplete(() => _fireTriggerHuman.transform.DOLocalMoveZ(0f, _fireTimeCount));
        }
    }

    private void CollectHuman(GameObject collectedObject) //İnsanlara değip yok ettiğimiz ve paralarını aldığımız method
    {
        collectedObject.SetActive(false);
        EventManager.Instance.OnMoneyCollected();
    }
}