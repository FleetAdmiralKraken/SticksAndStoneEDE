using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

namespace com.cringejam.sticksandstones {

    public class GameManager : MonoBehaviour {

        #region Declares

        //Declare serializables
        [Header("Setup")]
        [SerializeField] private InputCache inputCache = null;
        public CJCharacterController cjCharacterController = null;
        [SerializeField] private CJCursorLock cjCursorLock = null;
        public Transform[] enemies = null;

        [Header("UI")]
        public TextUI[] TextUIs = null;
        [SerializeField] private GameObject gameCursor = null;
        public GameObject craftMenu = null;
        [SerializeField] private Button craftButton = null;

        #endregion

        #region Start

        private void Start() {
            //Set
            PublicStatics.gameManager = this;
            PublicStatics.inputCache = inputCache;
            //Set cursor lock
            cjCursorLock.SetCursorLock();
            //Disable
            craftMenu.SetActive(false);
            //Disable
            craftButton.enabled = false;
        }

        #endregion

        #region Classes

        [Serializable]
        public class TextUI {
            //Declare
            public TextMeshProUGUI TheText = null;
            public string Tag = string.Empty;
            public GameObject Prefab = null;
            [NonSerialized] public int TheNumber = 0;
        }

        #endregion

        #region Update

        private void Update() {
            //Cache the input
            inputCache.Cache();
            //Run character controller update
            cjCharacterController.CJCharacterControllerUpdate();
            //Check to show craft menu
            CheckToShowCraftMenu();
        }

        private void CheckToShowCraftMenu() {
            //Check for key press
            if (Input.GetKeyDown(KeyCode.Escape)) {
                //Show active of menu
                craftMenu.SetActive(!craftMenu.activeSelf);
                //Show active of game cursor
                gameCursor.SetActive(!gameCursor.activeSelf);
                //Set cursor lock mode
                cjCursorLock.SetCursorLock(GetCursorLockMode(), GetCursorVisible());
                //Set time scale
                SetTimeScale();
            }
        }

        private CursorLockMode GetCursorLockMode() {
            //Check
            if (cjCursorLock.GetCursorLockMode() == CursorLockMode.Locked) {
                //Return
                return CursorLockMode.None;
            } else {
                //Return
                return CursorLockMode.Locked;
            }
        }

        private bool GetCursorVisible() {
            //Return
            return cjCursorLock.GetCursorLockMode() == CursorLockMode.Locked;
        }

        private void SetTimeScale() {
            //Check to set time scale
            if (craftMenu.activeSelf) {
                //Set time scale
                Time.timeScale = 0f;
            } else {
                //Reset time scale
                Time.timeScale = 1f;
            }
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