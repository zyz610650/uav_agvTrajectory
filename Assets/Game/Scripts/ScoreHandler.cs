using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Score system is changed , it now more reliable and effecient
/// </summary>

namespace MadFireOn
{
    public class ScoreHandler : MonoBehaviour
    {

        public static ScoreHandler instance;

        public List<Tile> cellTiles;  //ref to all tiles which need to be shuffled
        public List<DotController> dots; //ref to the active dots in the scene
        [HideInInspector]
        public bool scoreInc = false;  //decides when to increase score
        private int score;  //score variable

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            score = 0;
            GameManager.instance.currentScore = score;
            scoreInc = false;
        }

        // Update is called once per frame
        void Update()
        {
            //we keep checking for score inc 
            if (GameManager.instance.isGameOver == false && scoreInc == false)
                CheckForScoreInc();

            //here we reset few values when score is increased
            if (GameManager.instance.isGameOver == false && scoreInc == true)
            {
                for (int i = 0; i < dots.Count; i++)
                {
                    dots[i].boost = true;
                }

                EmptyList();
                score++;
                GameManager.instance.currentScore = score;
                SpawnController.instance.SpawnDots();
                scoreInc = false;

            }
        }
        //this methods are called by spawncontroller to add the corresponding tile to the list
        public void GetGreenTile()
        {
            cellTiles.Add(GameObject.Find("GreenCell").GetComponent<Tile>());
        }

        public void GetRedTile()
        {
            cellTiles.Add(GameObject.Find("RedCell").GetComponent<Tile>());
        }

        public void GetBlueTile()
        {
            cellTiles.Add(GameObject.Find("BlueCell").GetComponent<Tile>());
        }

        public void GetYellowTile()
        {
            cellTiles.Add(GameObject.Find("YellowCell").GetComponent<Tile>());
        }
        //the methode empty the list when score is increased
        public void EmptyList()
        {
            cellTiles.Clear();
            dots.Clear();
        }
        //this methode checks if we can increase the score
        void CheckForScoreInc()
        {
            //checks is any tile is need to shuffle
            if (cellTiles.Count <= 0)
                return;
            //checks if the required tile grid position is present
            for (int i = 0; i < cellTiles.Count; i++)
            {
                if (cellTiles[i].onReqCell == false)
                {
                    return;
                }
            }
            //if we satisfy all the above conditions we increase the score
            scoreInc = true;
        }

    }
}//namespace