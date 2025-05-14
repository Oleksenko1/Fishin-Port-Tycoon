using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace WaterStylizedShader
{
    [RequireComponent(typeof(Rigidbody))]
    public class FloatingObject : MonoBehaviour
    {
        public Transform[] floaters;
        public float underWaterDrag = 3f;
        public float underWaterAngularDrag = 1f;
        public float airWaterDrag = 0f;
        public float airWaterAngularDrag = 0.05f;

        public float floatingPower = 15f;

        public float baseWaterHeight = 0f;
        public float waterHeightVariation = 2f;
        public float waveSpeed = 1.0f;
        public float waterHeight;

        Rigidbody rb;
        int floatersUnderwater;
        bool underwater;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            waterHeight = baseWaterHeight + Mathf.Sin(Time.time * waveSpeed) * (waterHeightVariation / 2f);

            floatersUnderwater = 0;
            for (int i = 0; i < floaters.Length; i++)
            {
                float diff = floaters[i].position.y - waterHeight;

                if (diff < 0)
                {
                    rb.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(diff), floaters[i].position, ForceMode.Force);
                    floatersUnderwater++;
                    if (!underwater)
                    {
                        underwater = true;
                        SwitchState(true);
                    }
                }
            }


            if (underwater && floatersUnderwater == 0)
            {
                underwater = false;
                SwitchState(false);
            }
        }

        void SwitchState(bool isUnderwater)
        {
            if (isUnderwater)
            {
                rb.drag = underWaterDrag;
                rb.angularDrag = underWaterAngularDrag;
            }
            else
            {
                rb.drag = airWaterDrag;
                rb.angularDrag = airWaterAngularDrag;
            }
        }
    }
}

