using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cringejam.sticksandstones {

    public class GameManager : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private InputCache inputCache = null;
        public CJCharacterController cjCharacterController = null;
        [SerializeField] private CJCursorLock cjCursorLock = null;
        public Transform[] enemies = null;

        #endregion

        #region Start

        private void Start() {
            //Set
            PublicStatics.gameManager = this;
            PublicStatics.inputCache = inputCache;
            //Set cursor lock
            cjCursorLock.SetCursorLock();
        }

        #endregion

        #region Update

        private void Update() {
            //Cache the input
            inputCache.Cache();
        }

        #endregion

        #region Late update

        private void LateUpdate() {
            //Run character late update
            cjCharacterController.CJCharacterControllerLateUpdate();
        }

        #endregion

        #region Fixed update

        private void FixedUpdate() {
            //Run character fixed update
            cjCharacterController.CJCharacterControllerFixedUpdate();
        }

        #endregion

    }

}