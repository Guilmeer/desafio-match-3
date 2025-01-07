using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public class ShakeObjectEffect : MonoBehaviour
    {
        [SerializeField] Vector3 initialPosition;

        public float shakeDuration = 0f;

        public float shakeAmount = 0.7f;
        public float decreaseSpeed = 1.0f;

        private void OnDisable()
        {
            GameEvents.Instance.onMatchTilesDone -= AddShakeDuration;

        }

        private void Start()
        {
            GameEvents.Instance.onMatchTilesDone += AddShakeDuration;

            initialPosition = transform.position;
        }

        private void AddShakeDuration(int matchAmount)
        {
            switch (matchAmount)
            {
                case 3:
                    shakeDuration = .2f;
                    shakeAmount = 0.7f;
                    break;
                case 4:
                    shakeDuration = .5f;
                    shakeAmount = 1f;
                    break;
                default:
                    shakeDuration = 1f;
                    shakeAmount = 1.3f;
                    break;
            }
        }

        void Update()
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = initialPosition + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = initialPosition;
            }
        }
    }
}
