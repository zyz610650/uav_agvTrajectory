using UnityEngine;
using System.Collections;
/// <summary>
/// This script controlls the movement of dot
/// </summary>

namespace MadFireOn
{
    public class DotController : MonoBehaviour
    {

        public static DotController instance;

        private SpriteRenderer img;

        private Rigidbody2D myBody;//we get the rigidbody of gameobjetc to which this script is attached
        [HideInInspector]
        public float forceY;
        [SerializeField]
        private Sprite[] blockImgs; // 0 rock , 1 crystal , 2 meteor
        [HideInInspector]
        public bool boost = false;

        void Awake()
        {
            if (instance == null)
                instance = this;
        }

        // Use this for initialization
        void Start()
        {
            //we want to go down so we need -ve speed value
            forceY = (SpawnController.instance.speed);
            img = GetComponent<SpriteRenderer>();

            img.sprite = blockImgs[GameManager.instance.styleInd];
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.isGameOver)
                return;

            if (boost)
            {
                forceY = 8f;
            }
            else
            {
                forceY = SpawnController.instance.speed;
            }

            //myBody.velocity = new Vector2(0, forceY);
            transform.Translate(new Vector3(0, -1, 0) * forceY * Time.deltaTime);
            //for safety if some how the dot manages to go out of screen the we set them inactive
            if (transform.position.y < -6)
            {
                gameObject.SetActive(false);
            }
        }

    }

}//namespace