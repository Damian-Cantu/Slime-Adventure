using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull, 
    wallInnerCornerDownLeft, wallInnerCornerDownRight,wallInnerCornerUpLeft, wallInnerCornerUpRight,
    wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft,
    wallFullEightDirections, wallUtileUp, wallUtileRight, wallUtileDown, wallUtileLeft, wallTopandBottom, wallRightandLeft,
    wallTopLeftBottomRight, wallTopRightBottomLeft;


    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions) {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach(var position in positions) {
            PaintSingleTile(tilemap,tile,position);
        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear() {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType,2);
        TileBase tile = null;
        if(WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if(WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if(WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if(WallTypesHelper.wallBottom.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        // else if(WallTypesHelper.wallFull.Contains(typeAsInt))
        // {
        //     tile = wallFull;
        // }
        if(tile!=null)
            PaintSingleTile(wallTilemap, tile, position);
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if(WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if(WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if(WallTypesHelper.wallInnerCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerUpLeft;
        }
        else if(WallTypesHelper.wallInnerCornerUpRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerUpRight;
        }
        else if(WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if(WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if(WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if(WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if(WallTypesHelper.wallTopLeftBottomRight.Contains(typeAsInt))
        {
            tile = wallTopLeftBottomRight;
        }
        else if(WallTypesHelper.wallTopRightBottomLeft.Contains(typeAsInt))
        {
            tile = wallTopRightBottomLeft;
        }
        else if(WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFullEightDirections;
        }
        else if(WallTypesHelper.wallUtileUp.Contains(typeAsInt))
        {
            tile = wallUtileUp;
        }
        else if(WallTypesHelper.wallUtileRight.Contains(typeAsInt))
        {
            tile = wallUtileRight;
        }
        else if(WallTypesHelper.wallUtileDown.Contains(typeAsInt))
        {
            tile = wallUtileDown;
        }
        else if(WallTypesHelper.wallUtileLeft.Contains(typeAsInt))
        {
            tile = wallUtileLeft;
        }
        else if(WallTypesHelper.wallTopandBottom.Contains(typeAsInt))
        {
            tile = wallTopandBottom;
        }
        else if(WallTypesHelper.wallRightandLeft.Contains(typeAsInt))
        {
            tile = wallRightandLeft;
        }
        if(tile!=null)
            PaintSingleTile(wallTilemap, tile, position);
    }
}
