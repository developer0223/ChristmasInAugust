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
            public static int Total { get; set; } = 0;
            public static int Snow { get; set; } = 0;
            public static int Avoid { get; set; } = 0;
        }

        public struct BestScore
        {
            public static readonly string PrefsName = "CIU_Score";
        }

        public struct Tags
        {
            public static readonly string PLANET = "Planet";
            public static readonly string PLAYER = "Player";
            public static readonly string CLOUD = "Cloud";
        }

    }
}