using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Store.Tile;
using System;

namespace Store
{
    public class WallPlacement : MonoBehaviour
    {
        private bool enablePlacement = false;

        private bool removeWall = false;
        private bool placeWall = false;

        [SerializeField]
        private LayerMask tileLayer;

        [SerializeField]
        private TileManager ghostTile;

        private int activeWall = 0;
        private KeyCode WallCycleButton = KeyCode.R;

        // true is clockwise, false is counterclockwise
        public void CycleActiveWall(bool direction)
        {
            ghostTile.DisableWall(activeWall);

            if (direction)
                activeWall = (activeWall + 1) % Enum.GetNames(typeof(TileManager.Walls)).Length;
            else
                activeWall = (activeWall == 0) ? Enum.GetNames(typeof(TileManager.Walls)).Length - 1 : activeWall - 1;

            ghostTile.EnableWall(activeWall);
        }

        public void EnablePlacement(bool enable)
        {
            enablePlacement = enable;
            ghostTile.EnableWall(activeWall);
        }

        private void Update()
        {
            if (enablePlacement)
            {
                HandleRotatingWall();
                HandlePlacementSelections();

                TileManager raycastResult = RaycastForTile();

                if(raycastResult != null)
                {
                    ghostTile.gameObject.SetActive(true);
                    ghostTile.transform.position = raycastResult.transform.position;

                    if (placeWall)
                    {
                        placeWall = false;
                        PlaceWall(raycastResult);
                    }

                    if (removeWall)
                    {
                        removeWall = false;
                        RemoveWall(raycastResult);
                    }

                } else {
                    ghostTile.gameObject.SetActive(false);
                }
            }
        }

        private void HandlePlacementSelections()
        {
            if (Input.GetMouseButtonDown(0))
            {
                placeWall = true;
            } else
            {
                placeWall = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                removeWall = true;
            } else
            {
                removeWall = false;
            }
        }

        private void PlaceWall(TileManager raycastResult)
        {
            raycastResult.EnableWall(activeWall);
        }

        private void RemoveWall(TileManager raycastResult)
        {
            raycastResult.DisableWall(activeWall);
        }

        private void HandleRotatingWall()
        {
            if (Input.GetKeyDown(WallCycleButton))
            {
                CycleActiveWall(!Input.GetKey(KeyCode.LeftShift));
            }
        }

        private TileManager RaycastForTile()
        {
            TileManager result;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, tileLayer))
            {
                result = hitInfo.transform.parent.GetComponent<TileManager>();

                if(result == null)
                {
                    result = hitInfo.transform.parent.parent.GetComponent<TileManager>();
                }

                return result;
            } else
            {
                return null;
            }
        }
    }
}