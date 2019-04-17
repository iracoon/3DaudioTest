namespace VRTK
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;


    public class MainScript : MonoBehaviour
    {
        private static float radius = 0.8f;
        private static float[] azimuths = { -80, -65, -55, -45, -40, -35, -30, -25, -20, -15, -10, -5, 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 55, 65, 80 };
        private static float[] elevations = new float[50];
        private static GameObject[] spheres = new GameObject[1250];
        float cAzimuth;
        float cElevation;
        float cosTheta;

        VRTK_StraightPointerRenderer myPointer;
        private Text text;
        GameObject player;
        Vector3 position;

        void Start()
        {
            text = GameObject.Find("RedMenuText").GetComponent<Text>();
            player = GameObject.Find("Head");
            myPointer = GameObject.Find("Headset").GetComponent<VRTK_StraightPointerRenderer>();

            Physics.gravity = Vector3.zero;
            float t = -45f;
            for (int e = 0; e < elevations.Length; e++)
            {
                elevations[e] = t;
                t += 5.625f;

            }

            int counter = 0;
            GameObject prefab = Resources.Load("Sphere") as GameObject;
            for (int i = 0; i < azimuths.Length; i++)
            {
                for (int j = 0; j < elevations.Length; j++)
                {
                    GameObject player = GameObject.Find("Head");
                    Transform playerTransform = player.transform;
                    Vector3 position = playerTransform.position;

                    GameObject go = Instantiate(prefab) as GameObject;
                    Rigidbody Rb;
                    Rb = go.GetComponent<Rigidbody>();
                    Rb.isKinematic = true;

                    float x = Mathf.Sin(azimuths[i] * Mathf.Deg2Rad) * radius;
                    float y = Mathf.Cos(azimuths[i] * Mathf.Deg2Rad) * Mathf.Sin(elevations[j] * Mathf.Deg2Rad) * radius;
                    float z = Mathf.Cos(azimuths[i] * Mathf.Deg2Rad) * Mathf.Cos(elevations[j] * Mathf.Deg2Rad) * radius;

                    go.GetComponent<SphereScript>().aAngle = azimuths[i];
                    go.GetComponent<SphereScript>().eAngle = elevations[j];
                    go.transform.position = new Vector3(position.x - x, position.y + y, position.z + z);

                    spheres[counter] = go;
                    counter++;
                }
            }
        }

        void Update()
        {
            position = player.transform.position;
            
            int counter = 0;
            for (int i = 0; i < azimuths.Length; i++)
            {
                for (int j = 0; j < elevations.Length; j++)
                {
                    float x = Mathf.Sin(azimuths[i] * Mathf.Deg2Rad) * radius;
                    float y = Mathf.Cos(azimuths[i] * Mathf.Deg2Rad) * Mathf.Sin(elevations[j] * Mathf.Deg2Rad) * radius;
                    float z = Mathf.Cos(azimuths[i] * Mathf.Deg2Rad) * Mathf.Cos(elevations[j] * Mathf.Deg2Rad) * radius;
                    
                    spheres[counter].transform.position = new Vector3(position.x - x, position.y + y, position.z + z);
                    counter++;
                }
            }

            cAzimuth = Mathf.Asin((myPointer.GetPointerObjects()[1].transform.position.x - player.transform.position.x) / 0.8f) * Mathf.Rad2Deg;
            cAzimuth = (float)Math.Round(cAzimuth, MidpointRounding.AwayFromZero);

            cElevation = Mathf.Asin((myPointer.GetPointerObjects()[1].transform.position.y - player.transform.position.y) / (0.8f * Mathf.Cos(cAzimuth * Mathf.Deg2Rad))) * Mathf.Rad2Deg;
            cElevation = (float)Math.Round(cElevation, 3, MidpointRounding.AwayFromZero);

            if (myPointer.GetPointerObjects()[1].transform.position.z <= 0)
            {
                cElevation = (90 - cElevation) + 90;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                text.text = "Data point collision:\n" + "Azimuth: " + -cAzimuth + "\nElevation: "
                     + cElevation + "\nRadius: 0.8";
            }
        }
    }
}