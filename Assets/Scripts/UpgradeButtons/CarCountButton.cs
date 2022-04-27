using UnityEngine;
using UnityEngine.UI;

namespace UpgradeButtons
{
    public class CarCountButton : UpgradeButton
    {
        public override void Execute()
        {
            var moneyManager = GameObject.FindGameObjectWithTag("MoneyManager")?.GetComponent<MoneyManager>();
            if (moneyManager && CanExecute())
            {
                moneyManager.maxSellingCars += skills[usedPoints].buff;
                moneyManager.cFreeSellingCars += skills[usedPoints].buff;
                skills[usedPoints].GetComponent<Image>().color = Color.white;
                usedPoints += 1;
                points.u_freePoint -= usedPoints;
            }
        }
    }
}
