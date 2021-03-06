﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Store.Tile
{
    public class TileManager : MonoBehaviour
    {

        [SerializeField]
        private GameObject wallContainer;

        [SerializeField]
        private GameObject northWall;
        [SerializeField]
        private GameObject southWall;
        [SerializeField]
        private GameObject eastWall;
        [SerializeField]
        private GameObject westWall;

        [SerializeField]
        private float sizeX;
        [SerializeField]
        private float sizeZ;

        public enum Walls
        {
            NORTH,
            EAST,
            SOUTH,
            WEST
        }

        void Start()
        {
            northWall = wallContainer.transform.Find("North").gameObject;
            southWall = wallContainer.transform.Find("South").gameObject;
            eastWall = wallContainer.transform.Find("East").gameObject;
            westWall = wallContainer.transform.Find("West").gameObject;
        }

        public void EnableWall(Walls wall)
        {
            switch (wall)
            {
                case Walls.NORTH:
                    northWall.SetActive(true);
                    break;
                case Walls.SOUTH:
                    southWall.SetActive(true);
                    break;
                case Walls.EAST:
                    eastWall.SetActive(true);
                    break;
                case Walls.WEST:
                    westWall.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        public void EnableWall(int wall)
        {
            EnableWall((Walls)wall);
        }

        public void DisableWall(int wall)
        {
            DisableWall((Walls)wall);
        }

        public void DisableWall(Walls wall)
        {
            switch (wall)
            {
                case Walls.NORTH:
                    northWall.SetActive(false);
                    break;
                case Walls.SOUTH:
                    southWall.SetActive(false);
                    break;
                case Walls.EAST:
                    eastWall.SetActive(false);
                    break;
                case Walls.WEST:
                    westWall.SetActive(false);
                    break;
                default:
                    break;
            }
        }

        public float GetSizeX()
        {
            return sizeX;
        }

        public float GetSizeZ()
        {
            return sizeZ;
        }
    }
}