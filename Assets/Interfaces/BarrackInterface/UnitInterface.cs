using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS;

public class UnitInterface : MonoBehaviour
{
    public Base PlayerBase;
    
    public Text Title;
    public Text Attack;
    public Text Defence;
    public Text Speed;
    public Text CreditsCost;
    public Text ProductsCost;
    public Text ResidentsCost;
    public InputField TrainedUnitInputField;

    public int TrainedUnitsCount
    {
        get
        {
            if (TrainedUnitInputField.text == string.Empty)
            {
                return 0;
            }
            return int.Parse(TrainedUnitInputField.text);
        }
        
    }
    
    public void SetInfo(int unitToTrain)
    {
        if (unitToTrain == 0)
        {
            Title.text = "Attack Unit";
            Attack.text = (AttackUnit.DefaultAttack + PlayerBase.UnitLevelBonus).ToString();
            Defence.text = (AttackUnit.DefaultDefence + PlayerBase.UnitLevelBonus).ToString();
            Speed.text = (AttackUnit.DefaultSpeed).ToString();
        }
        if (unitToTrain == 1)
        {
            Title.text = "Defence Unit";
            Attack.text = (DefenceUnit.DefaultAttack + PlayerBase.UnitLevelBonus).ToString();
            Defence.text = (DefenceUnit.DefaultDefence + PlayerBase.UnitLevelBonus).ToString();
            Speed.text = (DefenceUnit.DefaultSpeed).ToString();
        }
        if (unitToTrain == 2)
        {
            Title.text = "Speed Unit";
            Attack.text = (SpeedUnit.DefaultAttack + PlayerBase.UnitLevelBonus).ToString();
            Defence.text = (SpeedUnit.DefaultDefence + PlayerBase.UnitLevelBonus).ToString();
            Speed.text = (SpeedUnit.DefaultSpeed).ToString();
        }
        CreditsCost.text = Barracks.UnitTrainingCreditsCost.ToString() + " credits";
        ProductsCost.text = Barracks.UnitTrainingProductsCost.ToString() + " products";
        ResidentsCost.text = Barracks.UnitTrainingResidentsCost.ToString() + " resident";
    }
}
