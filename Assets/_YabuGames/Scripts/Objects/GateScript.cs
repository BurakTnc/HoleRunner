using System;
using System.Globalization;
using _YabuGames.Scripts.Enums;
using _YabuGames.Scripts.Interfaces;
using _YabuGames.Scripts.Signals;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _YabuGames.Scripts.Objects
{
    public class GateScript : MonoBehaviour,IInteractable
    {
        [SerializeField] private GateMode gateMode;
        [SerializeField] private int rangeValue;
        [SerializeField] private float fireRate;
        [SerializeField] private TextMeshPro gateText;

        [Header("Colored Objects")] [SerializeField]
         private MeshRenderer[] renderers = new MeshRenderer[2];
         [SerializeField] private SpriteRenderer spriteRenderer;
         [Header("Materials")] [SerializeField] private Material greenMat;
         [SerializeField] private Material redMat;
         [SerializeField] private Sprite greenSprite, redSprite;

        private void Start()
        {
            SetText();
        }
        
        private void SetText()
        {
            switch (gateMode)
            {
                case GateMode.Range:
                    if (rangeValue > 0)
                    {
                        foreach (var meshRenderer in renderers)
                        {
                            meshRenderer.material.DOColor(greenMat.color,.5f);
                        }

                        spriteRenderer.sprite = greenSprite;
                        gateText.text = "+" + rangeValue;
                    }
                    else
                    {
                        foreach (var meshRenderer in renderers)
                        {
                            meshRenderer.material.DOColor(redMat.color, .5f);
                        }
                        spriteRenderer.sprite = redSprite;
                        gateText.text = rangeValue.ToString();
                    }
                    break;
                case GateMode.FireRate:
                    if (fireRate > 0)
                    {
                        gateText.text = "+" + fireRate;
                    }
                    else
                    {
                        gateText.text = fireRate.ToString(CultureInfo.CurrentCulture);
                    }
                    break;
                default:
                    break;
            }
            
            
        }

        private void UpdateGateStats()
        {
            switch (gateMode)
            {
                case GateMode.Range:
                    rangeValue++;
                    break;
                case GateMode.FireRate:
                    fireRate += 1f;
                    break;
                default:
                    break;
            }
            transform.DOShakeScale(.1f, Vector3.up*.1f, 1, 0, true);
            SetText();
        }

        private void GiveTheStats()
        {
            switch (gateMode)
            {
                case GateMode.Range:
                    CoreGameSignals.Instance.OnRangeChange?.Invoke(rangeValue);
                    break;
                case GateMode.FireRate:
                    CoreGameSignals.Instance.OnFireRateChange?.Invoke(fireRate);
                    break;
                default:
                    break;
            }

            transform.DOScale(Vector3.zero, .5f).SetEase(Ease.InBack);
        }

        public void Interact()
        {
            GiveTheStats();
        }

        public void Shoot()
        {
            UpdateGateStats();
        }
        
    }
}
