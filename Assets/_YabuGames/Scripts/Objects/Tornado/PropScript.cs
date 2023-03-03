using System;
using _YabuGames.Scripts.Interfaces;
using UnityEngine;

namespace _YabuGames.Scripts.Objects.Tornado
{
    public class PropScript : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable gate))
            {
                gate.Shoot();
                Destroy(gameObject);
            }
        }
    }
}
