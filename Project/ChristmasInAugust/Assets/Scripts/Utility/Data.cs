using UnityEngine;

namespace Utility
{
    public class Data
    {
        public static int Stage { get; set; } = 0;
        public static int GameSpeed { get; set; } = 1;
        public static bool IsEasterEgg = false;

        public static void InitData()
        {
            Score.Total = 0;
            Score.Snow = 0;
            Score.Avoid = 0;

            Item.SlowWatch = 0;
            Stage = 0;
        }

        public struct ModifyData
        {
            public bool MuteBGM { get; set; }
            public bool MuteEffect { get; set; }
        }

        public struct PrefabName
        {
            public const string AngryBullet = "AngryBullet(Clone)";
            public const string SilverSnow = "SilverSnow(Clone)";
            public const string YellowSnow = "YellowSnow(Clone)";
            public const string GrumpyBullet = "GrumpyBullet(Clone)";
            public const string Watch = "WatchItem(Clone)";
            public const string Cloud = "CloudItem(Clone)";
            public const string Heart = "HeartItem(Clone)";
        }

        public struct Item
        {
            public static int SlowWatch { get; set; } = 0;
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
            public static readonly string GAME_MANAGER = "GameManager";
        }

    }
}