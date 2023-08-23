using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class testing : MonoBehaviour
{
    private Grid grid;
    void Start()
    {
         float screenAspect = (float)Screen.width / Screen.height;
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = screenAspect * cameraHeight;

        int horizontalGridCount = Mathf.FloorToInt((cameraWidth - 2 * 0.32f) / 0.32f) + 1;
        int verticalGridCount = Mathf.FloorToInt((cameraHeight - 2 * 0.32f) / 0.32f) + 1;

        float xOffset = 0.32f; // Sol kenardan boşluk
        float yOffset = 0.32f; // Alt kenardan boşluk
        
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(xOffset, yOffset, 0));

        grid = new Grid(horizontalGridCount, verticalGridCount, 0.32f, screenBottomLeft);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
