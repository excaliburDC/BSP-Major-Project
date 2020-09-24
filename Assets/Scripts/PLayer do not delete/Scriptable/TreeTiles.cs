using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class TreeTiles : Tile
{

    public  bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (go != null)
        {
            go.GetComponent<SpriteRenderer>().sortingOrder = -position.y * 2;
        }
        return base.StartUp(position, tilemap, go);
    }
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/TreeTiles")]
    public static void CreateWaterTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Treetile", "TreeTile", "asset", "Save treetile", "Assets");
        if(path =="")
        {
            return;

        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TreeTiles>(), path);
    }
#endif
}
