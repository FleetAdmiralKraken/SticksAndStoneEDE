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

        [Header("Meele Attacking Animation")]
        [SerializeField] private Transform stickLocalRotatorTransform = null;
        [SerializeField] private float rotateForwardSpeed = 10f;

        [Header("Range Attacking Animation")]
        [SerializeField] private Transform stickLocalTransform = null;
        [SerializeField] private Transform stickRangedBackTransform = null;
        [SerializeField] private Transform stickRangedForwardTransform = null;
        [SerializeField] private float forwardSpeed = 10f;

        //Declare privates
        private Quaternion originalLocalRotation = Quaternion.identity;
        private attackStates AttackState = attackStates.NotAttacking;
        private bool meeleForward = false;
        private bool rangedForward = false;

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

        #region Enumerators

        private enum attackStates {
            NotAttacking, Meele, Ranged
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
            //Check state
            switch (AttackState) {
                case attackStates.Meele:
                    Meeled();
                    break;
                case attackStates.Ranged:
                    Ranged();
                    break;
                default: //Not attacking
                    //Do nothing
                    break;
            }
            //Check
            if (enemy != null) {
                //Declare
                Vector3 enemyDirection = enemy.parent.GetChild(0).position - transform.position;
                //Rotate towards
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(enemyDirection), GetRotationSpeed());
                //Check for mouse
                CheckForMouse(enemy);
            } else {
                //Rotate towards
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, originalLocalRotation, GetRotationSpeed());
            }
        }

        private void Meeled() {
            //Check
            if (meeleForward) {
                //Rotate
                stickLocalRotatorTransform.localRotation =
                    Quaternion.RotateTowards(stickLocalRotatorTransform.localRotation, Quaternion.identity, GetStickMeeleSpeed());
                //Check
                if (stickLocalRotatorTransform.localRotation == Quaternion.identity) {
                    //Change
                    AttackState = attackStates.NotAttacking;
                }
            } else {
                //Rotate
                stickLocalRotatorTransform.localRotation = 
                    Quaternion.RotateTowards(stickLocalRotatorTransform.localRotation, Quaternion.Euler(-180f, 0f, 0f), GetStickMeeleSpeed());
                //Check
                if (stickLocalRotatorTransform.localRotation == Quaternion.Euler(-180f, 0f, 0f)) {
                    //Set
                    meeleForward = true;
                }
            }
        }

        private float GetStickMeeleSpeed() {
            //Return
            return rotateForwardSpeed * Time.deltaTime;
        }

        private void Ranged() {
            //Check
            if (rangedForward) {
                //Go backward
                stickLocalTransform.localPosition =
                    Vector3.MoveTowards(stickLocalTransform.localPosition, stickRangedForwardTransform.localPosition, GetStickRangedSpeed());
                //Check
                if (stickLocalTransform.localPosition == stickRangedForwardTransform.localPosition) {
                    //Change
                    AttackState = attackStates.NotAttacking;
                }
            } else {
                //Go backward
                stickLocalTransform.localPosition = 
                    Vector3.MoveTowards(stickLocalTransform.localPosition, stickRangedBackTransform.localPosition, GetStickRangedSpeed());
                //Check
                if (stickLocalTransform.localPosition == stickRangedBackTransform.localPosition) {
                    //Change
                    rangedForward = true;
                }
            }
        }

        private float GetStickRangedSpeed() {
            //Return
            return forwardSpeed * Time.deltaTime;
        }

        private void CheckForMouse(Transform collisionModel) {
            //Check for left click
            if (Input.GetMouseButtonDown(0)) {
                //Check
                if (AttackState == attackStates.NotAttacking) {

                    DoAttackOf(collisionModel, meeleDamage);

                    //Shoot enemy
                    Debug.Log("Enemy melee attack");
                    //Change state
                    AttackState = attackStates.Meele;

                    //Set
                    meeleForward = false;
                }
            } else if (Input.GetMouseButtonDown(1)) { //Check for right click
                //Check
                if (AttackState == attackStates.NotAttacking) {

                    DoAttackOf(collisionModel, rangedDamage);

                    //Change state
                    AttackState = attackStates.Ranged;
                    Debug.Log("Enemy ranged attack");
                    //Set
                    rangedForward = false;
                }
            }
        }

        private void DoAttackOf(Transform collisionModel, int damage) {
            //Declare
            Enemy enemy = collisionModel.GetComponent<Enemy>();

            enemy.OnDamageDealt(damage);

            //Deal damage
            enemy.fullHealth -= damage;

            //Check health
            if (enemy.fullHealth <= 0) {
                GameObject droppable = Instantiate(PublicStatics.gameManager.TextUIs[0].Prefab);
                droppable.transform.position = collisionModel.parent.GetChild(0).position;
                Destroy(collisionModel.parent.gameObject);
            }
        }

        private float GetRotationSpeed() {
            //Return
            return rotationSpeed * Time.deltaTime;
        }

        #endregion

    }

}
