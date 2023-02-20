using System;
using System.Collections.Generic;
using UnityEngine;

namespace _YabuGames.Scripts.Controllers
{
    public class HoleManager : MonoBehaviour
    {
        public float standardDistance;
        public float holeSize = 1;
        
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private new MeshCollider collider;

        private Mesh _mesh;
        private List<int> _verticesIndex = new List<int>();
        private readonly List<Vector3> _offset = new List<Vector3>();

        private void Awake()
        {
            _mesh = meshFilter.mesh;
            
        }

        private void Start()
        {
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                var distance = Vector3.Distance(transform.position, _mesh.vertices[i]);
                
                if (distance<=standardDistance)
                {
                    _verticesIndex.Add(i);
                    _offset.Add(_mesh.vertices[i]-transform.position);
                }
            }
        }

        private void LateUpdate()
        {
            var vertices = _mesh.vertices;

            for (int i = 0; i < _verticesIndex.Count; i++)
            {
                vertices[_verticesIndex[i]] = transform.position + _offset[i] * holeSize;
            }

            _mesh.vertices = vertices;
            meshFilter.mesh = _mesh;
            collider.sharedMesh = _mesh;
        }
    }
}