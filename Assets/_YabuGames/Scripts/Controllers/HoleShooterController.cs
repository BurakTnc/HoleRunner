using System;
using _YabuGames.Scripts.Managers;
using _YabuGames.Scripts.Signals;
using UnityEngine;

namespace _YabuGames.Scripts.Controllers
{
    public class HoleShooterController : MonoBehaviour
    {
        [SerializeField] private Transform firePosition;
        
        private float _fireRate = .6f;
        private float _timer;
        private int _range = 30;


        private void OnEnable()
        {
            CoreGameSignals.Instance.OnRangeChange += ChangeRange;
            CoreGameSignals.Instance.OnFireRateChange += ChangeFireRate;
        }

        private void OnDisable()
        {
            CoreGameSignals.Instance.OnRangeChange -= ChangeRange;
            CoreGameSignals.Instance.OnFireRateChange -= ChangeFireRate;
        }

        private void ChangeFireRate(float newRate)
        {
            if (newRate>0)
            {
                _fireRate -= newRate / 10;
            }
            else
            {
                _fireRate += Mathf.Abs(newRate) / 10;
            }
            
            _fireRate = Mathf.Clamp(_fireRate, 0.05f, 5);
        }

        private void ChangeRange(int newValue)
        {
            if (newValue>0)
            {
                _range += newValue * 10;
            }
            else
            {
                _range -= Mathf.Abs(newValue) * 10;
            }
            
            _range = Mathf.Clamp(_range, 10, 500);
        }

        private void Fire()
        {
            if (_timer <= 0) 
            {
                PoolManager.Instance.GetProjectile(firePosition.position,_range);
                _timer += _fireRate;
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