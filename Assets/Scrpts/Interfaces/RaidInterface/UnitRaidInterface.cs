using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS;

public class UnitRaidInterface : MonoBehaviour
{
    public Base PlayerBase;

    public Text Attack;
    public Text Defence;
    public Text Speed;

    public Text UnitCountText;
    public Slider UnitSlider;

    public int UnitCount;

    private UnitType _unitType;

    public void SetInfo(UnitType unitType)
    {
        _unitType = unitType;
        switch (_unitType)
        {
            case UnitType.attackUnit:
                {
                    Attack.text = (AttackUnit.DefaultAttack + PlayerBase.UnitLevelBonus).ToString();
                    Defence.text = (AttackUnit.DefaultDefence + PlayerBase.UnitLevelBonus).ToString();
                    Speed.text = AttackUnit.DefaultSpeed.ToString();
                    break;
                }
            case UnitType.defenceUnit:
                {
                    Attack.text = (DefenceUnit.DefaultAttack + PlayerBase.UnitLevelBonus).ToString();
                    Defence.text = (DefenceUnit.DefaultDefence + PlayerBase.UnitLevelBonus).ToString();
                    Speed.text = DefenceUnit.DefaultSpeed.ToString();
                    break;
                }
            case UnitType.speedUnit:
                {
                    Attack.text = (SpeedUnit.DefaultAttack + PlayerBase.UnitLevelBonus).ToString();
                    Defence.text = (SpeedUnit.DefaultDefence + PlayerBase.UnitLevelBonus).ToString();
                    Speed.text = SpeedUnit.DefaultSpeed.ToString();
                    break;
                }
        }
    }

    public void OnSliderEdit()
    {
        switch (_unitType)
        {
            case UnitType.attackUnit:
                {
                    UnitCount = (int)(UnitSlider.value * PlayerBase.AttackUnitsCount);
                    UnitCountText.text = (UnitCount).ToString();
                    break;
                }
            case UnitType.defenceUnit:
                {
                    UnitCount = (int)(UnitSlider.value * PlayerBase.DefenceUnitsCount);
                    UnitCountText.text = (UnitCount).ToString();
                    break;
                }
            case UnitType.speedUnit:
                {
                    UnitCount = (int)(UnitSlider.value * PlayerBase.SpeedUnitsCount);
                    UnitCountText.text = (UnitCount).ToString();
                    break;
                }
        }
    }
}
