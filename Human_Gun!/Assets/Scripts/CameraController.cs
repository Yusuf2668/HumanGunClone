using System;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineTransposer _cinemachineTransposer;

    private float _cameraYOffest;
    private bool _finishGame;

    private void Awake()
    {
        _cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();
        _cinemachineTransposer = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        //takibi nıraktır losegame eventinden
    }

    private void Start()
    {
        _cameraYOffest = _cinemachineTransposer.m_FollowOffset.y;
    }

    private void OnEnable()
    {
        EventManager.Instance.FinishGame += FinishGame;
        EventManager.Instance.LoseGame += NotFollowPlayer;
    }

    private void OnDisable()
    {
        EventManager.Instance.FinishGame -= FinishGame;
        EventManager.Instance.LoseGame -= NotFollowPlayer;
    }

    void Update()
    {
        if (_finishGame && _cameraYOffest <= 6.5f)
        {
            MoveUp();
            _cameraYOffest += Time.deltaTime;
        }
    }

    private void FinishGame()
    {
        _finishGame = true;
    }

    private void NotFollowPlayer()
    {
        _cinemachineVirtualCamera.Follow = null;
    }

    private void MoveUp()
    {
        _cinemachineVirtualCamera.LookAt = _player;
        _cinemachineTransposer.m_FollowOffset = new Vector3(2.17f, _cameraYOffest, -4.570001f);
    }
}