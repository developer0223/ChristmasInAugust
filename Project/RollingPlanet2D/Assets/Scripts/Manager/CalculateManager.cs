using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class CalculateManager : Manager
    {

        public float GetX(float degree, float radius)
        {
            return Mathf.Cos(degree * Mathf.Deg2Rad) * radius;
        }

        public float GetY(float degree, float radius)
        {
            return Mathf.Sin(degree * Mathf.Deg2Rad) * radius;
        }

        public Vector3 GetPosition(float degree, float radius)
        {
            float x = Mathf.Cos(degree * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(degree * Mathf.Deg2Rad) * radius;

            return new Vector3(x, y, 0);
        }
    }
}