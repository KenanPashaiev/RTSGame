using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS;

public class BarrackInterface : MonoBehaviour
{
    public Interface Interface;

    public Base PlayerBase;

    public UnitInterface AttackUnitInterface;
    public UnitInterface DefenceUnitInterface;
    public UnitInterface SpeedUnitInterface;

    public Text CreditsCost;
    public Text ProductsCost;
    public Text ResidentsCost;
    public Image ProgressBar;

    public Button closeButton;

    private bool _isTraining = false;
    private float _maxTime;
    private float _timeLeft;

    public void TrainUnits()
    {
        if (!_isTraining)
        {
            var attackUnitsCount = AttackUnitInterface.TrainedUnitsCount;
            var defenceUnitsCount = DefenceUnitInterface.TrainedUnitsCount;
            var speedUnitsCount = SpeedUnitInterface.TrainedUnitsCount;

            if (!PlayerBase.CanTrainUnit)
            {
                return;
            }

            _timeLeft = _maxTime;
            _isTraining = true;
            closeButton.interactable = false;
        }
    }

    public void OnUnitInputEdit()
    {
        var attackUnitsCount = AttackUnitInterface.TrainedUnitsCount;
        var defenceUnitsCount = DefenceUnitInterface.TrainedUnitsCount;
        var speedUnitsCount = SpeedUnitInterface.TrainedUnitsCount;

        var allUnitsCount = attackUnitsCount + defenceUnitsCount + speedUnitsCount;
        
        CreditsCost.text = (Barracks.UnitTrainingCreditsCost * allUnitsCount).ToString() + " credits";
        ProductsCost.text = (Barracks.UnitTrainingProductsCost * allUnitsCount).ToString() + " products";
        ResidentsCost.text = (Barracks.UnitTrainingResidentsCost * allUnitsCount).ToString() + " resident";
    }

    private void Start()
    {
        _maxTime = PlayerBase.Parent.map.GameStepLength;

        AttackUnitInterface.PlayerBase = PlayerBase;
        AttackUnitInterface.SetInfo(UnitType.attackUnit);
        DefenceUnitInterface.PlayerBase = PlayerBase;
        DefenceUnitInterface.SetInfo(UnitType.defenceUnit);
        SpeedUnitInterface.PlayerBase = PlayerBase;
        SpeedUnitInterface.SetInfo(UnitType.speedUnit);
    }

    private void Update()
    {
        if (_isTraining)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                ProgressBar.fillAmount = _timeLeft/_maxTime;
            }
            else
            {
                TrainFinish();
            }
        }
    }

    private void TrainFinish()
    {
        _isTraining = false;
        closeButton.interactable = true;

        ProgressBar.fillAmount = 1;
        var attackUnitsCount = AttackUnitInterface.TrainedUnitsCount;
        var defenceUnitsCount = DefenceUnitInterface.TrainedUnitsCount;
        var speedUnitsCount = SpeedUnitInterface.TrainedUnitsCount;

        for (int i = 0; i < attackUnitsCount; i++)
            PlayerBase.TrainUnit(UnitType.attackUnit);
        for (int i = 0; i < defenceUnitsCount; i++)
            PlayerBase.TrainUnit(UnitType.defenceUnit);
        for (int i = 0; i < speedUnitsCount; i++)
            PlayerBase.TrainUnit(UnitType.speedUnit);
    }

    public void CloseInterface()
    {
        Interface.ToggleButtonVisibility();
        Destroy(gameObject);
    }
}
