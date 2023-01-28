using UnityEngine;

interface ICollectable
{
    void CollectForGun(Vector3 humanTargetPosition,Vector3 humanTargetRotation,Transform parentObject,string colorName,string animTrigger);
}