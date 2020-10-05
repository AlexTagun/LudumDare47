using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewIterationController : MonoBehaviour
{
    public SpawnController spawnController = null;
    public SpaceShipManager shipManager = null;
    public List<GameObject> activeBulletsOnScene = new List<GameObject>();
    public List<GameObject> activeClonesOnScene = new List<GameObject>();

    public Coroutine spawnCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        spawnController = (SpawnController)FindObjectOfType(typeof(SpawnController));
        shipManager = (SpaceShipManager)FindObjectOfType(typeof(SpaceShipManager));
    }

    // Update is called once per frame

    public void StartNewIteration()
    {
        foreach (var bullet in activeBulletsOnScene)
        {
            if (bullet != null)
            {
                Destroy(bullet);
            }
        }
        activeBulletsOnScene.Clear();
        foreach (var clone in activeClonesOnScene)
        {
            if (clone != null)
            {
                Destroy(clone);
            }
        }
        activeClonesOnScene.Clear();
        spawnController.ReplaceForNewIteration(transform.position);
        spawnCoroutine =  StartCoroutine(shipManager.startSpawnClones());

        FindObjectOfType<SpaceShipPlayerController>().startRecordingNewReplay();
    }
}
