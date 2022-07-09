using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.GameObject
{
    /// <summary>
    /// Disable GameObject delayed by time.
    /// </summary>        
    public class DisabledByTime : MonoBehaviour
    {
        public float time;

        /// <summary>
        /// Start timer to disable this GameObject.
        /// </summary>        
        public void startTimer()
        {
            StartCoroutine("Disabling");
        }

        IEnumerator Disabling()
        {
            yield return new WaitForSeconds(time);

            this.gameObject.SetActive(false);
        }
    }
}