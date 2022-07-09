using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARFPS
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int power = 30;
        [SerializeField] private float speed = 30f;
        private Rigidbody rb;
        private Camera cam;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            cam = Camera.main;
        }

        private void OnEnable()
        {
            if(rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }

            if(cam == null)
            {
                cam = Camera.main;
            }

            transform.position = cam.transform.position;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(cam.transform.forward * speed, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("target"))
            {
                other.GetComponent<Target>().ReduceHealth(power);
                this.gameObject.SetActive(false);
            }
        }
    }
}