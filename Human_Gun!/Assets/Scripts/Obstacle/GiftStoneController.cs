using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class GiftStoneController : Stone
{
    [SerializeField] private Transform _player;

    private void OnParticleCollision(GameObject other)
    {
        var collisionObject = other.gameObject;
        if (collisionObject.CompareTag(nameof(StringType.Tags.Bullet)))
        {
            LowerGiftStoneNumber();
            ShakeScale();
        }
    }

    private void LowerGiftStoneNumber()
    {
        _stoneNumber--;
        if (_stoneNumber <= 0)
        {
            MoveMoneyToPlay();
            DisableStone();
        }

        _stoneText.text = _stoneNumber.ToString();
    }

    private void MoveMoneyToPlay()
    {
        _moneys.SetParent(_player);
        _moneys.DOLocalJump(Vector3.zero, 1.5f, 1, 1f).OnComplete(() => EventManager.Instance.OnAddMoney());
        _moneys.DOScale(Vector3.zero, 1.2f);
    }
}