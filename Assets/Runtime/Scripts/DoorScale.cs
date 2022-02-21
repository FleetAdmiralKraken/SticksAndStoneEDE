using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class DoorScale : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private Transform doorFeetTransform = null;
        [SerializeField] private Transform characterFeetTransform = null;
        [SerializeField] private DoorScaleWall doorScaleWall = null;

        [Header("Specifications")]
        [SerializeField] private float distanceBeforeScaleChange = 2f;

        #endregion

        #region Update

        private void Update() {
            //Scale door
            ScaleDoor();
            //Run door scale wall update
            doorScaleWall.DoorScaleWallUpdate();
        }

        private void ScaleDoor() {
            //Declare
            float distance = Vector3.Distance(doorFeetTransform.position, characterFeetTransform.position);
            Vector3 scale = Vector3.one;
            //Check
            if (distance < distanceBeforeScaleChange) {
                ;
                //Set
                scale = new Vector3(distance, distance, distance) / distanceBeforeScaleChange;
            }
            //Change scale as getting closer
            transform.localScale = scale;
        }

        #endregion

    }

}