using System;
using UnityEngine;
using DG.Tweening;

public class HumanController : MonoBehaviour, ICollectable
{
    [SerializeField] private HumanMaterials _humanMaterials;
    [SerializeField] private SkinnedMeshRenderer _myRenderer;

    private Animator _animator;
    private Material _selectedMaterial;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void CollectForGun(Vector3 humanTargetPosition, Vector3 humanTargetRotation, Transform parentObject,
        string colorName, string animTrigger)
    {
        switch (colorName)
        {
            case nameof(StringType.MaterialColorNames.Black):
                _selectedMaterial = _humanMaterials.black;
                break;
            case nameof(StringType.MaterialColorNames.Red):
                _selectedMaterial = _humanMaterials.red;
                break;
            case nameof(StringType.MaterialColorNames.Blue):
                _selectedMaterial = _humanMaterials.blue;
                break;
            case nameof(StringType.MaterialColorNames.Yellow):
                _selectedMaterial = _humanMaterials.yellow;
                break;
        }
        transform.SetParent(parentObject);
        _myRenderer.material = _selectedMaterial;
        transform.DOLocalJump(humanTargetPosition, 0.5f, 1, 0.5f);
        transform.DOLocalRotate(humanTargetRotation, 0.5f);
        _animator.SetTrigger(animTrigger);
        
    }
}