using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace MadFireOn
{
    public class SplashScreen : MonoBehaviour
    {

        public string menuScene = "MainMenu";

        // Use this for initialization
        void Start()
        {
            StartCoroutine(ChangeScene());
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator ChangeScene()
        {
            yield return new WaitForSeconds(0.5f);

            SceneManager.LoadScene(menuScene);

            //use below code for unity below 5.3
            //Application.LoadLevel(menuScene);
        }
    }
}//namespace