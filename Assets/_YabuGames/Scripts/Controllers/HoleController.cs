using System.Collections.Generic;
using UnityEngine;

namespace _YabuGames.Scripts.Controllers
{
    public class HoleController : MonoBehaviour
    {
        public float standartDistance;
        
        
        [SerializeField] private MeshFilter meshFilter;
        [SerializeField] private new SphereCollider collider;

        private Mesh _mesh;
        private List<int> _verticesIndex = new List<int>();
        private readonly List<Vector3> _offset = new List<Vector3>();
        

    }
}