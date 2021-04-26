using UnityEngine;
using System.Collections;
//using admob; //uncomment after importing package 
/// <summary>
/// use this package for admob https://drive.google.com/open?id=0B1zuy1Q88VZra0RvdjhQbU1ackk
/// use this script on the scene which loads only one time when game start like the splash screen
/// </summary>

namespace MadFireOn
{

    public class AdMobScript : MonoBehaviour
    {

        public static AdMobScript instance;

        [SerializeField]
        private string bannerID = "";
        [SerializeField]
        private string videoID = "";

        void Awake()
        {
            MakeSingleton();
        }

        void MakeSingleton()
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
            //For detailed info check this link: https://www.youtube.com/watch?v=khlROw-PfNE
            //here we initialize admob
#if UNITY_EDITOR
            Debug.Log("Working");
#elif UNITY_ANDROID
        //Admob.Instance().initAdmon(bannerID,videoID);
        //Admob.Instance().setTesting(true); //use this code only for testing time
        //Admob.Instance().loadInterstitial();
#endif
        }

        // Update is called once per frame
        void Update()
        {

        }


        //..........................................................................Admob
        public void ShowBanner()
        {
#if UNITY_EDITOR
            Debug.Log("Working");
#elif UNITY_ANDROID
        //Admob.Instance().showBannerRelative(AdSize.Banner,AdPosition.BOTTOM_CENTER , 5);
#endif
        }

        public void RemoveBanner()
        {
#if UNITY_EDITOR
            Debug.Log("Working");
#elif UNITY_ANDROID
        //Admob.Instance().removeBanner();
#endif
        }

        public void ShowVideo()
        {
#if UNITY_EDITOR
            Debug.Log("Working");
#elif UNITY_ANDROID
        /*
        if (Admob.Instance().isInterstitialReady())
        {
            Admob.Instance().showInterstitial();
        }
        */
#endif
        }

    }

} //namespace