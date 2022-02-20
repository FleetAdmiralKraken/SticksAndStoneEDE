using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class CJCursorLock : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Specifications")]
        [SerializeField] private CursorLockMode startCursorLockMode = CursorLockMode.Locked;
        [SerializeField] private bool isVisible = false;

        #endregion

        #region Start

        private void Start() {
            //Set cursor lock
            SetCursorLock(startCursorLockMode, isVisible);
        }

        #endregion

        #region Public functions

        public void SetCursorLock(CursorLockMode cursorLockMode = CursorLockMode.Locked, bool visible = false) {
            //Set
            Cursor.lockState = cursorLockMode;
            //Set visible
            Cursor.visible = visible;
        }

        #endregion

    }

}