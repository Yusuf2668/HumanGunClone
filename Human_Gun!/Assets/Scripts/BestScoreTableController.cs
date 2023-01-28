using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BestScoreTableController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private int _bestScore;
    
    private void Start()
    {
        SetStartPos();
    }
    
    public void UpdateBestScore()
    {
        _bestScore = PlayerPrefs.GetInt(nameof(StringType.PlayerPrefs.bestScore), 10);
        _bestScore += 10;
        var zPos = transform.localPosition.z;
        transform.DOLocalMoveZ(zPos + 3, 0.5f).OnComplete(() => _bestScoreText.text = _bestScore.ToString());
        PlayerPrefs.SetInt(nameof(StringType.PlayerPrefs.bestScore), _bestScore);
        PlayerPrefs.SetFloat(nameof(StringType.PlayerPrefs.bestScoreTableZPos), zPos);
    }

    private void SetStartPos()
    {
        _bestScore = PlayerPrefs.GetInt(nameof(StringType.PlayerPrefs.bestScore), 10);
        transform.localPosition = new Vector3(transform.position.x, transform.position.y,
            PlayerPrefs.GetFloat(nameof(StringType.PlayerPrefs.bestScoreTableZPos), 79f));
        _bestScoreText.text = _bestScore.ToString();
    }
}