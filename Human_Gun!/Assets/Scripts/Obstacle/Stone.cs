using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class Stone : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _stoneText;
    [SerializeField] protected Transform _moneys;

    protected int _stoneNumber;

    private void Awake() => _stoneNumber = int.Parse(_stoneText.text);

    protected void ShakeScale()
    {
        transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 0.1f)
            .OnComplete(() => transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f));
    }

    protected void LowerTheStoneNumber()
    {
        _stoneNumber--;
        if (_stoneNumber <= 0)
        {
            DropMoneys();
            DisableStone();
        }
        _stoneText.text = _stoneNumber.ToString();
    }

    protected void DisableStone()
    {
        transform.gameObject.SetActive(false);
    }

    protected void DropMoneys()
    {
        if (_moneys)
        {
            var moneysPos = _moneys.position;
            _moneys.SetParent(null);
            _moneys.DOJump(
                new Vector3(_moneys.position.x, moneysPos.y, moneysPos.z + 1), 1,
                1,
                0.5f);
        }
    }
}