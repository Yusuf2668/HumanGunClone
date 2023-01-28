using UnityEngine;

public class GunState : WeaponState
{
    [SerializeField] private float _fireTimeCount;
    
    public override WeaponState RunState()
    {
        return base.Run(_fireTimeCount);
    }
}