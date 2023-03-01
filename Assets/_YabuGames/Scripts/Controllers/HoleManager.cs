using System.Collections.Generic;
using UnityEngine;

namespace _YabuGames.Scripts.Controllers
{
    public class HoleManager : MonoBehaviour
    {
        [SerializeField] private float standardDistance;
        [SerializeField] private float holeSize = 1;
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private new MeshCollider collider;
        [SerializeField] private Transform fake3D;
        [SerializeField] private Vector3 fakeHoleIncreaseRate;

        private Mesh _mesh;
        private readonly List<int> _verticesIndex = new List<int>();
        private readonly List<Vector3> _offset = new List<Vector3>();
        private readonly List<float> _yPosList = new List<float>();
        private List<float> _height = new List<float>();


        private void Start()
        {
            _mesh = meshFilter.mesh;
            for (int i = 0; i < _mesh.vertices.Length; i++)
            {
                var distance = Vector3.Distance(transform.position, _mesh.vertices[i]);
                
                if (distance<=standardDistance)
                {
                    _verticesIndex.Add(i);
                    _offset.Add(_mesh.vertices[i]-transform.position);
                }
            }
            var vertices = _mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                _height.Add(vertices[i].y);
            }

            for (int i = 0; i < _offset.Count; i++)
            {
                _yPosList.Add(_offset[i].y);
            }
        }

        public void SizeUp(float value)
        {
            holeSize += value;
        }

        private void LateUpdate()
        {
            
            var vertices = _mesh.vertices;
            for (int i = 0; i < _verticesIndex.Count; i++)
            {
                vertices[_verticesIndex[i]] = transform.position + _offset[i] * holeSize;
            }

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].y = _height[i];
            }

            var holeScale = new Vector3(holeSize * fakeHoleIncreaseRate.x, fake3D.localScale.y,
                holeSize * fakeHoleIncreaseRate.z);
            fake3D.localScale = holeScale;
            _mesh.vertices = vertices;
            meshFilter.mesh = _mesh;
            collider.sharedMesh = _mesh;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color=Color.red;
            Gizmos.DrawWireSphere(transform.position,standardDistance);
        }
    }
}