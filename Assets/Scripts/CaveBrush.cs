using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace UnityEditor {


    [CustomGridBrush(false, true, false, "Cave Brush")]
    public class CaveBrush : GridBrushBase {

        public const string k_WallLayerName = "Walls";
        public const string k_FloorLayerName = "Floor";

        public TileBase m_Wall;
        public TileBase m_Floor;

        /**
         * Fetch the two existing Tilemap in the scene that separate walls and floor, based on position clicked
         * place a floor and add walls around if no floor is there.
         **/
        public override void Paint(GridLayout grid, GameObject layer, Vector3Int position)
        {
            Tilemap walls = GetWall();
            Tilemap floor = GetFloor();

            if (walls != null && floor != null)
            {
                floor.SetTile(position, m_Floor);

                if (walls.GetTile(position) == m_Wall)
                {
                    walls.SetTile(position, null);
                }

                for (int x = -1; x < 2; ++x)
                {
                    for (int y = -1; y < 2; ++y)
                    {
                        Vector3Int pos = new Vector3Int(position.x + x, position.y + y, position.z);
                        if (floor.GetTile(pos) != m_Floor)
                        {
                            walls.SetTile(pos, m_Wall);
                        }
                    }
                }

            }
        }


        public override void Erase(GridLayout grid, GameObject layer, Vector3Int position)
        {
            Tilemap walls = GetWall();
            Tilemap floor = GetFloor();

            if (walls != null && floor != null)
            {
                walls.SetTile(position, null);
                floor.SetTile(position, null);
            }
        }


        public static Tilemap GetWall()
        {
            GameObject go = GameObject.Find(k_WallLayerName);
            return go != null ? go.GetComponent<Tilemap>() : null;
        }

        public static Tilemap GetFloor()
        {
            GameObject go = GameObject.Find(k_FloorLayerName);
            return go != null ? go.GetComponent<Tilemap>() : null;
        }

        [MenuItem("Assets/Create/Cave Brush")]
        public static void CreateBrush()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Cave Brush", "New Cave Brush", "Asset", "Save Cave Brush", "Assets");
            if (path == "")
                return;
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<CaveBrush>(), path);
        }
    }

}