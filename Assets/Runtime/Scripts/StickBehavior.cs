using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class StickBehavior : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Specifications")]
        [SerializeField] private float enemyDistanceToRotate = 1f;
        [SerializeField] private float rotationSpeed = 25f;

        //Declare privates
        private List<TransformAndDistance> enemiesWithinDistance = new List<TransformAndDistance>();
        private Quaternion originalLocalRotation = Quaternion.identity;

        #endregion

        #region Classes

        private class TransformAndDistance {
            //Declare
            public Transform TheTransform = null;
            public float Distance = 0f;
            //Constructor
            public TransformAndDistance(Transform theTransform, float distance) {
                //Set
                TheTransform = theTransform;
                Distance = distance;
            }
        }

        #endregion

        #region Start

        private void Start() {
            //Set
            originalLocalRotation = transform.rotation;
        }

        #endregion

        #region Update

        private void Update() {

            Debug.Log(Vector3.Distance(PublicStatics.gameManager.enemies[0].position, transform.position));

            //Declare
            Transform enemyClosest = GetClosestEnemyFromList();
            //Check if not null
            if (enemyClosest != null) {
                //Declare
                Vector3 enemyDirection = enemyClosest.position - transform.position;
                //Rotate towards
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(enemyDirection), GetRotationSpeed());
            } else {
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, originalLocalRotation, GetRotationSpeed());
            }
        }

        private float GetRotationSpeed() {
            //Return
            return rotationSpeed * Time.deltaTime;
        }

        private Transform GetClosestEnemyFromList() {
            //Add enemies within distance
            AddEnemiesWithinDistance();
            //Declare
            Transform enemyTransform = null;
            //Check
            if (enemiesWithinDistance.Count > 1) {
                //Declare
                float closestDistance = enemiesWithinDistance[0].Distance;
                //Set
                enemyTransform = enemiesWithinDistance[0].TheTransform;
                //Loop
                for (int i = 1; i < PublicStatics.gameManager.enemies.Length; i++) {
                    //Check
                    if (closestDistance > enemiesWithinDistance[i].Distance) {
                        //Set
                        enemyTransform = enemiesWithinDistance[i].TheTransform;
                    }
                }
            } else if (enemiesWithinDistance.Count == 1) {
                //Set
                enemyTransform = enemiesWithinDistance[0].TheTransform;
            }
            //Empty list
            enemiesWithinDistance.Clear();
            //Return
            return enemyTransform;
        }

        private void AddEnemiesWithinDistance() {
            //Loop
            for (int i = 0; i < PublicStatics.gameManager.enemies.Length; i++) {
                //Declare
                float distance = Vector3.Distance(PublicStatics.gameManager.enemies[i].position, PublicStatics.gameManager.cjCharacterController.transform.position);
                //Check within
                if (distance <= enemyDistanceToRotate) {
                    //Add
                    enemiesWithinDistance.Add(new TransformAndDistance(PublicStatics.gameManager.enemies[i], distance));
                }
            }
        }

        #endregion

    }

}
