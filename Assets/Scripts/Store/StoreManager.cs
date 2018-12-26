using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Store.Tile;

namespace Store
{
    public class StoreManager : MonoBehaviour
    {

        [SerializeField]
        private int width;
        [SerializeField]
        private int height;

        [SerializeField]
        private TileManager[,] tiles;

        [SerializeField]
        private GameObject tileContainer;

        public TileManager tilePrefab;

        public bool regenerateFloor = false;

        void Start()
        {
            GenerateFloorplan();
        }

        void Update()
        {
            if (regenerateFloor)
            {
                regenerateFloor = false;
                DestroyFloorplan();
                GenerateFloorplan();
            }
        }

        private void DestroyFloorplan()
        {
            for (int x = 0; x < tiles.GetLength(1); x++)
            {
                for (int y = 0; y < tiles.GetLength(0); y++)
                {
                    Destroy(tiles[y, x].gameObject);
                }
            }
        }

        private void GenerateFloorplan()
        {
            tiles = new TileManager[height, width];
            GenerateTiles();

            EnableExteriorWalls();
        }

        private void EnableExteriorWalls()
        {
            for(int x=0; x<width; x++)
            {
                tiles[0, x].EnableWall(TileManager.Walls.NORTH);
                tiles[height - 1, x].EnableWall(TileManager.Walls.SOUTH);
            }

            for (int y = 0; y < height; y++)
            {
                tiles[y, 0].EnableWall(TileManager.Walls.WEST);
                tiles[y, width-1].EnableWall(TileManager.Walls.EAST);
            }
        }

        private void GenerateTiles()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Quaternion rotation = Quaternion.identity;
                    Vector3 position = new Vector3(tilePrefab.GetSizeZ() * y, 0, tilePrefab.GetSizeX() * x);
                    GameObject newTile = Instantiate(tilePrefab.gameObject, position, rotation, tileContainer.transform);
                    
                    
                    tiles[y, x] = newTile.GetComponent<TileManager>();

                }
            }
        }
    }
}