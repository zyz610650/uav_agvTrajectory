using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
/// <summary>
/// This script helps in saving and loading data in device
/// </summary>

namespace MadFireOn
{

    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;

        private GameData data;

        //data which is not stored on device but refered while game is on
        public bool isGameOver = false;
        public int currentScore;
        public bool spawnNextDots = true;

        //data to store on device
        public bool isGameStartedFirstTime;
        public bool isMusicOn;
        public int hiScore;
        public bool noAds;//when noAds is false we show ads and when its true we dont show it
        public int styleInd; //this is to change the style of falling blocks
        public bool[] styleUnlocked; //this is to keep ref which style is unlocked
        public int points; //this the currency required to unlock new block styles
                           //ref to the background music
        private AudioSource audio;

        void Awake()
        {
            MakeInstance();
            InitializeVariables();
        }

        void Start()
        {
            audio = GetComponent<AudioSource>();
        }

        void MakeInstance()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        //we initialize variables here
        void InitializeVariables()
        {
            //first we load any data is avialable
            Load();
            if (data != null)
            {
                isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            }
            else
            {
                isGameStartedFirstTime = true;
            }
            if (isGameStartedFirstTime)
            {
                //when game is started for 1st time on device we set the initial values
                isGameStartedFirstTime = false;
                hiScore = 0;
                isMusicOn = true;
                noAds = false;
                styleInd = 0;
                //we have only 3 types 0 is rock 1 is crystal and 2 is meteor
                styleUnlocked = new bool[3];
                //we want 1st to be unlocked and others locked
                styleUnlocked[0] = true;
                styleUnlocked[1] = false;
                styleUnlocked[2] = false;

                data = new GameData();

                //storing data
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setIsMusicOn(isMusicOn);
                data.setHiScore(hiScore);
                data.setNoAds(noAds);
                data.setStyleInd(styleInd);
                data.setPoints(points);
                data.setStyleUnlocked(styleUnlocked);

                Save();

                Load();

            }
            else
            {
                //getting data
                isGameStartedFirstTime = data.getIsGameStartedFirstTime();
                isMusicOn = data.getIsMusicOn();
                hiScore = data.getHiScore();
                noAds = data.getNoAds();
                styleInd = data.getStyleInd();
                points = data.getPoints();
                styleUnlocked = data.getStyleUnlocked();

            }
        }

        public void ResetGameManager()
        {
            //when game is started for 1st time on device we set the initial values
            isGameStartedFirstTime = false;
            hiScore = 0;
            isMusicOn = true;
            noAds = false;
            styleInd = 0;
            //we have only 3 types 0 is rock 1 is crystal and 2 is meteor
            styleUnlocked = new bool[3];
            //we want 1st to be unlocked and others locked
            styleUnlocked[0] = true;
            styleUnlocked[1] = false;
            styleUnlocked[2] = false;

            data = new GameData();

            //storing data
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setIsMusicOn(isMusicOn);
            data.setHiScore(hiScore);
            data.setNoAds(noAds);
            data.setStyleInd(styleInd);
            data.setPoints(points);
            data.setStyleUnlocked(styleUnlocked);

            Save();

            Load();
        }

        void Update()
        {//here we control the background music
            if (isGameOver == false && audio.isPlaying == false)
            {
                audio.Play();
            }
            else if (isGameOver == true)
            {
                audio.Stop();
            }
        }

        //method to save data
        public void Save()
        {
            FileStream file = null;

            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                file = File.Create(Application.persistentDataPath + "/GameData.dat");
                if (data != null)
                {
                    data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                    data.setHiScore(hiScore);
                    data.setIsMusicOn(isMusicOn);
                    data.setNoAds(noAds);
                    data.setStyleInd(styleInd);
                    data.setPoints(points);
                    data.setStyleUnlocked(styleUnlocked);

                    bf.Serialize(file, data);
                }
            }
            catch (Exception e)
            { }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        //method to load data
        public void Load()
        {
            FileStream file = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);//here we get saved file
                data = (GameData)bf.Deserialize(file);
            }
            catch (Exception e) { }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }
    }

    [Serializable]
    class GameData
    {
        private bool isGameStartedFirstTime;
        private bool isMusicOn;
        private int hiScore;
        private bool noAds;
        private int styleInd, points;
        public bool[] styleUnlocked;
        //is game started 1st time
        public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
        {
            this.isGameStartedFirstTime = isGameStartedFirstTime;
        }

        public bool getIsGameStartedFirstTime()
        {
            return isGameStartedFirstTime;
        }

        public void setNoAds(bool noAds)
        {
            this.noAds = noAds;
        }

        public bool getNoAds()
        {
            return noAds;
        }

        //music
        public void setIsMusicOn(bool isMusicOn)
        {
            this.isMusicOn = isMusicOn;
        }

        public bool getIsMusicOn()
        {
            return isMusicOn;
        }

        //hi score 
        public void setHiScore(int hiScore)
        {
            this.hiScore = hiScore;
        }

        public int getHiScore()
        {
            return hiScore;
        }

        //styleInd
        public void setStyleInd(int styleInd)
        {
            this.styleInd = styleInd;
        }

        public int getStyleInd()
        {
            return styleInd;
        }

        //style unlocked
        public void setStyleUnlocked(bool[] styleUnlocked)
        {
            this.styleUnlocked = styleUnlocked;
        }

        public bool[] getStyleUnlocked()
        {
            return styleUnlocked;
        }

        //points
        public void setPoints(int points)
        {
            this.points = points;
        }

        public int getPoints()
        {
            return points;
        }

    }
}//namespace