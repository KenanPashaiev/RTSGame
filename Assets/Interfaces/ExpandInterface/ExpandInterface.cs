using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpandInterface : MonoBehaviour
{
    public Interface Interface;

    public Text CreditsCost;
    public Text ProductsCost;
    public Text ResidentsCost;

    public void CloseInterface()
    {
        Interface.PlayerBase.UnMarkTiles();
        Interface.ToggleButtonVisibility();
        Destroy(gameObject);
    }

    private void Start()
    {
        CreditsCost.text = Base.ExpandCreditCost.ToString();
        ProductsCost.text = Base.ExpandProductCost.ToString();
        ResidentsCost.text = Base.ExpandResidentCost.ToString();
    }
}
