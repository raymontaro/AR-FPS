using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.UI
{
    /// <summary>
    /// Follow target in 3d Space.
    /// Can add offset value.
    /// </summary>        
    public class Follow3dObject : MonoBehaviour
    {
        public Vector3 offset;
        public Transform target;

        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            if (target != null)
            {
                Vector3 targetPos = target.position + offset;
                Vector3 pos = cam.WorldToScreenPoint(targetPos);
                transform.position = pos;
            }
        }

        /// <summary>
        /// Set the target to follow.
        /// </summary>
        /// <param name="newTarget">Transform of the new target.</param>   
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}