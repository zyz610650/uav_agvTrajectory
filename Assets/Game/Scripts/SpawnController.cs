using UnityEngine;
using System.Collections;
/// <summary>
/// This script controls the spawn of dots there number and also the score
/// </summary>

namespace MadFireOn
{
    public class SpawnController : MonoBehaviour
    {

        public static SpawnController instance;

        [SerializeField]
        private Transform[] spawnPosition;

        //for speed of dots
        public float speed;
        public float maxSpeed;
        //the milestone which tells when to increase speed
        public float speedIncreaseMilestone;
        //this is used to set new milestone when we reach one
        [HideInInspector]
        public float speedMileStoneCount;
        //amount by whihc speed is increase when we hit milestone
        public float speedMultiplier;
        //we use this to assign positions to the dots
        [HideInInspector]
        public int a, b, c, d;

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            SpawnDots();
            //initially we need some milestone 
            speedMileStoneCount = speedIncreaseMilestone;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.isGameOver == false)
            {
                CheckMileStone();
            }
        }

        public void SpawnDots()
        {
            RandomNumber();
            SpawnRed();
            SpawnYellow();
            SpawnBlue();
            SpawnGreen();
        }
        //here we select the number
        void RandomNumber()
        {
            a = Random.Range(0, 4);

            b = Random.Range(0, 4);
            while (b == a)
            {
                b = Random.Range(0, 4);
            }

            c = Random.Range(0, 4);
            while (c == a || c == b)
            {
                c = Random.Range(0, 4);
            }

            d = Random.Range(0, 4);
            while (d == a || d == b || d == c)
            {
                d = Random.Range(0, 4);
            }
        }

        void SpawnRed()
        {
            int i = Random.Range(0, 4);
            if (i == 2)
                return;
            //spawn red
            GameObject newRed = ObjectPooling.instance.GetRedDot();
            newRed.transform.position = spawnPosition[a].position; // we spawn dot at the spawner position
            newRed.transform.rotation = this.transform.rotation;
            DotController script = newRed.GetComponent<DotController>();
            ScoreHandler.instance.dots.Add(script);
            script.boost = false;
            newRed.SetActive(true);//we make that dot active 
                                   //here we find the required cell
            Tile cell = GameObject.Find("RedCell").GetComponent<Tile>();
            //here we set his required cell position to score
            cell.requiredCell = "Cell" + (a + 1).ToString();
            ScoreHandler.instance.GetRedTile();
        }
        void SpawnYellow()
        {
            int i = Random.Range(0, 4);
            if (i == 2)
                return;
            //spawn yellow
            GameObject newYellow = ObjectPooling.instance.GetYellowDot();
            newYellow.transform.position = spawnPosition[b].position; // we spawn dot at the spawner position
            newYellow.transform.rotation = this.transform.rotation;
            DotController script = newYellow.GetComponent<DotController>();
            ScoreHandler.instance.dots.Add(script);
            script.boost = false;
            newYellow.SetActive(true);//we make that dot active 
                                      //here we find the required cell
            Tile cell = GameObject.Find("YellowCell").GetComponent<Tile>();
            //here we set his required cell position to score
            cell.requiredCell = "Cell" + (b + 1).ToString();
            ScoreHandler.instance.GetYellowTile();
        }
        void SpawnBlue()
        {
            int i = Random.Range(0, 4);
            if (i == 2)
                return;
            //spawn blue
            GameObject newBlue = ObjectPooling.instance.GetBlueDot();
            newBlue.transform.position = spawnPosition[c].position; // we spawn dot at the spawner position
            newBlue.transform.rotation = this.transform.rotation;
            DotController script = newBlue.GetComponent<DotController>();
            ScoreHandler.instance.dots.Add(script);
            script.boost = false;
            newBlue.SetActive(true);//we make that dot active 
                                    //here we find the required cell
            Tile cell = GameObject.Find("BlueCell").GetComponent<Tile>();
            //here we set his required cell position to score
            cell.requiredCell = "Cell" + (c + 1).ToString();
            ScoreHandler.instance.GetBlueTile();
        }
        void SpawnGreen()
        {
            int i = Random.Range(0, 4);
            if (i == 2)
                return;
            //spawn green
            GameObject newGreen = ObjectPooling.instance.GetGreenDot();
            newGreen.transform.position = spawnPosition[d].position; // we spawn dot at the spawner position
            newGreen.transform.rotation = this.transform.rotation;
            DotController script = newGreen.GetComponent<DotController>();
            ScoreHandler.instance.dots.Add(script);
            script.boost = false;
            newGreen.SetActive(true);//we make that dot active  
                                     //here we find the required cell
            Tile cell = GameObject.Find("GreenCell").GetComponent<Tile>();
            //here we set his required cell position to score
            cell.requiredCell = "Cell" + (d + 1).ToString();
            ScoreHandler.instance.GetGreenTile();
        }

        void CheckMileStone()
        {
            //if the score gets grater than milestone the speed is increased
            if (GameManager.instance.currentScore > speedMileStoneCount)
            {
                //when the speeed is increased we increase the milestones and set new ones
                speedMileStoneCount += speedIncreaseMilestone;
                speedIncreaseMilestone += speedIncreaseMilestone;
                //speed is increase my multiplying with the specific number whihc you can change from inspector
                speed *= speedMultiplier;

                //if our speed goes above max speed limit then we set the speed to max speed
                if (speed >= maxSpeed)
                {
                    speed = maxSpeed;
                }
            }
        }
    }
}//namespace