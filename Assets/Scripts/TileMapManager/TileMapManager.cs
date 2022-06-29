using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

public class TileMapManager : MonoBehaviour
{

    [SerializeField] Tilemap _tilemap;
    [SerializeField] Tile _baseblockTile;
    [SerializeField] Tile _deapthBlockTile;
    [SerializeField] Tile _baseDeapthBottomBlockTile;
    [SerializeField] Tile _baseDeapthRightBottomLeftBlockTile;

    int _randNum;

    //下から上へチェックする
    void checkTileType(int x, int fillStartY, string type, int randNum)
    {
        //BaseTileだよ。左にも右にもタイルはないよ。
        if (
            _tilemap.GetTile(new Vector3Int(x - 1, randNum + fillStartY, 0)) == null
            && _tilemap.GetTile(new Vector3Int(x + 1, randNum + fillStartY, 0)) == null
            && type == "BaseTile"
           )
        {
            _tilemap.SetTile(new Vector3Int(x, _randNum + fillStartY, 0), _baseDeapthRightBottomLeftBlockTile);
            //Debug.Log("BaseTileだよ。左にも右にもタイルはないよ");
        }
    }
    void DrawTileMapToTop(int _fillMaximumX, int _fillStartY, int[] _yOption, Tile baseblockTile, Tile deapthBlockTile)
    {

        for (int x = 0; x < _fillMaximumX; x++)
        {
            _randNum = Common.GetRandomInt.GetRandomIndex(_yOption);
            _tilemap.SetTile(new Vector3Int(x, _randNum + _fillStartY, 0), baseblockTile);
            if (_randNum != 0)
            {
                while (_randNum > 0)
                {
                    _randNum--;
                    _tilemap.SetTile(new Vector3Int(x, _randNum + _fillStartY, 0), deapthBlockTile);
                }
            }
        }
    }
    void DrawTileMapToDown(int _fillMaximumX, int _fillStartY, int[] _yOption, Tile baseblockTile, Tile deapthBlockTile)
    {

        for (int x = 0; x < _fillMaximumX; x++)
        {
            _randNum = Common.GetRandomInt.GetRandomIndex(_yOption);
            _tilemap.SetTile(new Vector3Int(x, _randNum + _fillStartY, 0), baseblockTile);
            checkTileType(x, _fillStartY, "BaseTile", _randNum);
            if (_randNum != _yOption.Length)
            {
                while (_randNum < _yOption.Length - 1)
                {
                    _randNum++;
                    _tilemap.SetTile(new Vector3Int(x, _randNum + _fillStartY, 0), deapthBlockTile);
                    checkTileType(x, _fillStartY, "FillTile", _randNum);
                }
            }
        }
    }

    public List<Vector2> GetAirBlockPositionsFromMaximumXAndYAndRate(int maximumX, int y, int[] rate)
    {
        List<Vector2> positions = new List<Vector2>();
        for (int x = 0; x < maximumX; x++)
        {
            if (_tilemap.GetSprite(new Vector3Int(x, y, 0)) == null)
            {

                int rateIndex = Common.GetRandomInt.GetRandomIndex(rate);
                if (rateIndex == 0)
                {
                    positions.Add(new Vector2(x + 0.5f, y + 2));
                }
            }
        }
        return positions;

    }

    public void DestroyTile(int x, int y)
    {
        _tilemap.SetTile(new Vector3Int(x, y, 0), null);
    }

    public void Init()
    {
        DrawTileMapToTop(200, 0, new int[] { 10, 0, 0, 0 }, _baseblockTile, _deapthBlockTile);
        //DrawTileMapToTop(200, 0, new int[] { 0, 10, 0, 0 }, _baseblockTile, _deapthBlockTile);

        //洞窟、掘る前提のバージョン
        //DrawTileMapToDown(200, -4, new int[] { 7, 0, 0, 0 }, _baseDeapthBottomBlockTile, _deapthBlockTile);

        //DrawTileMapToTop(200, -3, new int[] { 7, 0, 0, 0 }, _baseblockTile, _deapthBlockTile);

        //DrawTileMapToTop(200, -6, new int[] { 7, 0, 0, 0 }, _baseblockTile, _deapthBlockTile);


        //ダンジョン横に歩けるバージョン
        DrawTileMapToDown(200, -4, new int[] { 0, 1, 3, 10 }, _baseDeapthBottomBlockTile, _deapthBlockTile);
        DrawTileMapToTop(200, -6, new int[] { 10, 3, 1, 7 }, _baseDeapthBottomBlockTile, _deapthBlockTile);


        //タイルを消す
        //tilemap.SetTile(new Vector3Int(0, 0, 0), null);
    }

}
