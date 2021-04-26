using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// This script controls all the in game menu stuffs like buttons , scores , etc
/// </summary>

namespace MadFireOn
{
    public class InGameGuiManager : MonoBehaviour
    {

        public Text score, points;//in game score and points ref
        public Text goScore;//game over menu score ref
        public Text bestScore;
        public string facebookprofile;
        //ref to game scene
        public string playScene;
        //ref to main menu scenea
        public string menuScene;
        public string gameURL = "Link address of your game";
        public GameObject grid, scoreandPoint;
        //ref to game over panel animator
        public Animator gameOverPanel;

        private AudioSource btnSound;

        // Use this for initialization
        void Start()
        {
            //uncomment this if you are implementing banner ads at the main menu because we dont want banner ads to be shown on game scene
            //AdMobScript.instance.RemoveBanner(); 

            btnSound = GetComponent<AudioSource>();
            score.text = "SCORE " + GameManager.instance.currentScore;
            points.text = "" + 0;
            //check for the music
            if (GameManager.instance.isMusicOn)
            {
                AudioListener.volume = 1;
            }
            else
            {
                AudioListener.volume = 0;
            }
        }

        // Update is called once per frame
        void Update()
        {
            HiScore();

            GameManager.instance.points = Mathf.Abs(GameManager.instance.currentScore / 10);
            points.text = "" + GameManager.instance.points;
            //we keep updating the score each frame
            score.text = "SCORE " + GameManager.instance.currentScore;

            //when game is over we play the game over panel animation 
            if (GameManager.instance.isGameOver == true)
            {
                GameManager.instance.points = Mathf.Abs(GameManager.instance.currentScore / 10);
                GameManager.instance.Save();
                scoreandPoint.SetActive(false);
                score.enabled = false;
                grid.SetActive(false);
                StartCoroutine(waitGameOverPanel());
            }
        }

        //functions of button when they are pressed
        public void RetryBtn()
        {
            btnSound.Play();
            GameManager.instance.isGameOver = false;
            SceneManager.LoadScene(playScene);
        }

        public void HomeBtn()
        {
            btnSound.Play();
            GameManager.instance.isGameOver = false;
            SceneManager.LoadScene(menuScene);
        }

        public void RateBtn()
        {
            btnSound.Play();
            Application.OpenURL(gameURL);
        }

        /// <summary>
        /// Note: When you create apk file remember to make "write access = External(SD Card)" which you will
        /// find in player setting under other sattings 
        /// </summary>
        public void ShareBtn()
        {
            btnSound.Play();
            ShareScript.instance.ButtonShare();
        }

        //we want our panel to animate
        IEnumerator waitGameOverPanel()
        {
            yield return new WaitForSeconds(0.6f);
            goScore.text = "" + GameManager.instance.currentScore;
            bestScore.text = "BEST:" + GameManager.instance.hiScore;
            gameOverPanel.Play("GameOverSlide");

        }

        //we keep track of our hi score 
        void HiScore()
        {
            //we check it the previous hiscore is less than current score and the we renew our hiscore
            if (GameManager.instance.hiScore < GameManager.instance.currentScore)
            {
                GameManager.instance.hiScore = GameManager.instance.currentScore;
                GameManager.instance.Save();
            }
        }

        public void OnFacebookClicked()
        {
            Application.OpenURL("https://www.facebook.com/n/?" + facebookprofile);
        }

        public void RewardAds()
        {
            UnityAds.instance.ShowRewardedAd();
        }
    }
}//namespace