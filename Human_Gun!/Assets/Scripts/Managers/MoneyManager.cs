using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _finishPanelMoneyText;

    public int _moneyBonusCount;

    private void Start()
    {
        _moneyBonusCount = 0;
        _moneyText.text = PlayerPrefs.GetInt(nameof(StringType.PlayerPrefs.moneyPrefs), 0).ToString();
    }

    private void OnEnable()
    {
        EventManager.Instance.TriggerBonus += AddBonus;
        EventManager.Instance.AddMoney += AddMoney;
        EventManager.Instance.FinishGame += SetFinishPanelMoney;
    }

    private void OnDisable()
    {
        EventManager.Instance.TriggerBonus -= AddBonus;
        EventManager.Instance.AddMoney -= AddMoney;
        EventManager.Instance.FinishGame -= SetFinishPanelMoney;
    }

    private void AddMoney()
    {
        var money = PlayerPrefs.GetInt(nameof(StringType.PlayerPrefs.moneyPrefs));
        money += 10;
        _moneyText.text = money.ToString();
        PlayerPrefs.SetInt(nameof(StringType.PlayerPrefs.moneyPrefs), money);
    }

    private void SetFinishPanelMoney()
    {
        var money = PlayerPrefs.GetInt(nameof(StringType.PlayerPrefs.moneyPrefs));
        money *= _moneyBonusCount;
        _finishPanelMoneyText.text = money.ToString();
        PlayerPrefs.SetInt(nameof(StringType.PlayerPrefs.moneyPrefs), money);
    }

    private void AddBonus()
    {
        _moneyBonusCount++;
    }
}