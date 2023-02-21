using System;
using _YabuGames.Scripts.Managers;
using UnityEngine;

namespace _YabuGames.Scripts.Controllers
{
    public class HoleShooterController : MonoBehaviour
    {
        public float fireRate = .3f;

        [SerializeField] private Transform firePosition;

        private float _timer;

        private void Fire()
        {
            if (_timer <= 0) 
            {
                PoolManager.Instance.GetProjectile(firePosition.position);
                _timer += fireRate;
            }

            _timer -= Time.deltaTime;
            _timer = Mathf.Clamp(_timer, 0, 2);
        }

        private void Update()
        {
            Fire();
        }
    }
}