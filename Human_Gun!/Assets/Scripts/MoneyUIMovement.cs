using System;
using DG.Tweening;
using UnityEngine;

public class MoneyUIMovement : MonoBehaviour
{
    [SerializeField] private RectTransform _moneyImage;

    private Vector3 _firstPos;

    private void Awake() => _firstPos = gameObject.GetComponent<RectTransform>().position;

    private void OnEnable()
    {
        transform.DOMove(_moneyImage.position, 1f)
            .OnComplete(() => AddMoney());
    }

    private void AddMoney()
    {
        EventManager.Instance.OnAddMoney();
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        transform.position = _firstPos;
    }
}