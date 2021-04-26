using UnityEngine;
using System.Collections;
/// <summary>
/// This script controls the player inputs 
/// </summary>

namespace MadFireOn
{
    public class InputManager : MonoBehaviour
    {
        private bool draggingItem = false;
        private GameObject draggedObject;
        private Vector2 touchOffset;
        [SerializeField]
        private LayerMask dragObj;

        void Update()
        {
            if (GameManager.instance.isGameOver == true)
                return;
            //if player touches the screen and it detects the object we pick it
            if (HasInput)
            {
                DragOrPickUp();
            }
            else
            {
                //if there is not input from player and the object is being drag we then drop it
                if (draggingItem)
                    DropItem();
            }
        }

        //get the position of touch 
        Vector2 CurrentTouchPosition
        {
            get
            {
                return Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        //method which controls the drag and pick function of tile
        private void DragOrPickUp()
        {
            //we store the input of player
            var inputPosition = CurrentTouchPosition;
            if (draggingItem)
            {
                inputPosition.y = 0;
                touchOffset.y = -2.34f;//this the y value of grids
                draggedObject.transform.position = inputPosition + touchOffset;
            }
            else
            {
                //we pass the ray cast to detect the object
                RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f, dragObj);
                //we check if touches are more the 0
                if (touches.Length > 0)
                {
                    var hit = touches[0];
                    //we chech if we are touching object which has tag tile
                    if (hit.transform != null && hit.transform.tag == "Tile")
                    {
                        //then we do following function
                        draggingItem = true;
                        draggedObject = hit.transform.gameObject;
                        touchOffset = (Vector2)hit.transform.position - inputPosition;
                        hit.transform.GetComponent<Tile>().PickUp();
                    }
                }
            }
        }

        private bool HasInput
        {
            get
            {
                // returns true if either the mouse button is down or at least one touch is felt on the screen
                return Input.GetMouseButton(0);
            }
        }

        void DropItem()
        {
            draggingItem = false;
            draggedObject.transform.localScale = new Vector3(1, 1, 1);
            draggedObject.GetComponent<Tile>().Drop();
        }
    }
}//namespace