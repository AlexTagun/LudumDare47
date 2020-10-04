using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewIterationController : MonoBehaviour
{
    public SpawnController spawnController = null;
    public SpaceShipManager shipManager = null;
    public List<GameObject> activeBulletsOnScene = new List<GameObject>();
    public List<GameObject> activeClonesOnScene = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
        shipManager = (SpaceShipManager)FindObjectOfType(typeof(SpaceShipManager));
    }

    // Update is called once per frame

    public void StartNewIteration()
    {
        for (int i = 0; i < activeBulletsOnScene.Count; i++)
        {
            if(activeBulletsOnScene[i] != null)
            {
                Destroy(activeBulletsOnScene[i]);
            }
            activeBulletsOnScene.RemoveAt(i);
        }
        for (int i = 0; i < activeClonesOnScene.Count; i++)
        {
            Destroy(activeClonesOnScene[i]);
            activeClonesOnScene.RemoveAt(i);
        }
        spawnController.ReplaceForNewIteration(transform.position);
        StartCoroutine(shipManager.startSpawnClones(spawnController.positionSpawnClone));

        FindObjectOfType<SpaceShipPlayerController>().startRecordingNewReplay();
    }
}
