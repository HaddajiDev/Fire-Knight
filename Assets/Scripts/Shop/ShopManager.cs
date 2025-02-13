using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public GameObject openButton;
    public RectTransform ShopItemsContainer;
    public GameObject[] itemCards;
    public List<item> items;
    private List<item> RandomItems;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        RandomItems = GetRandomItems(4);
        assignItems();
    }


    private List<item> GetRandomItems(int amount)
    {
        return items.OrderBy(x => Guid.NewGuid()).Take(amount).ToList();
    }

    private void assignItems()
    {
        for (int i = 0; i < itemCards.Length; i++)
        {
            TMP_Text item_Name = itemCards[i].transform.GetChild(1).GetComponent<TMP_Text>();
            Image sprite = itemCards[i].transform.GetChild(2).GetComponent<Image>();
            TMP_Text description = itemCards[i].transform.GetChild(3).GetComponent<TMP_Text>();
            TMP_Text price = itemCards[i].transform.GetChild(4).GetComponent<Button>().GetComponentInChildren<TMP_Text>();
            item_Name.text = RandomItems[i].itemName;
            sprite.sprite = RandomItems[i].icon;
            description.text = RandomItems[i].itemDescription;
            price.text = RandomItems[i].price.ToString();
        }
    }

}