using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARFPS
{
    public class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int amountToPool = 20;

        private List<GameObject> pooledObjects = new List<GameObject>();        

        // Start is called before the first frame update
        void Start()
        {
            for(int i = 0; i < amountToPool; i++)
            {
                GameObject go = Instantiate(projectilePrefab, this.transform);
                go.SetActive(false);
                pooledObjects.Add(go);
            }
        }

        /// <summary>
        /// Get available projectile from pool.
        /// </summary>       
        /// <returns>Return a projectile ready for launch.</returns>
        public GameObject GetPooledObject()
        {
            for(int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }

            return null;
        }
    }
}