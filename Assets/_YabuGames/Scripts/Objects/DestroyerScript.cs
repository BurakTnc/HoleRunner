using System;
using _YabuGames.Scripts.Controllers;
using UnityEngine;

namespace _YabuGames.Scripts.Objects
{
    public class DestroyerScript : MonoBehaviour
    {
        [SerializeField] private float holeSizeUpRate = .5f;
        [SerializeField] private HoleManager holeManager;

        private void Awake()
        {
            //_holeManager = transform.root.GetComponent<HoleManager>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Prop"))
            {
                collision.gameObject.tag = "Untagged";
                Destroy(collision.gameObject,3);
                holeManager.SizeUp(holeSizeUpRate);
            }
        }
    }
}