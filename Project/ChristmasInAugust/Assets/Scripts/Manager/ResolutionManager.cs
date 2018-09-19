using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ResolutionManager : Manager
    {
        private void Awake()
        {
#if UNITY_ANDROID
            Screen.SetResolution(720, 1280, true); // FullScreenMode.FullScreenWindow
#elif UNITY_STANDALONE
            //Screen.SetResolution(600, 800, false);
            Screen.SetResolution(540, 960, false);  
#endif
        }
    }
}