using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class Rotate : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Specifications")]
        [SerializeField] private Vector3 speed = new Vector3(0f, 1f, 0f);

        #endregion

        #region Update

        private void Update() {
            //Rotate
            transform.rotation *= Quaternion.Euler(speed * Time.deltaTime);
        }

        #endregion

    }

}
