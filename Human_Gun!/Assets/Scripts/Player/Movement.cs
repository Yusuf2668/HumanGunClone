using System;
using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;

    private Rigidbody _rigidbody;
    private BoxCollider _myCollider;
    private Vector3 _currentPos;

    private bool _canRun;

    void Update()
    {
        if (_canRun)
        {
            PlayerForwardRun();
            if (Input.GetMouseButton(0))
            {
                PlayerMovement();
            }
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.TriggerObstacle += PlayerObstacleCollision;
        EventManager.Instance.FinishGame += StartStopPlayer;
        EventManager.Instance.LoseGame += StartSetCanRunFonction;
        _rigidbody = GetComponent<Rigidbody>();
        _myCollider = GetComponent<BoxCollider>();
        _canRun = true;
    }

    private void OnDisable()
    {
        EventManager.Instance.TriggerObstacle -= PlayerObstacleCollision;
        EventManager.Instance.FinishGame -= StartStopPlayer;
        EventManager.Instance.LoseGame -= StartSetCanRunFonction;
    }

    private void PlayerObstacleCollision()
    {
        _rigidbody.velocity = Vector3.up * Time.deltaTime * playerType.playerJumpPower;
    }

    private void PlayerForwardRun()
    {
        _currentPos = transform.position;
        _currentPos += Vector3.forward * (Time.deltaTime * playerType.playerRunSpeed);
        transform.position = _currentPos;
    }

    private void PlayerMovement()
    {
        _currentPos += Vector3.right *
                       (Input.GetTouch(0).deltaPosition.x * Time.deltaTime * playerType.playerMoveSpeed);
        ClampCurrentPosition();
    }

    private void ClampCurrentPosition()
    {
        _currentPos = new Vector3(Mathf.Clamp(_currentPos.x, -1.5f, 1.5f), _currentPos.y, _currentPos.z);
        transform.position = _currentPos;
    }

    private void StartSetCanRunFonction()
    {
        StartCoroutine(SetCanRunFalse());
    }

    private void StartStopPlayer()
    {
        StartCoroutine(StopPlayer());
    }

    IEnumerator StopPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;
    }


    IEnumerator SetCanRunFalse()
    {
        yield return new WaitForSeconds(0.5f);
        _canRun = false;
        _myCollider.isTrigger = true;
    }
}