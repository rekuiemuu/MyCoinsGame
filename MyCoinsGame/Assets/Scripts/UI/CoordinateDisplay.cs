using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoordinateDisplay : MonoBehaviour
{
    private Text coordinateText;
    private Transform playerTransform;

    private void Start()
    {
        coordinateText = GetComponent<Text>();
        playerTransform = GameObject.FindWithTag("Player").transform; // Замените "Player" на тег вашего игрового объекта игрока
    }

    private void Update()
    {
        float x = playerTransform.position.x;
        float y = playerTransform.position.y;
        coordinateText.text = $"X: {x.ToString("F2")} Y: {y.ToString("F2")}"; // Форматирование с двумя знаками после запятой
    }
}
