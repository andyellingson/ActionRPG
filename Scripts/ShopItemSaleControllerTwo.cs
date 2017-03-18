using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActionRPG
{
    public class ShopItemSaleControllerTwo : MonoBehaviour
    {

        public GameObject ShopUI;
        public Inventory inventory;
        public List<Item> itemsForSale;
        public Text ItemTitleTextBox;
        public Text ItemDescriptionTextBox;
        public Text ItemCostTextBox;

        public Button BuyButton;
        private bool itemsLoaded;


        private PlayerItemManager playerItemManager;

        // Use this for initialization
        void Start()
        {

            playerItemManager = FindObjectOfType<PlayerItemManager>();
            BuyButton.onClick.AddListener(BuyItem);
            itemsLoaded = false;

            //ItemDescriptionTextBox.text = inventory. .SelectedSlot.Items.Peek(). itemsForSale.ItemTitle + "\n" + itemsForSale[i].ItemDescription + "\n Gold: " + itemsForSale[i].ItemCost;

        }

        // Update is called once per frame
        void Update()
        {
            //this ensures items are loaded before inventory is created
            if (!itemsLoaded)
            {
                
                for (int i = 0; i < itemsForSale.Count; i++)
                {
                    inventory.AddItem(itemsForSale[i]);
                }
                itemsLoaded = true;
            }

            if (inventory != null)
            {
                if (inventory.GetShopLastSelected() != null)
                {
                    if (!inventory.GetShopLastSelected().IsEmpty)
                    {

                        ItemTitleTextBox.text = inventory.GetShopLastSelected().Items.Peek().ItemTitle;
                        ItemDescriptionTextBox.text = inventory.GetShopLastSelected().Items.Peek().ItemDescription;
                        ItemCostTextBox.text = "Cost:\t" + inventory.GetShopLastSelected().Items.Peek().ItemCost.ToString();
                    }

                }
            }

        }

        public void BuyItem()
        {
            inventory.BuyItem();
        }

    }
}
