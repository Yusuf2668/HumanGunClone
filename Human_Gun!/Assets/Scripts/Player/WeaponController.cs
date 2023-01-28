using DG.Tweening;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private LayerMask _stoneLayerMask;
    [SerializeField] private GameObject _mainPlayer;

    private float _timeCount;
    private void Awake() => _timeCount = 0;

    public bool LookAtTarget(Transform rayOrigin)
    {
        var hitStone = HitRaycast(rayOrigin);
        if (hitStone)
        {
            _mainPlayer.transform.DOLookAt(hitStone.transform.position, 0f, AxisConstraint.Y);
            return true;
        }
        _mainPlayer.transform.DORotate(Vector3.zero, 0.2f);
        return false;
    }

    private GameObject HitRaycast(Transform rayOrigin)
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, Vector3.forward, out hit, 5, _stoneLayerMask))
        {
            return hit.transform.gameObject;
        }

        return null;
    }
}