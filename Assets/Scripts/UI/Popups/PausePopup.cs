using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Popups
{
    public class PausePopup : BasePopups
    {
        public override void MainButtonPressed()
        {
            LevelEvents.Pause(false);
            Hide();
        }
    }
}