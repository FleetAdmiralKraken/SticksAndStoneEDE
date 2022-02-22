using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.cringejam.sticksandstones {

    public class StickBehavior : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private Transform floatingSphereTransform = null;

        [Header("Specifications")]
 
        [SerializeField] private float rotationSpeed = 25f;
        [SerializeField] private EnemiesAndDistances[] enemiesAndDistances = null;
        [SerializeField] private LayerMask layerMask = ~0;

        [Header("Attacking")]
        [SerializeField] private int meeleDamage = 10;
        [SerializeField] private int rangedDamage = 5;

        //Declare privates
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

        [Serializable]
        private class EnemiesAndDistances {
            //Declare
            public string Tag = string.Empty;
            public float DistanceToRaycast = 0f;
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
            //Declare
            Transform enemy = null;
            //Raycast
            if (Physics.Raycast(floatingSphereTransform.position, floatingSphereTransform.forward, out RaycastHit raycastHit, float.MaxValue, layerMask)) {
                //Loop
                for (int i = 0; i < enemiesAndDistances.Length; i++) {
                    //Check tag
                    if (raycastHit.transform.CompareTag(enemiesAndDistances[i].Tag)) {
                        //Check distance
                        if (raycastHit.distance <= enemiesAndDistances[i].DistanceToRaycast) {
                            //Set
                            enemy = raycastHit.transform;
                        }
                        //Exit
                        break;
                    }
                }
            }
            //Check
            if (enemy != null) {
                //Declare
                Vector3 enemyDirection = enemy.parent.GetChild(0).position - transform.position;
                //Rotate towards
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(enemyDirection), GetRotationSpeed());
                //Check for mouse
                CheckForMouse();
            } else {

                //Rotate towards
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, originalLocalRotation, GetRotationSpeed());
            }
        }

        private void CheckForMouse() {
            //Check for left click
            if (Input.GetMouseButtonDown(0)) {
                //Shoot enemy
                Debug.Log("Enemy melee attack");
            } else if (Input.GetMouseButtonDown(1)) { //Check for right click
                Debug.Log("Enemy ranged attack");
            }
        }

        private float GetRotationSpeed() {
            //Return
            return rotationSpeed * Time.deltaTime;
        }

        #endregion

    }

}
