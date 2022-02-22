using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class Door : MonoBehaviour {

        #region Declares

        //Declare serializables
        [SerializeField] private Transform doorFeetTransform = null;

        [Header("Specifications")]
        [SerializeField] private float doorOpenDistance = 5f;
        [SerializeField] private float openingAngle = 125f;
        [SerializeField] private float speed = 250f;

        //Declare privates
        private bool show = false;

        #endregion

        #region Update

        private void Update() {
            //Check
            if (show) {
                //Declare
                float distance = Vector3.Distance(PublicStatics.gameManager.cjCharacterController.feetTransform.position, doorFeetTransform.position);
                //Check distance
                if (distance <= doorOpenDistance) {
                    //Rotate towards
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0f, GetLocalYRotation(distance), 0f), GetSpeed());
                } else {
                    //Rotate towards
                    transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, GetSpeed());
                }
            }
        }

        private float GetSpeed() {
            //Return
            return speed * Time.deltaTime;
        }

        private float GetLocalYRotation(float distance) {
            //Return
            return PublicStatics.RangeMapping(0f, openingAngle, doorOpenDistance, 0f, distance);
        }

        #endregion

        #region Public functions

        public void SetToShow() {
            //Set
            show = true;
        }

        #endregion

    }

}