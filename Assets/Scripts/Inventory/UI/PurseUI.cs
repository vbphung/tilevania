using UnityEngine;
using UnityEngine.UI;

namespace Inventories.UI
{
    public class PurseUI : MonoBehaviour
    {
        [SerializeField] Text balanceField;

        Purse purse;

        private void Awake()
        {
            purse = FindObjectOfType<Purse>();
            if (purse != null)
            {
                purse.onPurseChanged += Redraw;
                Redraw();
            }
        }

        private void Redraw()
        {
            balanceField.text = purse.GetBalance().ToString() + "$";
        }
    }
}