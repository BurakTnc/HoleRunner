using System.Collections.Generic;
using _YabuGames.Scripts.Objects;
using UnityEngine;
using UnityEngine.Serialization;

namespace _YabuGames.Scripts.Managers
{
    public class PoolManager : MonoBehaviour
    {
        public static PoolManager Instance;
        
    [Header("                               // Set Particles Stop Action To DISABLE //")]
    [Space(20)]
        [SerializeField] private List<GameObject> projectiles = new List<GameObject>();
        [SerializeField] private List<GameObject> secondParticle = new List<GameObject>();
        [SerializeField] private List<GameObject> thirdParticle = new List<GameObject>();
        [SerializeField] private List<GameObject> fourthParticle = new List<GameObject>();

        
        private void Awake()
        {
            #region Singleton

            if (Instance != this && Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            #endregion
            
        }

        public void GetProjectile(Vector3 desiredPos,int range)
        {
            var temp = projectiles[0];
            projectiles.Remove(temp);
            temp.transform.position = desiredPos;
            temp.SetActive(true);
            temp.GetComponent<ProjectileScript>().SetRange(range);
            projectiles.Add(temp);
            
        }
        public void GetSecondParticle(Vector3 desiredPos)
        {
            var temp = secondParticle[0];
            secondParticle.Remove(temp);
            temp.transform.position = desiredPos;
            temp.SetActive(true);
            secondParticle.Add(temp);
        }
        public void GetThirdParticle(Vector3 desiredPos)
        {
            var temp = thirdParticle[0];
            thirdParticle.Remove(temp);
            temp.transform.position = desiredPos;
            temp.SetActive(true);
            thirdParticle.Add(temp);
        }
        public void GetFourthParticle(Vector3 desiredPos)
        {
            var temp = fourthParticle[0];
            fourthParticle.Remove(temp);
            temp.transform.position = desiredPos;
            temp.SetActive(true);
            fourthParticle.Add(temp);
        }
    }
}
