using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileGetter : MonoBehaviour
{

    public Grid grid;
    public Tilemap map;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetTile(TilePosition.Left));
        // Debug.Log((this.transform.position).ToIntVector());
    }

    public TileBase GetTile(TilePosition tilePos = TilePosition.Under)
    {
        Vector3Int coordinate = grid.WorldToCell(this.transform.position);

        switch (tilePos)
        {
            case TilePosition.Above:
                return map.GetTile(grid.WorldToCell(this.transform.position + Vector3.up));
            case TilePosition.Under:
                return map.GetTile(grid.WorldToCell(this.transform.position + Vector3.down));
            case TilePosition.Over:
                return map.GetTile(grid.WorldToCell(this.transform.position));
            case TilePosition.Right:
                return map.GetTile(grid.WorldToCell(this.transform.position + Vector3.right));
            case TilePosition.Left:
                return map.GetTile(grid.WorldToCell(this.transform.position + Vector3.left));
        }

        return map.GetTile(grid.WorldToCell(this.transform.position));
    }
}
public enum TilePosition { Above, Under, Over, Right, Left}
