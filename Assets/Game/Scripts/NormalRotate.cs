using UnityEngine;
using System.Collections;

namespace MadFireOn
{
    public class NormalRotate : MonoBehaviour
    {

        public float rotSpeed = 5;
        private float angle;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            MoveRotate();

        }

        void MoveRotate()
        {
            angle = transform.rotation.eulerAngles.z;
            angle += rotSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

    }
}//namespace