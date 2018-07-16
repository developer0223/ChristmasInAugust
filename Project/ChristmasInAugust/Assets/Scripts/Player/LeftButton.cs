using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Utility;

namespace Manager
{
    public class LeftButton : Manager
    {
        private Player.Player player;
        private bool pressed = false;

        private void Start()
        {
            player = FindComponentWithTag<Player.Player>(Data.Tags.PLAYER);
        }

        private void Update()
        {
            player.LeftPressed = pressed;
        }

        public void SetTrue()
        {
            pressed = true;
        }

        public void SetFalse()
        {
            pressed = false;
        }
    }
}