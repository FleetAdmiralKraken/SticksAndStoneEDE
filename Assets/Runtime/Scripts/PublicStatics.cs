using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace com.cringejam.sticksandstones {

    public static class PublicStatics {

        #region Declares

        //Declare publics
        public static GameManager gameManager = null;
        public static InputCache inputCache = null;
        public static GameData gameData = new GameData();
        public static bool initializeOnce = false;

        //Declare privates
        private static string persistentDataPath = string.Empty; //C:/Users/UserName/AppData/LocalLow/DefaultCompany/GameName/GameData.dat
        private const string GAME_DATA_PATH = "/GameData.dat";

        #endregion

        #region Classes

        [Serializable]
        public class GameData {
            //Declare
            public int Rocks = 0;
            public int Metals = 0;
            public int Souls = 0;
            public int Stick = 0;
            //Constructor
            public GameData() {
            }
        }

        #endregion

        #region Public functions

        public static float RangeMapping(float a, float b, float x, float y, float z) {
            //Return
            return Mathf.Lerp(a, b, Mathf.InverseLerp(x, y, z));
        }

        public static void InitializeGameSaving() {
            //Set
            persistentDataPath = Application.persistentDataPath + GAME_DATA_PATH;
            //Check if file exists
            if (File.Exists(persistentDataPath)) {
                //Load data
                LoadData();
            } else {
                //Save data
                SaveData();
            }
        }

        private static void LoadData() {
            //Declare
            BinaryFormatter bnfBinaryFormatter = new BinaryFormatter();
            FileStream fsmFileStream = File.Open(persistentDataPath, FileMode.Open);
            gameData = (GameData)bnfBinaryFormatter.Deserialize(fsmFileStream);
            //Close
            fsmFileStream.Close();
        }

        public static void SaveData() {
            //Declare
            BinaryFormatter bnfBinaryFormatter = new BinaryFormatter();
            FileStream fsmFileStream = File.Create(Application.persistentDataPath + GAME_DATA_PATH);
            //Serialize
            bnfBinaryFormatter.Serialize(fsmFileStream, gameData);
            //Close
            fsmFileStream.Close();
        }

        #endregion

    }

}
