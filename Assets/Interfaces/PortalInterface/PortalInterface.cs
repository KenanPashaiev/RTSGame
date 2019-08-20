using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalInterface : MonoBehaviour
{
    public Interface Interface;

    public Base PlayerBase;

    public Text ProductBuyExchangeRate;
    public Text ProductSaleExchangeRate;

    public Text CreditsBuyCount;
    public Slider ProductsBuySlider;
    public Text ProductsBuySliderCount;

    public Text CreditsSellCount;
    public Slider ProductsSellSlider;
    public Text ProductsSellSliderCount;

    public void OnProductsBuySliderEdit()
    {
        int productCount = (int)(ProductsBuySlider.value * PlayerBase.CreditsCount / PlayerBase.ProductBuyPrice);

        ProductsBuySliderCount.text = productCount.ToString();
        CreditsBuyCount.text = (productCount * PlayerBase.ProductBuyPrice).ToString();
    }

    public void OnProductsSellSliderEdit()
    {
        int productCount = (int)(ProductsSellSlider.value * PlayerBase.ProductsCount);

        ProductsSellSliderCount.text = productCount.ToString();
        CreditsSellCount.text = (productCount * PlayerBase.ProductSalePrice).ToString();
    }

    public void BuyProducts()
    {
        int productCount = (int)(ProductsBuySlider.value * PlayerBase.CreditsCount / PlayerBase.ProductBuyPrice);

        PlayerBase.BuyProducts(productCount);
    }

    public void SellProducts()
    {
        int productCount = (int)(ProductsSellSlider.value * PlayerBase.ProductsCount);

        PlayerBase.SellProducts(productCount);
    }

    private void Start()
    {
        if (true)
        {
            ProductBuyExchangeRate.text = (PlayerBase.ProductBuyPrice * 100).ToString() + " -> 100";
            ProductSaleExchangeRate.text = "100 -> " + (PlayerBase.ProductSalePrice * 100).ToString();
        }
    }

    public void CloseInterface()
    {
        Interface.ToggleButtonVisibility();
        Destroy(gameObject);
    }
}
