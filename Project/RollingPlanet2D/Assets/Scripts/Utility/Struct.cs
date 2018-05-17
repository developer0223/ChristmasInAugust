namespace Utility
{
    public class Struct
    {
        public struct ModifyData
        {
            public bool muteBGM { get; set; }
            public bool muteEffect { get; set; }
        }

        public struct Tags
        {
            public static readonly string PLANET = "Planet";
            public static readonly string PLAYER = "Player";
        }
    }
}