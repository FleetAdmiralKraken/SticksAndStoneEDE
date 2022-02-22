using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class DoorScale : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private Transform doorFeetTransform = null;
        [SerializeField] private DoorScaleWall doorScaleWall = null;

        [Header("Specifications")]
        [SerializeField] private float distanceBeforeScaleChange = 2f;

        //Declare privates
        private bool show = false;

        #endregion

        #region Update

        private void Update() {
            //Check
            if (show) {
                //Scale door
                ScaleDoor();
                //Run door scale wall update
                doorScaleWall.DoorScaleWallUpdate();
            }
        }

        private void ScaleDoor() {
            //Declare
            float distance = Vector3.Distance(doorFeetTransform.position, PublicStatics.gameManager.cjCharacterController.feetTransform.position);
            Vector3 scale = Vector3.one;
            //Check
            if (distance < distanceBeforeScaleChange) {
                //Set
                scale = new Vector3(distance, distance, distance) / distanceBeforeScaleChange;
            }
            //Change scale as getting closer
            transform.localScale = scale;
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