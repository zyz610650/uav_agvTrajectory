using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// This script is used to monitor object pooling, in it we create a specific number of object and 
/// make them active and deactive as per requirement
/// For more detail see ReadMe
/// </summary>

namespace MadFireOn
{
    public class ObjectPooling : MonoBehaviour
    {

        public static ObjectPooling instance;

        // ref to the game object to be pooled
        public GameObject blueDot;
        public GameObject greenDot;
        public GameObject redDot;
        public GameObject yellowDot;

        public int poolAmount;//amount of clone to create

        //get then into the list
        List<GameObject> blue;
        List<GameObject> green;
        List<GameObject> red;
        List<GameObject> yellow;

        void Awake()
        {
            MakeInstance();
        }

        void MakeInstance()
        {
            if (instance == null)
                instance = this;
        }


        // Use this for initialization
        void Start()
        {
            //at start we create ne list to avoid any problems
            blue = new List<GameObject>();
            green = new List<GameObject>();
            red = new List<GameObject>();
            yellow = new List<GameObject>();

            //we create the clone of object as per poolAmount
            for (int i = 0; i < poolAmount; i++)
            {
                GameObject obj = Instantiate(blueDot);
                obj.transform.parent = transform;
                obj.SetActive(false);
                blue.Add(obj);
            }

            for (int i = 0; i < poolAmount; i++)
            {
                GameObject obj = Instantiate(greenDot);
                obj.transform.parent = transform;
                obj.SetActive(false);
                green.Add(obj);
            }

            for (int i = 0; i < poolAmount; i++)
            {
                GameObject obj = Instantiate(redDot);
                obj.transform.parent = transform;
                obj.SetActive(false);
                red.Add(obj);
            }

            for (int i = 0; i < poolAmount; i++)
            {
                GameObject obj = Instantiate(yellowDot);
                obj.transform.parent = transform;
                obj.SetActive(false);
                yellow.Add(obj);
            }
        }

        //this is a method which will be used for calling gameobject from the list
        public GameObject GetBlueDot()
        {
            for (int i = 0; i < blue.Count; i++)
            {
                if (!blue[i].activeInHierarchy)
                {
                    return blue[i];
                }
            }

            GameObject obj = Instantiate(blueDot);
            obj.transform.parent = transform;
            obj.SetActive(false);
            blue.Add(obj);
            return obj;

        }

        public GameObject GetGreenDot()
        {
            for (int i = 0; i < green.Count; i++)
            {
                if (!green[i].activeInHierarchy)
                {
                    return green[i];
                }
            }

            GameObject obj = Instantiate(greenDot);
            obj.transform.parent = transform;
            obj.SetActive(false);
            green.Add(obj);
            return obj;

        }

        public GameObject GetRedDot()
        {
            for (int i = 0; i < red.Count; i++)
            {
                if (!red[i].activeInHierarchy)
                {
                    return red[i];
                }
            }

            GameObject obj = Instantiate(redDot);
            obj.transform.parent = transform;
            obj.SetActive(false);
            red.Add(obj);
            return obj;

        }

        public GameObject GetYellowDot()
        {
            for (int i = 0; i < yellow.Count; i++)
            {
                if (!yellow[i].activeInHierarchy)
                {
                    return yellow[i];
                }
            }

            GameObject obj = Instantiate(yellowDot);
            obj.transform.parent = transform;
            obj.SetActive(false);
            yellow.Add(obj);
            return obj;

        }
    }
}//namespace