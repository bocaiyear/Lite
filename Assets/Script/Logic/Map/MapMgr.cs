
using Script.Resource;
using UnityEngine;

namespace Script.Logic
{
    public class MapMgr
    {
        private static GameObject[] cubes = new GameObject[9];
        
        private static readonly float WIDTH = 100;
        private static readonly Vector3[,] LAYOUT = new Vector3[3, 3];

        public static void Init()
        {
            LAYOUT[0, 0] = new Vector3(-WIDTH, 0, WIDTH);
            LAYOUT[0, 1] = new Vector3(0, 0, WIDTH);
            LAYOUT[0, 2] = new Vector3(WIDTH,0, WIDTH);
            LAYOUT[1, 0] = new Vector3(-WIDTH,0, 0);
            LAYOUT[1, 1] = new Vector3(0,0, 0);
            LAYOUT[1, 2] = new Vector3(WIDTH,0, 0);
            LAYOUT[2, 0] = new Vector3(-WIDTH,0, -WIDTH);
            LAYOUT[2, 1] = new Vector3(0,0, -WIDTH);
            LAYOUT[2, 2] = new Vector3(WIDTH,0, -WIDTH);
            
            ResourceMgr.LoadAssetAsync<GameObject>("Assets/Res/Model/Cube/Cube.prefab", c =>
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        GameObject cube = Object.Instantiate(c, Lite.SceneRoot);
                        cube.transform.localRotation = Quaternion.Euler(Vector3.zero);
                        cube.transform.localScale = new Vector3(WIDTH, 0.1f, WIDTH);
                        cube.transform.localPosition = LAYOUT[i, j];
                        cubes[i * 3 + j] = cube;
                    }
                }
            });
        }
    }
}