using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class Floating : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private Transform floatingSphereTransform = null;

        [Header("Specifications")]
        [SerializeField] private float speed = 5f;

        //Declare privates
        private Vector3 nextLocalPosition = Vector3.zero;

        #endregion

        #region Start

        private void Start() {
            //Create next local position
            CreateNextLocalPosition();
        }

        private void CreateNextLocalPosition() {
            //Set
            nextLocalPosition = floatingSphereTransform.localPosition + (Random.insideUnitSphere * floatingSphereTransform.localScale.x);
        }

        #endregion

        #region Update

        private void Update() {
            //Move towards
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextLocalPosition, speed * Time.deltaTime);
            //Check
            if (transform.localPosition == nextLocalPosition) {
                //Create next local position
                CreateNextLocalPosition();
            }
        }

        #endregion

    }

}