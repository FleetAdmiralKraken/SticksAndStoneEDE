using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class CJCamera : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Specifications")]
        [SerializeField] private bool invertMouseY = false;
        [SerializeField] private Vector2 cameraXMinMax = new Vector2(-90f, 90f);

        //Declare privates
        private Vector2 cameraAxis = Vector2.zero;

        #endregion

        #region Public functions

        public float ApplyXRotationAndGetYRotation(Vector2 rotationSpeed) {
            //Store axis
            cameraAxis.x += GetMouseY() * rotationSpeed.y;
            cameraAxis.y += PublicStatics.inputCache.mouseAxes.floatAxes[0] * rotationSpeed.x;
            //Clamp
            cameraAxis.x = Mathf.Clamp(cameraAxis.x, cameraXMinMax.x, cameraXMinMax.y);
            //Set local rotation
            transform.localRotation = Quaternion.Euler(cameraAxis.x, 0f, 0f);
            //Return
            return cameraAxis.y;
        }

        private float GetMouseY() {
            //Return
            return invertMouseY ? PublicStatics.inputCache.mouseAxes.floatAxes[1] : -PublicStatics.inputCache.mouseAxes.floatAxes[1];
        }

        #endregion

    }

}