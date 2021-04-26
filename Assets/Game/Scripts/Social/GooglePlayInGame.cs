using UnityEngine;
using System.Collections;
//using GooglePlayGames; //uncomment this after importing google services and implementing then
using UnityEngine.SocialPlatforms;

namespace MadFireOn
{
    public class GooglePlayInGame : MonoBehaviour
    {

        public static GooglePlayInGame instance;

        public string LEADERBOARDS_SCORE = "Copy the leaderboard code here";

        void Awake()
        {
            MakeInstance();
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


        // Use this for initialization
        void Start()
        {
            //uncomment this after importing google services and implementing then
            //PlayGamesPlatform.Activate();
            //Social.localUser.Authenticate((bool success) =>
            //{
            //    if (success)
            //    {
            //        //InitializeAchievements();
            //    }
            //});
        }

        void OnLevelWasLoaded()
        {
            ReportScore(GameManager.instance.hiScore);
        }

        //use this method for button
        public void OpenLeaderboardsScore()
        {
            //uncomment this after importing google services and implementing then
            //if (Social.localUser.authenticated)
            //{
            //    PlayGamesPlatform.Instance.ShowLeaderboardUI(LEADERBOARDS_SCORE);
            //}
        }

        void ReportScore(int score)
        {
            //uncomment this after importing google services and implementing then
            //if (Social.localUser.authenticated)
            //{
            //    Social.ReportScore(score, LEADERBOARDS_SCORE, (bool success) => { });
            //}
        }
    }
}//namespace