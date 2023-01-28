using System;
using UnityEngine;

public class StringType : MonoBehaviour
{
    public enum Tags
    {
        Player,
        Obstacle,
        Human,
        Bullet,
        Gun,
        Stone,
        Bonus,
        BestScoreTable
    }
    public enum MaterialColorNames
    {
        Black,
        Blue,
        Red,
        Yellow
    }
    

    public enum HumanAnimatorTriggers
    {
        Pose01,Pose02,Pose03
        
    }

    public enum PlayerPrefs
    {
        moneyPrefs,
        bestScore,
        bestScoreTableZPos,
        levelPrefs
    }
}
