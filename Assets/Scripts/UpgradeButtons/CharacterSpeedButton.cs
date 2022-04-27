using UnityEngine;
using UnityEngine.UI;

namespace UpgradeButtons
{
    public class CharacterSpeedButton : UpgradeButton
    {
        public override void Execute()
        {
            var characterInput = GameObject.FindGameObjectWithTag("Player")?.GetComponent<CharacterInput>();
            if (characterInput && CanExecute())
            {
                characterInput.ch_speed *= (float)(skills[usedPoints].buff + 100)/100;;
                skills[usedPoints].GetComponent<Image>().color = Color.white;
                usedPoints += 1;
                points.u_freePoint -= usedPoints;

            }
        }
    }
}