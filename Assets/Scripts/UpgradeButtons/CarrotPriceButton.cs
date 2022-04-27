using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace UpgradeButtons
{
    public class CarrotPriceButton : UpgradeButton
    {
        public override void Execute()
        {
            var moneyManager = GameObject.FindGameObjectWithTag("MoneyManager")?.GetComponent<MoneyManager>();
            if (moneyManager && CanExecute())
            {
                //buff in procent => need to convert in multiplier
                moneyManager.carrotPrice = Mathf.CeilToInt(moneyManager.carrotPrice * (float)(skills[usedPoints].buff + 100)/100);
                
                skills[usedPoints].GetComponent<Image>().color = Color.white;
                usedPoints += 1;
                points.u_freePoint -= usedPoints;

            }
        }
    }
}
