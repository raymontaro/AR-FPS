using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARFPS
{
    public class Target : MonoBehaviour
    {        
        [SerializeField] private int maxHealth = 100;

        private int health = 100;

        private void OnDestroy()
        {
            GameManager.Instance.RemoveTarget(this);
        }

        /// <summary>
        /// Get health max value.
        /// </summary>       
        /// <returns>Returns maxHealth value.</returns>
        public int GetMaxHealthValue()
        {
            return maxHealth;
        }

        /// <summary>
        /// Get current health value.
        /// </summary>       
        /// <returns>Returns health Value.</returns>
        public int GetCurrentHealthValue()
        {
            return health;
        }

        /// <summary>
        /// Set health to Max value.
        /// </summary>        
        public void MaxHealth()
        {
            health = maxHealth;            
        }

        /// <summary>
        /// Reduce target health by damage amount.
        /// </summary>
        /// <param name="damage">Amount od damage to decrease health.</param>                
        public void ReduceHealth(int damage)
        {
            health -= damage;
            GameManager.Instance.ReduceHealthOfTarget(this);
            if (health <= 0)
            {
                health = 0;
                Despawn();
            }
        }

        /// <summary>
        /// Despawn this target.
        /// </summary>                
        public void Despawn()
        {
            GameManager.Instance.DespawnTarget(this);                        
        }

        #region Test Function
        [ContextMenu("ReduceHealth")]
        public void TestReduceHealth()
        {
            ReduceHealth(30);
        }
        #endregion
    }
}