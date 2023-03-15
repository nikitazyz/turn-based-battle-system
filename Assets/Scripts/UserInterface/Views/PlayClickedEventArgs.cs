using System;
using Enemies;

namespace UserInterface.Views
{
    public class PlayClickedEventArgs : EventArgs
    {
        public int? OverrideHealth;

        public int[] Enemies;
        public int?[] EnemiesOverrideHealth;
    }
}