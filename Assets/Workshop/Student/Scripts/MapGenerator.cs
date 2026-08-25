using System;
using UnityEngine;

namespace Workshop.Student
{
    public class MapGenerator : MonoBehaviour
    {
        public int columns = 10;
        public int rows = 10;

        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;

        public string[,] saveItemMap = new string[3, 3] {
            { " ", "Soda", " "},
            { " ", " ", " "},
            { " ", " ", "Food"},
        };

        // 1. declare Players variable
        public GameObject[] playerTiles;

        // 7. declare Exit variable 
        public GameObject exitTile;


        public void Start()
        {
            // 1. random player at the position <0, 0> map

            if (playerTiles != null && playerTiles.Length > 0)
            {
                // กำหนดพิกัดตายตัว เช่น X = 0, Y = 0
                int x_Player = 0;
                int y_Player = 0;

                int r_Player = UnityEngine.Random.Range(0, playerTiles.Length);
                Instantiate(playerTiles[r_Player], new Vector2(x_Player, y_Player), Quaternion.identity);
                
            }

            // 2. create obstacles

            if (wallTiles != null && wallTiles.Length > 0)
            {
                int x_obstacles = columns / 2;
                for (int y_obstacles = 0; y_obstacles < rows / 2; y_obstacles++)
                {
                    int r_obstacles = UnityEngine.Random.Range(0, wallTiles.Length);
                    Instantiate(wallTiles[r_obstacles], new Vector2(x_obstacles, y_obstacles), Quaternion.identity);
                }
            }

            // 3. create floor
            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int r = UnityEngine.Random.Range(0, floorTiles.Length);
                    GameObject tile = Instantiate(floorTiles[0], new Vector2(x, y), Quaternion.identity);
                    tile.name = "Floor" + x + "_" + y;
                }
            }

            // 4. create walls

            for (int y = -1; y < rows + 1; y++)
            {
                for (int x = -1; x < columns + 1; x++)
                {
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        int r = UnityEngine.Random.Range(0, wallTiles.Length);
                        GameObject tile = Instantiate(wallTiles[r], new Vector2(x, y), Quaternion.identity);
                        tile.name = "Wall" + x + "_" + y;
                    }
                }
            }

            // 5. random foods

            int numberOfFood = UnityEngine.Random.Range(1, 3);

            for (int i = 0; i < numberOfFood; i++)
            {
                int x_Food = UnityEngine.Random.Range(0, columns);
                int y_Food = UnityEngine.Random.Range(0, rows);
                int r = UnityEngine.Random.Range(0, floorTiles.Length);
                Instantiate(foodTiles[0], new Vector2(x_Food, y_Food), Quaternion.identity);
            }
            // 6. generate item along with the saveItemMap

            for (int y = 0; y < saveItemMap.GetLength(0); y++) 
            {
                for (int x = 0; x < saveItemMap.GetLength(1); x++) 
                {
                    string item = saveItemMap[x, y];
                    if (!string.IsNullOrEmpty(item)) 
                    {
                        foreach (var foodTile in foodTiles) 
                        {
                            if (foodTile.name == item) 
                            {
                                GameObject food = Instantiate(foodTile, new Vector2(x, y), Quaternion.identity);
                                food.name = "Food " + x + "_" + y;
                                break;
                            }
                        }
                    }
                }
            }

            // 7. place exit
            Instantiate(exitTile, new Vector2(columns - 1, rows - 1), Quaternion.identity);

        }
    }
}
