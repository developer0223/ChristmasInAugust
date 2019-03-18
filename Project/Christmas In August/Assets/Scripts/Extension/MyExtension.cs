using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Extension
{
    public static class MyExtension
    {
        public static bool SetTrue(this bool value)
        {
            return value = true;
        }

        public static bool SetFalse(this bool value)
        {
            return value = false;
        }
    }
}