using UnityEngine;
using UnityEngine.UI;

namespace UpgradeButtons
{
    public class WateringSpeedButton : UpgradeButton
    {
        public override void Execute()
        {
            var characterF = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterFunctionality>();
            if (characterF && CanExecute())
            {
                characterF.wateringTime /= (float)(skills[usedPoints].buff + 100)/100;
                
                skills[usedPoints].GetComponent<Image>().color = Color.white;
                usedPoints += 1;
                points.u_freePoint -= usedPoints;
            }
        }
    }
}
