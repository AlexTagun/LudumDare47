using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public SpawnController spawnController = null;
    public List<PartObstacle> partsObstacle = new List<PartObstacle>();
    public int CurrenNumberOfIndestructibleBlocks = 0;
    public int CurrenNumberOfDestructibleBlocks = 0;
    public Material materialDestructibleBlocks = null;
    public Material materialIndestructibleBlocks = null;


    int[] array = new int[25];
    private System.Random _random = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
        Init();
    }
    void Update()
    {

    }
    public void CheckForDestructiveBlock()
    {
        Debug.Log("Проверка");
        Debug.Log(CurrenNumberOfDestructibleBlocks);
        if (CurrenNumberOfDestructibleBlocks == 0)
        {
            RefreshObstacle();
        }
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
        CurrenNumberOfIndestructibleBlocks = Mathf.RoundToInt(Random.Range(minRandomNumberIndestructibleObjects, maxRandomNumberIndestructibleObjects));
        CurrenNumberOfDestructibleBlocks = 25 - CurrenNumberOfIndestructibleBlocks;
        for (int i = 0; i < array.Length; i++)
        {
            var element = array[i];
            if (element < CurrenNumberOfIndestructibleBlocks)
            {
                MakeBlockIsIndestructible(partsObstacle[i]);
            }
            else
            {
                MakeBlockIsDestructible(partsObstacle[i]);
            }
        }
    }
    // Update is called once per frame
    

    public void MakeBlockIsIndestructible(PartObstacle block)
    {
        block.IsCanBreak = false;
        block.meshRenderer.material = materialIndestructibleBlocks;
    }
    public void MakeBlockIsDestructible(PartObstacle block)
    {
        block.IsCanBreak = true;
        block.meshRenderer.material = materialDestructibleBlocks;
    }

    public void RefreshObstacle()
    {
        Init();
        foreach (var part in partsObstacle)
        {
            part.gameObject.SetActive(true);
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
