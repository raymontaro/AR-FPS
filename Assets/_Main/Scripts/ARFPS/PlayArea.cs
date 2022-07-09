using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARFPS
{
    public class PlayArea : MonoBehaviour
    {        
        // Start is called before the first frame update
        void Start()
        {
            PopulateTarget();
            GameManager.Instance.ChangeState(GameState.Play);
        }

        private void OnDestroy()
        {
            GameManager.Instance.ChangeState(GameState.Scan);
        }

        /// <summary>
        /// Populate all target.
        /// </summary>
        private void PopulateTarget()
        {
            for(int i = 0; i < GameManager.Instance.targetCount; i++)
            {
                Target target = Instantiate(GameManager.Instance.targetPrefab, this.transform).GetComponent<Target>();

                if (!GameManager.Instance.targets.Contains(target))
                {
                    GameManager.Instance.AddTarget(target);                    
                }
            }
        }
    }
}