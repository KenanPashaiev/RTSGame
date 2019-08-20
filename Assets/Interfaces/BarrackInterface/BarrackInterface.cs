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
    private float _maxTime = 4f;
    private float _timeLeft;

    public void TrainUnits()
    {
        if (!_isTraining)
        {
            var attackUnitsCount = AttackUnitInterface.TrainedUnitsCount;
            var defenceUnitsCount = DefenceUnitInterface.TrainedUnitsCount;
            var speedUnitsCount = SpeedUnitInterface.TrainedUnitsCount;

            if (!PlayerBase.CanTrainUnit())
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
        AttackUnitInterface.PlayerBase = PlayerBase;
        AttackUnitInterface.SetInfo(0);
        DefenceUnitInterface.PlayerBase = PlayerBase;
        DefenceUnitInterface.SetInfo(1);
        SpeedUnitInterface.PlayerBase = PlayerBase;
        SpeedUnitInterface.SetInfo(2);
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
                _isTraining = false;
                closeButton.interactable = true;

                ProgressBar.fillAmount = 1;
                var attackUnitsCount = AttackUnitInterface.TrainedUnitsCount;
                var defenceUnitsCount = DefenceUnitInterface.TrainedUnitsCount;
                var speedUnitsCount = SpeedUnitInterface.TrainedUnitsCount;

                for (int i = 0; i < attackUnitsCount; i++)
                    PlayerBase.TrainUnit(0);
                for (int i = 0; i < defenceUnitsCount; i++)
                    PlayerBase.TrainUnit(1);
                for (int i = 0; i < speedUnitsCount; i++)
                    PlayerBase.TrainUnit(2);
            }
        }
    }

    public void CloseInterface()
    {
        Interface.ToggleButtonVisibility();
        Destroy(gameObject);
    }
}
