using System;
using UnityEngine;

namespace Inventories
{
    public class Purse : MonoBehaviour
    {
        [SerializeField] float startingBalance = 200f;

        float balance = 0;

        public event Action onPurseChanged;

        private void Awake()
        {
            UpdateBalance(startingBalance);
        }

        public float GetBalance()
        {
            return balance;
        }

        public void UpdateBalance(float amount)
        {
            balance += amount;

            if (onPurseChanged != null)
                onPurseChanged();
        }
    }
}