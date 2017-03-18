using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActionRPG
{
    public class ShopItemSaleController : MonoBehaviour
    {

        public GameObject ShopUI;
        public Inventory inventory;
        public List<Item> itemsForSale;
        public Text[] ItemTextBox;

        public Button BuyButton;
        private bool shopUIExists;
        private bool itemsLoaded;


        private PlayerItemManager playerItemManager;

        // Use this for initialization
        void Start()
        {
            shopUIExists = false;

            playerItemManager = FindObjectOfType<PlayerItemManager>();
            BuyButton.onClick.AddListener(BuyItem);
            itemsLoaded = false;

            if (!shopUIExists)
            {
                shopUIExists = true;
                DontDestroyOnLoad(transform.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            for (int i = 0; i < itemsForSale.Count; i++)
            {
                ItemTextBox[i].text = itemsForSale[i].ItemTitle + "\n" + itemsForSale[i].ItemDescription + "\n Gold: " + itemsForSale[i].ItemCost;
            }

        }

        // Update is called once per frame
        void Update()
        {
            //this ensures items are loaded before inventory is created
            if (!itemsLoaded)
            {
                if (inventory != null)
                {
                    for (int i = 0; i < itemsForSale.Count; i++)
                    {
                        inventory.AddItem(itemsForSale[i]);
                    }
                    itemsLoaded = true;
                }
            }
        }

        public void BuyItem()
        {
            inventory.BuyItem();
        }

    }
}
