using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private BoxCollider _boxCollider;

    private void Awake() => _boxCollider = gameObject.GetComponent<BoxCollider>();


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(nameof(StringType.Tags.Player)))
        {
            _boxCollider.isTrigger = true;
            transform.DOScale(Vector3.zero, 0.1f)
                .OnComplete(() => EventManager.Instance.OnMoneyCollected());
        }
    }
}