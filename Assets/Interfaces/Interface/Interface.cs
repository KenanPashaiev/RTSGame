using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public Text Timer;

    public Text CreditsCountText;
    public Text ProductsCountText;
    public Text ResidentsCountText;
    public Text UnitsCountText;

    public PortalInterface PortalInterface;
    private PortalInterface PortalInterfaceClone;

    public BarrackInterface BarrackInterface;
    private BarrackInterface BarrackInterfaceClone;

    public LevelUpInterface LevelUpInterface;
    private LevelUpInterface LevelUpInterfaceClone;

    public RaidInterface RaidInterface;
    private RaidInterface RaidInterfaceClone;

    public ExpandInterface ExpandInterface;
    private ExpandInterface ExpandInterfaceClone;

    public GameObject ButtonManager;
    
    public PlayerBase PlayerBase;

    public void UpdateValues()
    {
        float CreditsCount = (float)Math.Round(PlayerBase.Base.CreditsCount, 2);
        float ProductsCount = (float)Math.Round(PlayerBase.Base.ProductsCount, 2);
        int ResidentsCount = PlayerBase.Base.ResidentsCount;
        int UnitsCount = PlayerBase.Base.UnitsCount;

        CreditsCountText.text = CreditsCount.ToString();
        ProductsCountText.text = ProductsCount.ToString();
        ResidentsCountText.text = ResidentsCount.ToString();
        UnitsCountText.text = UnitsCount.ToString();
    }

    public void ToggleButtonVisibility()
    {
        ButtonManager.SetActive(!ButtonManager.activeSelf);
    }

    public void OnPortalButtonClick()
    {
        if (PortalInterfaceClone == null)
        {
            PortalInterfaceClone = Instantiate(PortalInterface, new Vector3(), new Quaternion());
            PortalInterfaceClone.Interface = this;
            PortalInterfaceClone.PlayerBase = PlayerBase.Base;
            ToggleButtonVisibility();
        }
    }

    public void OnBarrackButtonClick()
    {
        if (BarrackInterfaceClone == null)
        {
            BarrackInterfaceClone = Instantiate(BarrackInterface, new Vector3(), new Quaternion());
            BarrackInterfaceClone.Interface = this;
            BarrackInterfaceClone.PlayerBase = PlayerBase.Base;
            ToggleButtonVisibility();
        }
    }

    public void OnLevelUpButtonClick()
    {
        if (LevelUpInterfaceClone == null)
        {
            LevelUpInterfaceClone = Instantiate(LevelUpInterface, new Vector3(), new Quaternion());
            LevelUpInterfaceClone.Interface = this;
            LevelUpInterfaceClone.PlayerBase = PlayerBase.Base;
            ToggleButtonVisibility();
        }
    }

    public void OnExpandButtonClick()
    {
        if (ExpandInterfaceClone == null)
        {
            ExpandInterfaceClone = Instantiate(ExpandInterface, new Vector3(), new Quaternion());
            ExpandInterfaceClone.Interface = this;
            PlayerBase.Expand();
            ToggleButtonVisibility();
        }
    }

    public void OnRaidButtonClick()
    {
        if (RaidInterfaceClone == null)
        {
            RaidInterfaceClone = Instantiate(RaidInterface, new Vector3(), new Quaternion());
            RaidInterfaceClone.Interface = this;
            RaidInterfaceClone.PlayerBase = PlayerBase.Base;
            ToggleButtonVisibility();
        }
    }

    public void OnEscapeButtonClick()
    {
        PlayerBase.map.FinishGame();
        Destroy(gameObject);
    }

    private void Update()
    {
        float time = PlayerBase.map.time;

        string hours = Mathf.Floor((time % 21600) / 3600).ToString("00");
        string minutes = Mathf.Floor((time % 3600) / 60).ToString("00");
        string seconds = (time % 60).ToString("00");
        Timer.text = hours + ":" + minutes + ":" + seconds;
    }
}
