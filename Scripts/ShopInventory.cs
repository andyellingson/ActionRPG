using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActionRPG
{
    public class ShopInventory : MonoBehaviour
    {

        private RectTransform inventoryRect;

        private float inventoryWidth;
        private float inventoryHeight;

        public int slots;
        public int rows;

        public float slotPaddingLeft;
        public float slotPaddingTop;

        public float slotSize;

        public GameObject slotPrefab;

        private List<GameObject> allSlots;

        private int emptySlots;

        private Slot from;
        private Slot to;

        private SFXManager sfxManager;
        private PlayerController playerController;

        public bool IsShopInventory;


        private static GameObject selectedSlot;
        public static GameObject SelectedSlot
        {
            get { return selectedSlot; }
            set { selectedSlot = value; }
        }

        public int EmptySlots
        {
            get
            {
                return emptySlots;
            }

            set
            {
                emptySlots = value;
            }
        }

        public Slot GetHotBarSlotAtIndex(int index)
        {
            Slot currentSlot;
            int counter = 0;
            foreach (GameObject slot in allSlots)
            {
                currentSlot = slot.GetComponent<Slot>();

                if (!currentSlot.IsEmpty && counter == index)
                {
                    return currentSlot;
                }
                counter++;
            }
            print("INVENTORY: Nothing at that index DUMMY!!");
            sfxManager.MagicFail.Play();
            return null;
        }

        // Use this for initialization
        void Start()
        {
            sfxManager = FindObjectOfType<SFXManager>();
            CreateLayout();
            playerController = FindObjectOfType<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            print("inventory count " + EmptySlots);
        }

        //set up all inventory
        private void CreateLayout()
        {
            allSlots = new List<GameObject>();
            EmptySlots = slots;

            inventoryWidth = (slots / rows) * (slotSize + slotPaddingLeft) + slotPaddingLeft;
            inventoryHeight = rows * (slotSize + slotPaddingTop) + slotPaddingTop;

            inventoryRect = GetComponent<RectTransform>();

            //set inventory size
            inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, inventoryWidth);
            inventoryRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, inventoryHeight);

            int columns = slots / rows;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    //instantiate new inventory cell
                    GameObject newSlot = (GameObject)Instantiate(slotPrefab);

                    //get the cells rectransform
                    RectTransform slotRect = newSlot.GetComponent<RectTransform>();

                    newSlot.name = "Slot";

                    //set slots parent to the inventory's parent
                    newSlot.transform.SetParent(this.transform.parent);

                    //set position of current inventory cell
                    slotRect.localPosition = inventoryRect.localPosition + new Vector3(slotPaddingLeft * (x + 1) + (slotSize * x), -slotPaddingTop * (y + 1) - (slotSize * y));


                    slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize);

                    slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);

                    allSlots.Add(newSlot);

                }
            }

        }

        public bool AddItem(Item item)
        {
            print("AddItem");
            if (item.maxSize == 1)
            {
                PlaceEmpty(item);
                return true;
            }
            else
            {
                foreach (GameObject slot in allSlots)
                {
                    Slot currentSlot = slot.GetComponent<Slot>();

                    if (!currentSlot.IsEmpty)
                    {
                        if (currentSlot.CurrentItem.type == item.type && currentSlot.IsAvailable)
                        {
                            currentSlot.AddItem(item);
                            return true;
                        }
                    }
                }
                if (EmptySlots > 0)
                {
                    PlaceEmpty(item);
                }
            }


            return false;
        }

        private bool PlaceEmpty(Item item)
        {
            if (EmptySlots > 0)
            {
                foreach (GameObject slot in allSlots)
                {
                    Slot tmp = slot.GetComponent<Slot>();

                    if (tmp.IsEmpty)
                    {
                        tmp.AddItem(item);
                        EmptySlots--;
                        return true;
                    }

                }
            }
            return false;
        }

        public void MoveItem(GameObject clicked)
        {

            selectedSlot = clicked;
            //if we are in a shop UI we can use the last item selected as the current selected item. Also we don't want people to be able to move items.
            //var test = GameObject.Find("ShopItemPurchaseWindow");
            //if (test != null && test.tag == "ShopWindow")
            if (gameObject.tag == "ShopWindow")
            {
                return;
            }
            //first item were clicking
            if (from == null)
            {
                if (!clicked.GetComponent<Slot>().IsEmpty)
                {
                    from = clicked.GetComponent<Slot>();
                    from.GetComponent<Image>().color = Color.gray;
                }

            }
            else if (to == null)
            {
                to = clicked.GetComponent<Slot>();
            }
            if (to != null && from != null)
            {
                Stack<Item> tmpTo = new Stack<Item>(to.Items);
                to.AddItems(from.Items);

                if (tmpTo.Count == 0)
                {
                    from.ClearSlot();
                }
                else
                {
                    from.AddItems(tmpTo);
                }

                from.GetComponent<Image>().color = Color.white;
                to = null;
                from = null;

            }
        }

        public void OpenOrCloseInventory(bool isOpen)
        {
            if (isOpen)
            {
                foreach (GameObject cell in allSlots)
                {
                    Slot current = cell.GetComponent<Slot>();
                    cell.SetActive(false);
                }
            }
            else
            {
                foreach (GameObject cell in allSlots)
                {
                    Slot current = cell.GetComponent<Slot>();
                    cell.SetActive(true);
                }
            }

        }

        public GameObject GetSelectedSlot()
        {
            return SelectedSlot;
        }

        public void BuyItem()
        {
            //first item were clicking

            if (SelectedSlot != null)
            {
                var playerItemManager = FindObjectOfType<PlayerItemManager>();
                if (playerItemManager.CoinCount >= SelectedSlot.GetComponent<Slot>().Items.Peek().ItemCost)
                {
                    playerItemManager.CoinCount -= SelectedSlot.GetComponent<Slot>().Items.Peek().ItemCost;

                    SelectedSlot.GetComponent<Slot>().Items.Peek().Buy();
                }
            }
            else
            {
                print("Nothing selected");
            }

        }

        public void UseHealthPotion()
        {
            foreach (GameObject slot in allSlots)
            {
                Slot tmp = slot.GetComponent<Slot>();

                //if there is nothing in the slot then skip it
                if (tmp.Items.Count > 0)
                {
                    if (tmp.CurrentItem.type == ItemType.HEALTH_POTION)
                    {
                       // tmp.UseItem();
                    }
                }
            }
        }
    }
}
