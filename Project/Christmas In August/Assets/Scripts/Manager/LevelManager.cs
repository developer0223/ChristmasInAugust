using UnityEngine;

namespace Manager
{
    public class LevelManager : Manager
    {
        public Sprite hacking;
        public Sprite[] backgrounds;
        
        public SpriteRenderer backgroundSprite;

        public bool SetBackground()
        {
            int easterEgg = Random.Range(0, 10);
            if (easterEgg == 0)
            {
                backgroundSprite.sprite = hacking;
                return true;
            }
            else
            {
                int random = Random.Range(0, backgrounds.Length);
                backgroundSprite.sprite = backgrounds[random];
                return false;
            }
        }
    }
}