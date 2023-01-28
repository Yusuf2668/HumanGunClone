using System;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _playerAnimator;

    private void OnEnable()
    {
        EventManager.Instance.PlayerAnimatorTrigger += PlayAnim;
    }

    private void OnDisable()
    {
        EventManager.Instance.PlayerAnimatorTrigger -= PlayAnim;
    }

    public void PlayAnim(string animName)
    {
        _playerAnimator.SetTrigger(animName);
    }
}