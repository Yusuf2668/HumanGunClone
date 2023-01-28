using System;
using UnityEngine;

public class EventManager : MonoSingleton<EventManager>
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #region Obstacle Trigger

    public event Action TriggerObstacle;
    public void OnTriggerObstacle() => TriggerObstacle?.Invoke();
    

    #endregion

    #region CollisionStone

    public event Action CollisionStone;

    public void OnCollisionStone() => CollisionStone?.Invoke();

    #endregion

    #region HumanAnimator

    public event Action<string> PlayerAnimatorTrigger;
    public void OnPlayerAnimatorTrigger(string animName) => PlayerAnimatorTrigger?.Invoke(animName);

    #endregion

    #region CollisionHuman

    public event Action<GameObject> CollisionHuman;

    public void OnCollisionHuman(GameObject collisionHuman) => CollisionHuman?.Invoke(collisionHuman);

    #endregion

    #region CollectMoney

    public event Action MoneyCollected;

    public void OnMoneyCollected() => MoneyCollected?.Invoke();

    #endregion

    #region AddHumanForWeapon

    public event Action<int> AddHumanForWeapon;

    public void OnAddHumanForWeapon(int humanCount) => AddHumanForWeapon?.Invoke(humanCount);

    #endregion

    #region DropHuman

    public event Action DropHuman;

    public void OnDropHuman() => DropHuman?.Invoke();

    #endregion

    #region WinGame

    public event Action FinishGame;

    public void OnFinishGame() => FinishGame?.Invoke();

    #endregion

    #region TriggerBonus

    public event Action TriggerBonus;

    public void OnTriggerBonus() => TriggerBonus?.Invoke();

    #endregion

    #region LoseGame

    public event Action LoseGame;

    public void OnLoseGame() => LoseGame?.Invoke();

    #endregion

    #region AddMoney

    public event Action AddMoney;

    public void OnAddMoney() => AddMoney?.Invoke();

    #endregion

    #region LoadNextLevel

    public event Action LoadNextLevel;

    public void OnLoadNextLevel() => LoadNextLevel?.Invoke();

    #endregion

    #region LoadCurrentLevel

    public event Action LoadCurrentLevel;

    public void OnLoadCurrentLevel() => LoadCurrentLevel?.Invoke();

    #endregion
}