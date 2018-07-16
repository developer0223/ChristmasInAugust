using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ResolutionManager : Manager
    {
        private void Awake()
        {
            Screen.SetResolution(720, 1280, FullScreenMode.FullScreenWindow);
        }
    }
}