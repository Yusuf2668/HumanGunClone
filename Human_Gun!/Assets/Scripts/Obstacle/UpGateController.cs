using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGateController : MonoBehaviour
{
    [SerializeField] private int _gateCount;

    [SerializeField] private GameObject _downGate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(nameof(StringType.Tags.Player)))
        {
            _downGate.SetActive(false);
            EventManager.Instance.OnAddHumanForWeapon(_gateCount);
        }
    }
}