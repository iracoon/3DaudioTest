namespace VRTK
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class SphereScript : MonoBehaviour
    {
        public float eAngle;
        public float aAngle;
        private Text text;
        VRTK_InteractableObject dataPoint;

        void Start()
        {
            dataPoint = gameObject.GetComponent<VRTK_InteractableObject>();
            text = GameObject.Find("RedMenuText").GetComponent<Text>();
        }

        void Update()
        {
            if (dataPoint.IsTouched())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    text.text = "Data point collision:\n" + "Azimuth: " + aAngle + "\nElevation: "
                     + eAngle + "\nRadius: 0.8";
                }
            }
        }
    }
}