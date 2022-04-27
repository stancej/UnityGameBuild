using System;
using JetBrains.Annotations;
using UnityEngine;

namespace UpgradeButtons
{
    public abstract class UpgradeButton : MonoBehaviour
    {
        [SerializeField]protected Skill[] skills;
        [HideInInspector] [SerializeField] protected int usedPoints;

        protected PointsManager points;

        private void Start()
        {
            points = GameObject.FindGameObjectWithTag("Skills")?.GetComponent<PointsManager>();
        }

        public abstract void Execute();

        public bool CanExecute()
        {
            if (points && points.u_freePoint > usedPoints && usedPoints < skills.Length)
                return true;
            else
                return false;

        }
    }
}
