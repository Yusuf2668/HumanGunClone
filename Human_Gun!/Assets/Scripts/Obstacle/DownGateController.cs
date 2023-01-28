using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownGateController : MonoBehaviour
{
    [SerializeField] private int _gateCount;

    [SerializeField] private GameObject _upGate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(nameof(StringType.Tags.Player)))
        {
            _upGate.SetActive(false);
            for (int i = 0; i < _gateCount; i++)
            {
                EventManager.Instance.OnDropHuman();
            }
        }
    }
}