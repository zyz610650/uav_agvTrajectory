using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; //if you are using unity below 5.3 remove this line
using UnityEngine.UI;
/// <summary>
/// This script controls the mai menu buttons
/// </summary>

namespace MadFireOn
{
    public class MainMenuManager : MonoBehaviour
    {

        //ref to our game scene
        public string playScene;
        //ref to our game link
        public string gameLink = "Link to your game";
        public string moreGames = "Link to the page which contain all games";

        public Text titleText;

        //ref to music button
        [SerializeField]
        private Image musicIcon; // 1 is for on and 0 is for off
                                 //ref to music button sprites
        [SerializeField]
        private Sprite[] musicIconSprite;
        private AudioSource btnSound;
        public Button adsBtn;//ref to ads button

        void Awake()
        {
            //at start we chech for music status and we decide to play music or not and dependingly we choose button sprite
            if (GameManager.instance.isMusicOn)
            {
                musicIcon.sprite = musicIconSprite[1];
                AudioListener.volume = 1;
            }
            else
            {
                musicIcon.sprite = musicIconSprite[0];
                AudioListener.volume = 0;
            }
        }

        // Use this for initialization
        void Start()
        {
            btnSound = GetComponent<AudioSource>();
            //AdMobScript.instance.ShowBanner(); //use this line of code to show banner ads 
        }

        // Update is called once per frame
        void Update()
        {
            //after the purchase of no ads we dot want ads button to be interactable so we deactivate it
            if (GameManager.instance.noAds == true)
            {
                adsBtn.interactable = false;
            }

        }

        //when button is pressed we do following stuff
        public void PlayButton()
        {
            btnSound.Play();
            GameManager.instance.isGameOver = false;
            SceneManager.LoadScene(playScene);
            //if you are using unity below 5.3 use below code
            //Application.LoadLevel(playScene);
        }

        //when button is pressed we do following stuff
        public void LeaderboardBtn()
        {
            btnSound.Play();
            GooglePlayInGame.instance.OpenLeaderboardsScore();
        }

        public void RateBtn()
        {
            btnSound.Play();
            Application.OpenURL(gameLink);
        }

        //when button is pressed we do following stuff
        public void SoundBtn()
        {
            btnSound.Play();
            if (GameManager.instance.isMusicOn)
            {
                AudioListener.volume = 0;
                musicIcon.sprite = musicIconSprite[1];
                GameManager.instance.isMusicOn = false;
                GameManager.instance.Save();
            }
            else
            {
                AudioListener.volume = 1;
                musicIcon.sprite = musicIconSprite[0];
                GameManager.instance.isMusicOn = true;
                GameManager.instance.Save();
            }
        }

        public void MoreGamesBtn()
        {
            btnSound.Play();
            Application.OpenURL(moreGames);
        }

        public void NoAdsBtn()
        {
            btnSound.Play();
            //Purchaser.instance.BuyNoAds();

        }


    }

}//namespace