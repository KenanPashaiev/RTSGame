using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayMenu : MonoBehaviour
{
    public GameObject MapPrefub;

    public InputField PlayerNameInputField;

    public Text GameStepCount;
    public Slider GameStepSlider;

    public Text MapSizeXCount;
    public Slider MapSizeXSlider;

    public Text MapSizeYCount;
    public Slider MapSizeYSlider;

    public void OnGameStepSliderEdit()
    {
        //var gameStepMax = 10f;

        var sliderValue = GameStepSlider.value;
        var gameStepValue = (int)(sliderValue * 9 + 1);
        GameStepCount.text = gameStepValue.ToString() + " seconds";
    }

    public void OnMapSizeXSliderEdit()
    {
        var mapSizeXMin = 15f;

        var sliderValue = MapSizeXSlider.value;
        var mapSizeX = (int)(sliderValue * 10 + mapSizeXMin);
        MapSizeXCount.text = mapSizeX.ToString() + " cells";
    }

    public void OnMapSizeYSliderEdit()
    {
        var mapSizeYMin = 15f;

        var sliderValue = MapSizeYSlider.value;
        var mapSizeY = (int)(sliderValue * 10 + mapSizeYMin);
        MapSizeYCount.text = mapSizeY.ToString()+ " cells";
    }

    public void OnPlayButtonClick()
    {
        GameObject mapGameObject;
        mapGameObject = Instantiate(MapPrefub, new Vector3(), new Quaternion());

        Map map = mapGameObject.GetComponent<Map>();

        //var gameStepMax = 10f;
        var gameStepSliderValue = MapSizeXSlider.value;
        var gameStep = (float)(gameStepSliderValue * 9 + 1f);
        map.GameStepLength = gameStep;

        var mapSizeXMin = 15f;
        var mapSizeXSliderValue = MapSizeXSlider.value;
        var mapSizeX = (int)(mapSizeXSliderValue * 10 + mapSizeXMin);
        map.MapSizeX = mapSizeX;

        var mapSizeYMin = 15f;
        var mapSizeYSliderValue = MapSizeYSlider.value;
        var mapSizeY = (int)(mapSizeYSliderValue * 10 + mapSizeYMin);
        map.MapSizeY = mapSizeY;

        var playerName = PlayerNameInputField.text;
        map.PlayerName = playerName;

        Destroy(gameObject); 
        gameObject.SetActive(false);
    }
}
