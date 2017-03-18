using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionRPG
{
    public enum ItemType
    {
        
        MANA_POTION,
        HEALTH_POTION,
        SPELL,
        STATUSEFFECTSPELL
    };

    public class Item : MonoBehaviour
    {

        public ItemType type;

        public Sprite spriteNeutral;
        public Sprite spriteHighlighted;

        public string ItemTitle;
        public string ItemDescription;
        public int ItemCost;

        public GameObject HotBarSpell;

        public int maxSize;

        public void Use()
        {
            switch (type)
            {
                case ItemType.HEALTH_POTION:
                    Debug.Log("I JUST USED A HEALTH POTION");
                    break;
                case ItemType.MANA_POTION:
                    Debug.Log("I JUST USED A MANA POTION");
                    break;
                case ItemType.SPELL:
                    Debug.Log("I JUST USED A SPELL");
                    break;
                default:
                    break;
            }
        }

        public void Buy()
        {
            switch (type)
            {
                case ItemType.HEALTH_POTION:
                    Debug.Log("I JUST BOUGHT A HEALTH POTION");
                    FindObjectOfType<PlayerItemManager>().HealthPotionCount++;
                    FindObjectOfType<PlayerItemManager>().CoinCount -= ItemCost;
                    FindObjectOfType<PlayerController>().inventory.AddItem(this);                    
                    break;
                case ItemType.MANA_POTION:
                    Debug.Log("I JUST BOUGHT A MANA POTION");
                    break;
                case ItemType.SPELL:
                    Debug.Log("I JUST BOUGHT A SPELL");
                    FindObjectOfType<PlayerItemManager>().CoinCount -= ItemCost;
                    FindObjectOfType<PlayerController>().spellBook.AddItem(this);
                    break;
                default:
                    break;
            }
                FindObjectOfType<SFXManager>().CoinPickup.Play();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (this.type == ItemType.HEALTH_POTION)
                {
                    bool result = FindObjectOfType<PlayerController>().inventory.AddItem(this);

                    if (result)
                    {
                        FindObjectOfType<SFXManager>().Potion.Play();
                        FindObjectOfType<PlayerItemManager>().HealthPotionCount++;
                    }

                }
                else if (this.type == ItemType.SPELL)
                {
                    bool result = FindObjectOfType<PlayerController>().spellBook.AddItem(this);

                }



                Destroy(gameObject);
            }
        }
    }
}
