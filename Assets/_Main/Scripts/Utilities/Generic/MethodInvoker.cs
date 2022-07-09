using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utilities.Generic
{
    /// <summary>
    /// Added method will be Invoke.
    /// </summary>        
    public class MethodInvoker : MonoBehaviour
    {
        public UnityEvent awakeEvent;
        public UnityEvent onEnableEvent;
        public UnityEvent startEvent;
        public UnityEvent updateEvent;
        public UnityEvent onDestroyEvent;

        private void Awake()
        {
            awakeEvent.Invoke();
        }

        private void OnEnable()
        {
            onEnableEvent.Invoke();
        }

        // Start is called before the first frame update
        void Start()
        {
            startEvent.Invoke();
        }

        // Update is called once per frame
        void Update()
        {
            updateEvent.Invoke();
        }

        private void OnDestroy()
        {
            onDestroyEvent.Invoke();
        }
    }
}