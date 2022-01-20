using UnityEngine;
using UnityEngine.UI;

namespace Shops.UI
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] Text shopName;
        [SerializeField] Transform itemListRoot;
        [SerializeField] RowUI rowPrefab;
        [SerializeField] Text totalField;
        [SerializeField] Button confirmButton;
        [SerializeField] Button buySellButton;

        Shopper shopper;
        Shop shop;
        FilterButton[] filterButtons;

        private void Start()
        {
            filterButtons = GetComponentsInChildren<FilterButton>();

            shopper = GameObject.FindGameObjectWithTag("Player").GetComponent<Shopper>();
            if (shopper != null)
                shopper.onActiveShopChanged += Redraw;

            Redraw();
        }

        private void Redraw()
        {
            if (shop != null)
                shop.onShopUpdated -= RefreshUI;

            shop = shopper.GetActiveShop();
            if (shop != null)
            {
                shop.onShopUpdated += RefreshUI;

                foreach (FilterButton button in filterButtons)
                    button.SetShop(shop);

                gameObject.SetActive(true);
                shopName.text = shop.GetName();

                RefreshUI();
            }
            else
                gameObject.SetActive(false);
        }

        private void RefreshUI()
        {
            foreach (Transform child in itemListRoot)
                Destroy(child.gameObject);

            foreach (ShopItem shopItem in shop.GetFilterItems())
            {
                RowUI rowUI = Instantiate(rowPrefab, itemListRoot);
                rowUI.Setup(shop, shopItem);
            }

            totalField.text = shop.TransactTotal().ToString() + "$";
            confirmButton.interactable = shop.CanTransact();

            if (shop.IsBuyingMode())
            {
                buySellButton.GetComponentInChildren<Text>().text = "switch to selling";
                confirmButton.GetComponentInChildren<Text>().text = "buy";
            }
            else
            {
                buySellButton.GetComponentInChildren<Text>().text = "switch to buying";
                confirmButton.GetComponentInChildren<Text>().text = "sell";
            }

            foreach (FilterButton button in filterButtons)
                button.Redraw();
        }

        public void SwitchMode()
        {
            shop.SwitchMode();
        }

        public void Confirm()
        {
            shop.ConfirmTransaction();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}