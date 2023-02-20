using System;
using UnityEngine;

namespace _YabuGames.Scripts.Controllers
{
    public class HolePhysicsController : MonoBehaviour
    {
        [SerializeField] [Range(1f,1000f)] private float power = 1f;
        [SerializeField] [Range(-10f,10f)] private float upOrDown;
        [SerializeField] [Range(1f,10f)] private float forceRange = 1f;
        [SerializeField] private ForceMode forceMode;
        [SerializeField] private LayerMask layerMask;

        private void ApplyGravity(Vector3 gravitySource, float range, LayerMask layer)
        {
            Collider[] objs = Physics.OverlapSphere(gravitySource, range, layer);
            for (int i = 0; i < objs.Length; i++)
            {
                Rigidbody rbs = objs[i].GetComponent<Rigidbody>();
                var forceDirection = new Vector3(gravitySource.x, upOrDown, gravitySource.z) -
                                     objs[i].transform.position;
                rbs.AddForceAtPosition(power * forceDirection.normalized, gravitySource, forceMode);
            }
        }

        private void FixedUpdate()
        {
            ApplyGravity(transform.position,forceRange,layerMask);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position,forceRange);
        }
    }
}