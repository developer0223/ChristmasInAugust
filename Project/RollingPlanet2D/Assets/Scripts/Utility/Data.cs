using UnityEngine;

namespace Utility
{
    public class Data
    {
        public struct ModifyData
        {
            public bool MuteBGM { get; set; }
            public bool MuteEffect { get; set; }
        }

        public struct Score
        {
            public static int Star { get; set; } = 0;
            public static int Item { get; set; } = 0;
        }

        public struct Tags
        {
            public static string PLANET { get; } = "Planet";
            public static string PLAYER { get; } = "Player";
        }

    }
}