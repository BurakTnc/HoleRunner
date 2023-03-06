using System;
using _YabuGames.Scripts.Signals;
using UnityEditor.PackageManager;
using UnityEngine;

namespace _YabuGames.Scripts.Controllers
{
    public class IdleMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float xPosClampMin,xPosClampMax, zPosClampMin, zPosClampMax;

        private bool _isGameRunning;

        private void Start()
        {
            CoreGameSignals.Instance.OnGameStart?.Invoke();
        }

        private void Move()
        {
            if (!_isGameRunning)
            {
                var xValue = SimpleInput.GetAxis("Horizontal");
                var zValue = SimpleInput.GetAxis("Vertical");
                var desiredPos = new Vector3(xValue * speed, 0, zValue * speed) * Time.deltaTime;
                transform.position += desiredPos;
            }
            ClampPosition();
        }

        private void ClampPosition()
        {
            var position = transform.position;
            position = new Vector3(Mathf.Clamp(position.x, xPosClampMin, xPosClampMax), position.y,
                Mathf.Clamp(position.z, zPosClampMin, zPosClampMax));
            transform.position = position;
        }

        private void Update()
        {
            Move();
        }
    }
}