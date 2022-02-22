using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class FloorScaleSpikes : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private Transform floorFeetTransform = null;
        [SerializeField] private FloorScaleSpikesFloor floorScaleSpikesFloor = null;

        [Header("Specifications")]
        [SerializeField] private float distanceBeforeScaleChange = 2f;

        //Declare privates
        private bool show = false;
        private bool locked = false;

        #endregion

        #region Start

        private void Start() {
            //Scale
            transform.localScale = Vector3.zero;
        }

        #endregion

        #region Update

        private void Update() {
            //Check
            if (show) {
                //Scale trap
                ScaleTrap();
                //Run floor scale spikes floor update
                floorScaleSpikesFloor.FloorScaleSpikesFloorUpdate();
            }
        }

        private void ScaleTrap() {
            //Declare
            float distance = Vector3.Distance(floorFeetTransform.position, PublicStatics.gameManager.cjCharacterController.feetTransform.position);
            Vector3 scale = Vector3.zero;
            //Check
            if (locked) {
                //Set
                scale = Vector3.one;
            } else {
                //Check
                if (distance < distanceBeforeScaleChange) {
                    //Check distance
                    if (distance < 1f) {
                        //Set
                        scale = Vector3.one;
                        //Lock
                        locked = true;
                    } else {
                        //Declare
                        float mappedScale = PublicStatics.RangeMapping(1f, 0f, 0f, distanceBeforeScaleChange, distance);
                        //Set
                        scale = new Vector3(mappedScale, mappedScale, mappedScale);
                    }
                }
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