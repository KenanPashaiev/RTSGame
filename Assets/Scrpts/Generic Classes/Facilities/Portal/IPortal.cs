using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPortal
{
    bool BuyProducts(int boughtProductsCount);

    bool SellProducts(int soldProductsCount);
}
