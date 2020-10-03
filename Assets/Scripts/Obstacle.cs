using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public SpawnController spawnController = null;
    public List<PartObstacle> partsObstacle = new List<PartObstacle>();
    public bool IsNeedUpdate = false;
    public int CurrenNumberOfDestructibleBlocks = 0;

    int[] array = new int[25];
    private System.Random _random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        if (spawnController == null)
        {
            spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
            if (!spawnController)
                Debug.Log("No SpawnController object could be found");

        }
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }
        Shuffle(array);
        float maxRandomNumberIndestructibleObjects = spawnController.MaxNumberOfImmuneBlocks;
        float minRandomNumberIndestructibleObjects = spawnController.MinNumberOfImmuneBlocks;
        CurrenNumberOfDestructibleBlocks = Mathf.RoundToInt(Random.Range(minRandomNumberIndestructibleObjects, maxRandomNumberIndestructibleObjects));
        for(int i = 0; i < array.Length; i++)
        {
            var element = array[i];
            Debug.Log("[" + i + "]" +element);
            if (element < CurrenNumberOfDestructibleBlocks)
            {
                partsObstacle[i].IsCanBreak = true;
            }
            else
            {
                partsObstacle[i].IsCanBreak = false;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsNeedUpdate)
        {
            foreach (var part in partsObstacle)
            {

            }
        }
    }
    private void Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(0, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

}
