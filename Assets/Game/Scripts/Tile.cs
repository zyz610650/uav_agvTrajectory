using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// This script manages the circle which playe moves , it take care of its placing in grid etc 
/// </summary>

namespace MadFireOn
{
    public class Tile : MonoBehaviour
    {
        //ref to the starting position of tile
        private Vector2 startingPosition;
        //ref to all the tiles in the game
        private List<Transform> touchingTiles;
        //ref to the tile parent
        private Transform myParent;
        //ref to the tile last parent obj
        private Transform oldObjOfCell;
        //all tile are different and allow different dots to pass so we choose which tile to allow which dot
        [SerializeField]
        private string dotToAllow;
        private AudioSource audSource;
        //[HideInInspector]
        public string currentCellName, requiredCell;
        //[HideInInspector]
        public bool onReqCell = false;

        private void Awake()
        {
            //store the position in starting position
            startingPosition = transform.position;
            //we make the list empty at start of game
            touchingTiles = new List<Transform>();
            //stores the parent in myparent
            myParent = transform.parent;
            audSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            currentCellName = transform.parent.gameObject.name.ToString();
        }

        void Update()
        {
            currentCellName = transform.parent.gameObject.name.ToString();

            if (currentCellName == requiredCell)
            {
                onReqCell = true;
            }
            else
            {
                onReqCell = false;
            }
        }

        //in it when the object is pick up we change its sorting order so it on the top of others
        //for the cirlce sprite the default sorting layer is 1 so we change it to 2
        //we also make it little large
        public void PickUp()
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 2;
            myParent = transform.parent;
        }

        //in it when the object is drop down we change its sorting order back to original
        public void Drop()
        {
            transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 1;

            Vector2 newPosition;
            //if the tile is not touching any cell and we drop then it tranform to its original position
            if (touchingTiles.Count == 0)
            {
                transform.position = startingPosition;
                transform.parent = myParent;
                return;
            }

            var currentCell = touchingTiles[0];
            //when the tile touches cell and we drop then we get the position of that cell
            if (touchingTiles.Count == 1)
            {
                newPosition = currentCell.position;
                currentCellName = currentCell.name;
            }
            else
            {//here when we move our tile and if it is touching more than 1 cell then we select the cell which is closer
                var distance = Vector2.Distance(transform.position, touchingTiles[0].position);

                foreach (Transform cell in touchingTiles)
                {
                    if (Vector2.Distance(transform.position, cell.position) < distance)
                    {
                        currentCell = cell;
                        distance = Vector2.Distance(transform.position, cell.position);
                    }
                }
                newPosition = currentCell.position;
                currentCellName = currentCell.name;
            }
            //here we check it the cell already contail another tile
            if (currentCell.childCount != 0)
            {
                //we get the ref to old object which was in the cell
                oldObjOfCell = currentCell.GetChild(0).transform;
                //we change its parent to the old parent of tile which is moved
                currentCell.GetChild(0).parent = myParent;
                //we then move the tile 
                oldObjOfCell.localPosition = Vector3.zero;
                //here we change the parent of tile which is been moved
                transform.parent = currentCell;
                //we perform some smooth movement here
                StartCoroutine(SlotIntoPlace(transform.position, newPosition));
                return;
            }
            else
            {
                transform.parent = currentCell;
                StartCoroutine(SlotIntoPlace(transform.position, newPosition));
            }

        }

        //we keep track which object are entering the tiles
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Cell"))
            {
                if (!touchingTiles.Contains(other.transform))
                {
                    touchingTiles.Add(other.transform);
                }
            }

            if (other.CompareTag(dotToAllow))
            {
                //here we deactivate the object
                other.gameObject.SetActive(false);
            }
            else if (!other.CompareTag(dotToAllow) && !other.CompareTag(tag) && !other.CompareTag("Cell"))
            {
                //here we do game over , spawn some effects
                other.gameObject.SetActive(false);
                GameManager.instance.isGameOver = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.tag != "Cell") return;
            if (touchingTiles.Contains(other.transform))
            {
                touchingTiles.Remove(other.transform);
            }
        }

        IEnumerator SlotIntoPlace(Vector2 startingPos, Vector2 endingPos)
        {
            float duration = 0.1f;
            float elapsedTime = 0;
            audSource.Play();
            while (elapsedTime < duration)
            {
                transform.position = Vector2.Lerp(startingPos, endingPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            transform.position = endingPos;
        }
    }
}//namespace