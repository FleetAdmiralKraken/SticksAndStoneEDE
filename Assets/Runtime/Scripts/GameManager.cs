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
            //Check
            if (!PublicStatics.initializeOnce) {
                //Initialize game saving
                PublicStatics.InitializeGameSaving();
                //Set
                TextUIs[0].TheNumber = PublicStatics.gameData.Rocks;
                TextUIs[1].TheNumber = PublicStatics.gameData.Metals;
                TextUIs[2].TheNumber = PublicStatics.gameData.Souls;
                //Update text on screen
                UpdateTextOnScreen();
                //Set
                PublicStatics.initializeOnce = true;
            }
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

        private void UpdateTextOnScreen() {
            //Loop
            for (int i = 0; i < TextUIs.Length; i++) {
                //Declare
                string theNumber = TextUIs[i].TheNumber.ToString();
                //Update text on screen index
                UpdateTextOnScreenIndex(i, theNumber);
            }
        }

        public void UpdateTextOnScreenIndex(int index, string theNumber) {
            //Update UI
            TextUIs[index].TheText.text = theNumber;
            //Update craft
            TextUIs[index].CraftText.text = theNumber;
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
            public TextMeshProUGUI CraftText = null;
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