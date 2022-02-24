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

        [Header("Raycasting")]
        [SerializeField] private float interactDistance = 5f;
        [SerializeField] private LayerMask interactLayerMask = ~0;

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

        public void TryToInteract() {
            //Check for interact
            if (Input.GetKeyDown(KeyCode.E)) {
                //Raycast
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, interactLayerMask)) {
                    //Loop
                    for (int i = 0; i < PublicStatics.gameManager.TextUIs.Length; i++) {
                        //Check
                        if (raycastHit.transform.CompareTag(PublicStatics.gameManager.TextUIs[i].Tag)) {
                            //Add
                            PublicStatics.gameManager.TextUIs[i].TheNumber += 1;
                            //Check index
                            switch (i) {
                                default: //0
                                    PublicStatics.gameData.Rocks += 1;
                                    break;
                                case 1:
                                    PublicStatics.gameData.Metals += 1;
                                    break;
                                case 2:
                                    PublicStatics.gameData.Souls += 1;
                                    break;
                            }
                            //Save
                            PublicStatics.SaveData();
                            //Update text
                            PublicStatics.gameManager.UpdateTextOnScreenIndex(i, PublicStatics.gameManager.TextUIs[i].TheNumber.ToString());
                            //Destroy object
                            Destroy(raycastHit.transform.gameObject);
                            //Exit
                            break;
                        }
                    }
                }
            }
        }

        #endregion

    }

}