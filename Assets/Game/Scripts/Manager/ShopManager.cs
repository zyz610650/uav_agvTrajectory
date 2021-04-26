using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace MadFireOn
{
    public class ShopManager : MonoBehaviour
    {

        public Button shopBtn, shopCloseBtn, rockBtn, crystalBtn, meteorBtn, rewardAdsBtn;
        public GameObject buttonHolder, shopPanel;
        public Text titleText, pointText;

        // Use this for initialization
        void Start()
        {
            pointText.text = "" + GameManager.instance.points;

            shopBtn.GetComponent<Button>().onClick.AddListener(() => { ShopBtn(); });    //shop
            shopCloseBtn.GetComponent<Button>().onClick.AddListener(() => { CLoseShopBtn(); });    //close shop
            rockBtn.GetComponent<Button>().onClick.AddListener(() => { RockBtn(); });    //for rock style block
            crystalBtn.GetComponent<Button>().onClick.AddListener(() => { CrystalBtn(); });    //for crystal style block
            meteorBtn.GetComponent<Button>().onClick.AddListener(() => { MeteorBtn(); });    //for meteor style block
            rewardAdsBtn.GetComponent<Button>().onClick.AddListener(() => { RewardAdsBtn(); });    //reward
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.points < 100)
            {
                if (!GameManager.instance.styleUnlocked[1])
                {
                    crystalBtn.interactable = false;
                }

                if (!GameManager.instance.styleUnlocked[2])
                {
                    meteorBtn.interactable = false;
                }
            }
            else
            {
                crystalBtn.interactable = true;
                meteorBtn.interactable = true;
            }

            if (GameManager.instance.styleUnlocked[1])
            {
                crystalBtn.GetComponentInChildren<Text>().enabled = false;
            }

            if (GameManager.instance.styleUnlocked[2])
            {
                meteorBtn.GetComponentInChildren<Text>().enabled = false;
            }

        }


        public void ShopBtn()
        {
            titleText.enabled = false;
            buttonHolder.SetActive(false);
            shopPanel.SetActive(true);
        }

        public void CLoseShopBtn()
        {
            titleText.enabled = true;
            buttonHolder.SetActive(true);
            shopPanel.SetActive(false);
        }

        public void RewardAdsBtn()
        {

        }

        //shop buttons

        public void CrystalBtn()
        {
            //if the crystal is unlocked , in bool array the crystal is 1st element
            if (GameManager.instance.styleUnlocked[1])
            {
                if (GameManager.instance.styleInd != 0)
                {
                    GameManager.instance.styleInd = 0;
                    GameManager.instance.Save();
                }
            }
            else
            {
                if (GameManager.instance.points >= 100)
                {
                    GameManager.instance.points -= 100;
                    GameManager.instance.styleInd = 1;
                    GameManager.instance.styleUnlocked[1] = true;
                    GameManager.instance.Save();
                }
            }
            pointText.text = "" + GameManager.instance.points;
        }

        public void RockBtn()
        {
            if (GameManager.instance.styleInd != 0)
            {
                GameManager.instance.styleInd = 0;
                GameManager.instance.Save();
            }
        }

        public void MeteorBtn()
        {
            //if the meteor is unlocked (in bool array the meteor is 2nd element)
            if (GameManager.instance.styleUnlocked[2])
            {
                if (GameManager.instance.styleInd != 2)
                {
                    GameManager.instance.styleInd = 2;
                    GameManager.instance.Save();
                }
            }
            else
            {
                if (GameManager.instance.points >= 100)
                {
                    GameManager.instance.points -= 100;
                    GameManager.instance.styleInd = 2;
                    GameManager.instance.styleUnlocked[2] = true;
                    GameManager.instance.Save();
                }
            }
            pointText.text = "" + GameManager.instance.points;
        }


    }
}//namespace