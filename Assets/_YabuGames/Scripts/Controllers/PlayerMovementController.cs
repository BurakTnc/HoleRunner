using System;
using System.Collections;
using _YabuGames.Scripts.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace _YabuGames.Scripts.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        public static bool OnTrap;

        [SerializeField] private float xPosClamp, speedSideways, speed;
        
        private Vector3 _pos1, _pos2;
        private bool _holding;
        private bool _isGameRunning;
        
        private void Awake()
        {
            _isGameRunning = _holding = OnTrap = false;
        }
        #region Subscribtions
       private void OnEnable()
       {
           Subscribe();
       }

       private void OnDisable()
       {
           UnSubscribe();
       }

       private void Subscribe()
       {
           CoreGameSignals.Instance.OnGameStart += OnGameStart;
           CoreGameSignals.Instance.OnLevelWin += OnGameEnd;
           CoreGameSignals.Instance.OnLevelFail += OnGameEnd;
       }

       private void UnSubscribe()
       {
           CoreGameSignals.Instance.OnGameStart -= OnGameStart;
           CoreGameSignals.Instance.OnLevelWin -= OnGameEnd;
           CoreGameSignals.Instance.OnLevelFail -= OnGameEnd;
       }

       #endregion

       private IEnumerator Start()
       {
           yield return new WaitForSeconds(1);
           //transform.position = new Vector3(0, 0, -91);
           CoreGameSignals.Instance.OnGameStart?.Invoke();
       }

       private void Update()
        {
            Movement();
        }

       private void OnGameStart()
       {
           _isGameRunning = true;
       }

       private void OnGameEnd()
       {
           _isGameRunning = false;
       }
       
       private void Movement()
        {
            if(!_isGameRunning) return;
            
            if (!OnTrap)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _pos1 = GetMousePosition();
                    _holding = true;
                }

                if (Input.GetMouseButton(0) && _holding)
                {
                    _pos2 = GetMousePosition();
                    var delta = _pos1 - _pos2;
                    _pos1 = _pos2;
                    transform.position += new Vector3(-delta.x * speedSideways * Time.deltaTime, 0,
                        speed * Time.deltaTime);
                }
                else
                {
                    transform.position += transform.forward * (speed * Time.deltaTime);
                }

                var position = transform.position;
                
                position = new Vector3(Mathf.Clamp(position.x, -xPosClamp, xPosClamp),
                    position.y, position.z);
                transform.position = position;
            }
        }

        private Vector2 GetMousePosition()
        {
            var pos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

            return pos;
        }

    }
}
