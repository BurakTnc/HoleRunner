using System;
using _YabuGames.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace _YabuGames.Scripts.Objects
{
    public class ProjectileScript : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private Vector3 reduceRate;
        [SerializeField] private Vector3 minimumSize;

        private Vector3 _startPos;
        private float _range = 10;

        private void Shoot()
        {
            _startPos = transform.position;
        }

        private void Update()
        {
            transform.position += Vector3.forward * (speed * Time.deltaTime);
            CalculateRange();
        }
        

        private void CalculateRange()
        {
            var distance = transform.position.z - _startPos.z;
            if (distance >= _range) 
            {
                gameObject.SetActive(false);
            }
        }

        public void SetRange(int newValue)
        {
            _range = newValue;
            Shoot();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable gate))
            {
                gate.Shoot();
            }
            
            if (other.gameObject.CompareTag("Prop"))
            {
                var obj = other.transform;
                var currentScale = obj.localScale;
                gameObject.SetActive(false);
                obj.DOShakeScale(.1f, Vector3.one*.3f, 5, 100, true);
                if (obj.localScale.magnitude>minimumSize.magnitude)
                {
                    obj.localScale -= reduceRate;
                }
            }

            
        }
    }
}