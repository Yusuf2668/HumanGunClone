using System;
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    #region BonusState

    public bool OnBonusState
    {
        get { return _onBonusState; }
        set { _onBonusState = value; }
    }

    private bool _onBonusState;

    #endregion

    private void Start()
    {
        _onBonusState = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(nameof(StringType.Tags.Obstacle)))
        {
            EventManager.Instance.OnTriggerObstacle();
        }

        if (other.CompareTag(nameof(StringType.Tags.Bonus)))
        {
            EventManager.Instance.OnTriggerBonus();
            _onBonusState = true;
        }

        if (other.CompareTag(nameof(StringType.Tags.BestScoreTable)))
        {
            other.gameObject.GetComponent<BestScoreTableController>().UpdateBestScore();
        }
    }
}