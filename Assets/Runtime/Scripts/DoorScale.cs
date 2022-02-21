using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class DoorScale : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private Transform characterFeetTransform = null;

        [Header("Specifications")]
        [SerializeField] private float distanceBeforeScaleChange = 2f;
        [SerializeField] private float distanceMultipler = 0.25f;

        #endregion

        #region Update

        private void Update() {
            //Declare
            float distance = Vector3.Distance(transform.position, characterFeetTransform.position);
            Vector3 scale = Vector3.one;
            //Check
            if (distance < distanceBeforeScaleChange) {;
                //Set
                scale = new Vector3(distance, distance, distance) * distanceMultipler;
            }
            //Change scale as getting closer
            transform.localScale = scale;
        }

        #endregion

    }

}