using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.cringejam.sticksandstones {

    public class CJCharacterController : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private CJCamera cjCamera = null;
        [SerializeField] private Rigidbody rigidbody = null;
        public Transform feetTransform = null;

        [Header("Specifications")]
        [SerializeField] private Vector2 velocitySpeed = Vector2.one;
        [SerializeField] private Vector2 rotationSpeed = Vector2.one;
        [SerializeField] private float runMultipler = 1.25f;

        #endregion

        #region Public functions

        public void CJCharacterControllerUpdate() {
            //Try to interact
            cjCamera.TryToInteract();
        }

        public void CJCharacterControllerLateUpdate() {
            //Camera late update
            transform.localRotation = Quaternion.Euler(0f, cjCamera.ApplyXRotationAndGetYRotation(rotationSpeed), 0f);
        }

        public void CJCharacterControllerFixedUpdate() {
            //Declare
            Vector2 diagonal = new Vector2(PublicStatics.inputCache.keyboardAxes.floatAxes[0], PublicStatics.inputCache.keyboardAxes.floatAxes[1]);
            //Check
            if (diagonal.magnitude > 1f) {
                //Reduce speed
                diagonal.Normalize();
                diagonal *= (velocitySpeed.x / velocitySpeed.y);
            }
            //Check to increase run speed
            if (PublicStatics.inputCache.runPressed) {
                //Increase
                diagonal *= runMultipler;
            }
            //Set velocity
            Vector3 forward = transform.forward * diagonal.y * velocitySpeed.y;
            Vector3 right = transform.right * diagonal.x * velocitySpeed.x;
            //Add force to move
            rigidbody.AddForce(forward + right);
        }

        #endregion

    }

}