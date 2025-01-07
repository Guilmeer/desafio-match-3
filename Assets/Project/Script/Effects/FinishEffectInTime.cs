using System.Collections;
using UnityEngine;

namespace Gazeus.DesafioMatch3
{
    public class FinishEffectInTime : MonoBehaviour
    {
        public float disableAfter = 5.0f;

        private void OnEnable()
        {
            StartCoroutine(DisableAfterDelay());
        }

        // Coroutine to wait for a specified duration and then disable the game object
        private IEnumerator DisableAfterDelay()
        {
            yield return new WaitForSeconds(disableAfter);
            gameObject.SetActive(false);
        }
    }
}