using System;
using System.Collections.Generic;
using _YabuGames.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace _YabuGames.Scripts.Controllers
{
    public class HolePhysicsController : MonoBehaviour
    {
        [SerializeField] [Range(1f,1000f)] private float power = 1f;
        [SerializeField] [Range(-10f,10f)] private float upOrDown;
        [SerializeField] [Range(1f,10f)] private float forceRange = 1f;
        [SerializeField] private ForceMode forceMode;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Vector3 sizeMultiplier;
        [SerializeField] private Image progressBar;

        private List<GameObject> _flyingObjects = new List<GameObject>();
        private List<GameObject> _destroyedList = new List<GameObject>();
        
        private void ApplyGravity(Vector3 gravitySource, float range, LayerMask layer)
        {
            var objs = Physics.OverlapSphere(gravitySource, range, layer);
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i].TryGetComponent(out Rigidbody rbs))
                {
                    if (!_flyingObjects.Contains(objs[i].gameObject))
                    {
                        _flyingObjects.Add(objs[i].gameObject);
                    }

                    rbs.constraints = RigidbodyConstraints.None;
                    var forceDirection = new Vector3(gravitySource.x, upOrDown, gravitySource.z) -
                                         objs[i].transform.position;
                    rbs.AddForceAtPosition(power * forceDirection.normalized, gravitySource, forceMode);
                }
            }
        }

        private void CheckAltitude()
        {
            if(_flyingObjects.Count<1) return;
            
            foreach (var obj in _flyingObjects)
            {
                var height = obj.transform.position.y;
                if (height>=15)
                {
                    var objMass = obj.GetComponent<Rigidbody>().mass;
                    IncreaseStats(objMass);
                    obj.SetActive(false);
                    _destroyedList.Add(obj);
                }
                
            }

            if (_destroyedList.Count < 1) return;
            
            for (int i = 0; i < _destroyedList.Count; i++)
            {
                _flyingObjects.Remove(_destroyedList[i]);
            }
            _destroyedList.Clear();
            
        }

        private void IncreaseStats(float mass)
        {
            forceRange += mass / 100;
            power += mass;
            forceRange = Mathf.Clamp(forceRange, 3, 10);
            if (transform.localScale.x<3)
            {
                transform.localScale += sizeMultiplier;
            }

            progressBar.fillAmount += 0.005f;
            CameraController.Instance.IncreaseAngle();
        }

        private void FixedUpdate()
        {
            ApplyGravity(transform.position,forceRange,layerMask);
        }

        private void Update()
        {
            CheckAltitude();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color=Color.blue;
            Gizmos.DrawWireSphere(transform.position,forceRange);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable gate))
            {
                gate.Interact();
            }
        }
    }
}