using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RTS;

public class LevelUpInterface : MonoBehaviour
{
    public Interface Interface;


    public Base PlayerBase;
    public GameObject ContentBox;
    public FacilityBox FacilityBox;
    
    public void AddFacilities()
    {
        ContentBox.GetComponent<RectTransform>().sizeDelta = new Vector2(304, 10 + 140 * (PlayerBase.facilityList.Count));
        for (int i = 0; i < PlayerBase.facilityList.Count; i++)
        {
            var facility = PlayerBase.facilityList[i];
            FacilityBox newFacilityBox;
            newFacilityBox = Instantiate(FacilityBox, new Vector3(), new Quaternion());//Create facility box
            newFacilityBox.transform.SetParent(ContentBox.transform);//Set canvas as facility box parent
            newFacilityBox.GetComponent<RectTransform>().localPosition = new Vector3(0, -10+i*(-140));//Set facility box posititon
            newFacilityBox.facility = facility;
            newFacilityBox.SetInfo();
        }
    }

    private void Start()
    {
        AddFacilities();
    }

    public void CloseInterface()
    {
        Interface.ToggleButtonVisibility();
        Destroy(gameObject);
    }
}
