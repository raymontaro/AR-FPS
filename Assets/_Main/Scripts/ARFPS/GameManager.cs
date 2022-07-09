using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

namespace ARFPS
{    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        #region public variable
        [Header("States")]
        public GameState gameState = GameState.Scan;

        [Header("public References")]
        public GameObject targetPrefab;

        [Header("public Values")]
        public int targetCount = 3;

        [Header("Dynamic")]
        public List<Target> targets = new List<Target>();
        #endregion

        #region Serialized variable
        [Header("private References")]
        [SerializeField] private ARSession arSession;
        [SerializeField] private GameObject scanInstructionUI;
        [SerializeField] private GameObject playScreenUI;
        [SerializeField] private GameObject winScreenUI;
        [SerializeField] private GameObject targetHealthUIPrefab;
        [SerializeField] private Transform targetHealthUIParent;
        [SerializeField] private ProjectilePool projectilePool;

        [Header("private Values")]
        [SerializeField] private Vector2 targetPositionRadius = new Vector2(4.5f, 4.5f);
        [SerializeField] private int restartTime = 5;
        #endregion

        #region private variable
        private List<TargetHealthUI> targetHealthUIs = new List<TargetHealthUI>();
        #endregion

        #region unity functions
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }        
        #endregion

        #region public functions
        /// <summary>
        /// Change the current gamestate.
        /// </summary>
        /// <param name="newGameState">Value for the new game state.</param>        
        public void ChangeState(GameState newGameState)
        {
            gameState = newGameState;

            scanInstructionUI.SetActive(gameState == GameState.Scan);
            playScreenUI.SetActive(gameState == GameState.Play);
            winScreenUI.SetActive(gameState == GameState.Finished);

            switch (gameState)
            {
                case GameState.Play:                    
                    InitGame();
                    break;
                case GameState.Finished:                    
                    StartCoroutine("RestartGame");
                    break;
            }
        }

        /// <summary>
        /// Fire projectile.
        /// </summary>
        public void Fire()
        {
            GameObject p = projectilePool.GetPooledObject();
            if (p != null)
            {
                p.SetActive(true);
            }
        }

        /// <summary>
        /// Check if all target is death then finished the game.
        /// </summary>    
        [ContextMenu("CheckEndGame")]
        public void CheckEndGame()
        {
            int targetCount = 0;

            foreach(Target t in targets)
            {
                if (t.gameObject.activeSelf)
                {
                    targetCount++;
                }
            }

            if (targetCount <= 0)
            {
                ChangeState(GameState.Finished);
            }
        }

        /// <summary>
        /// Add new target to target list.
        /// </summary>
        /// <param name="newTarget">Target to add.</param>        
        public void AddTarget(Target newTarget)
        {
            targets.Add(newTarget);

            TargetHealthUI healthUI = Instantiate(targetHealthUIPrefab, targetHealthUIParent).GetComponent<TargetHealthUI>();
            targetHealthUIs.Add(healthUI);
            healthUI.Init(newTarget);
        }

        /// <summary>
        /// Reduce health of target from target list.
        /// </summary>
        /// <param name="targetToReduce">Target to reduce.</param>             
        public void ReduceHealthOfTarget(Target targetToReduce)
        {
            int index = targets.IndexOf(targetToReduce);            
            targetHealthUIs[index].UpdateUI();            
        }

        /// <summary>
        /// Despawn a target from target list.
        /// </summary>
        /// <param name="targetToDespawn">Target to despawn.</param>             
        public void DespawnTarget(Target targetToDespawn)
        {
            int index = targets.IndexOf(targetToDespawn);
            targetToDespawn.gameObject.SetActive(false);            
            targetHealthUIs[index].gameObject.SetActive(false);
            CheckEndGame();
        }

        /// <summary>
        /// Remove a target from target list.
        /// </summary>
        /// <param name="targetToRemove">Target to Remove.</param>           
        public void RemoveTarget(Target targetToRemove)
        {
            int index = targets.IndexOf(targetToRemove);
            Destroy(targetHealthUIs[index].gameObject);
            targetHealthUIs.RemoveAt(index);
            Destroy(targetToRemove.gameObject);
            targets.RemoveAt(index);
        }
        #endregion

        #region private functions
        /// <summary>
        /// Initialize the game.
        /// </summary>                
        private void InitGame()
        {
            AllTargetToMaxHealth();
            RandomizedAllTargetPosition();
            EnableAllTarget();
        }        

        /// <summary>
        /// Set all target to it's max health value.
        /// </summary>  
        private void AllTargetToMaxHealth()
        {
            foreach (Target t in targets)
            {
                t.MaxHealth();
            }

            foreach (TargetHealthUI t in targetHealthUIs)
            {
                t.UpdateUI();
            }
        }

        /// <summary>
        /// Randomized all target positions.
        /// </summary>  
        private void RandomizedAllTargetPosition()
        {
            foreach(Target t in targets)
            {
                float xPos = Random.Range(-targetPositionRadius.x, targetPositionRadius.x);
                float zPos = Random.Range(-targetPositionRadius.y, targetPositionRadius.y);

                t.transform.localPosition = new Vector3(xPos, 0, zPos);
            }
        }

        /// <summary>
        /// Enabling all target.
        /// </summary>  
        private void EnableAllTarget()
        {
            foreach (Target t in targets)
            {
                t.gameObject.SetActive(true);
            }

            foreach (TargetHealthUI t in targetHealthUIs)
            {
                t.gameObject.SetActive(true);
            }
        }
        #endregion

        #region Coroutines
        /// <summary>
        /// Restart game with delay.
        /// </summary>  
        IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(restartTime);
            
            arSession.Reset();            

            ///if we want to restart without rescan, we can use this script instead of arSession reset
            //ChangeState(GameState.Play);
        }
        #endregion
    }
}