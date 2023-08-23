using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapMouse : MonoBehaviour
{
    public Tilemap tilemap; // Tilemap nesnesi buradan atanacak
    public Transform highlighter; // Highlighter'ın Transform bileşeni

    void Update()
    {
         // Fare pozisyonunu ekran koordinatlarından dünya koordinatlarına dönüştürme
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos); // Fare pozisyonunu cell pozisyonuna dönüştürme

        // Cell pozisyonunu dünya koordinatlarına dönüştürme ve tile'ın sol alt noktasını bulma
        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(cellPos);
        Vector3 tileBottomLeft = cellWorldPos - new Vector3(tilemap.cellSize.x / 2f, tilemap.cellSize.y / 2f, 0f);

        // Highlighter'ı güncel tile'in sol alt noktasına konumlandırma
        highlighter.position = tileBottomLeft;

        // Eğer tile varsa, istediğiniz işlemleri yapabilirsiniz
        TileBase currentTile = tilemap.GetTile(cellPos);
        if (currentTile != null)
        {
            Debug.Log("Üzerinde bulunulan tile: " + currentTile.name);
            // İşlemlerinizi burada gerçekleştirin
        }

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(highlighter.gameObject, tileBottomLeft, Quaternion.identity);
        }
    }
}
