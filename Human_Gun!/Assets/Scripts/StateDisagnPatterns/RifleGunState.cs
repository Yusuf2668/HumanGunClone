using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleGunState : WeaponState
{
    [SerializeField] private float _fireTimeCount;
    public override WeaponState RunState()
    {
        return base.Run(_fireTimeCount);
    }
}