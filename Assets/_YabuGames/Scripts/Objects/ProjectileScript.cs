using System;
using DG.Tweening;
using UnityEngine;

namespace _YabuGames.Scripts.Objects
{
    public class ProjectileScript : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private Vector3 reduceRate;
        [SerializeField] private Vector3 minimumSize;

        private void Update()
        {
            transform.position += Vector3.forward * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Prop"))
            {
                var obj = other.transform;
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