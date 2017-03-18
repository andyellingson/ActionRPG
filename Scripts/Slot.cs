using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ActionRPG
{

    public class Slot : MonoBehaviour, IPointerClickHandler
    {


        public Text stackTxt;
        public Sprite slotEmpty;
        public Sprite slotHighlight;
        public bool IsShopSlot;

        public GameObject HotBarSpell
        {
            get
            {
                if (Items.Count == 1)
                {
                    return Items.Peek().HotBarSpell;
                }
                return new GameObject();
            }
        }

        public bool IsEmpty
        {
            get { return Items.Count == 0; }
        }

        public Item CurrentItem
        {
            get { return Items.Peek(); }
        }

        public bool IsAvailable
        {
            get { return CurrentItem.maxSize > Items.Count; }
        }


        private Stack<Item> items;
        public Stack<Item> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            if (IsShopSlot) gameObject.tag = "Cell_Shop";
            Items = new Stack<Item>();
            RectTransform slotRect = GetComponent<RectTransform>();
            RectTransform txtRect = stackTxt.GetComponent<RectTransform>();

            //scale factor for text
            int txtScaleFactor = (int)(slotRect.sizeDelta.x * 0.60);
            stackTxt.resizeTextMaxSize = txtScaleFactor;
            stackTxt.resizeTextMinSize = txtScaleFactor;

            stackTxt.text = Items.Count > 1 ? Items.Count.ToString() : string.Empty;

            txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotRect.sizeDelta.x);
            txtRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotRect.sizeDelta.y);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddItem(Item item)
        {
            Items.Push(item);

            if (Items.Count > 1)
            {
                stackTxt.text = Items.Count.ToString();
            }

            ChangeSprite(item.spriteNeutral, item.spriteHighlighted);

        }

        public void AddItems(Stack<Item> items)
        {
            this.Items = new Stack<Item>(items);

            stackTxt.text = items.Count > 1 ? items.Count.ToString() : string.Empty;

            ChangeSprite(CurrentItem.spriteNeutral, CurrentItem.spriteHighlighted);

        }


        private void ChangeSprite(Sprite neutral, Sprite highLight)
        {
            GetComponent<Image>().sprite = neutral;

            SpriteState st = new SpriteState();
            st.highlightedSprite = highLight;
            st.pressedSprite = neutral;

            GetComponent<Button>().spriteState = st;
        }

        public void UseItem(Inventory inventory)
        {
            if (!IsEmpty)
            {
                Items.Pop().Use();

                stackTxt.text = Items.Count > 1 ? Items.Count.ToString() : string.Empty;

                if (IsEmpty)
                {
                    ChangeSprite(slotEmpty, slotHighlight);

                    inventory.EmptySlots++;
                }

            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                UseItem(GetComponentInParent<Inventory>());
            }
        }

        public void ClearSlot()
        {
            items.Clear();
            ChangeSprite(slotEmpty, slotHighlight);
            stackTxt.text = string.Empty;
        }

    }
}