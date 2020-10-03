using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartObstacle : MonoBehaviour
{
    public bool IsCanBreak = false;
    public Obstacle parent = null;
    public MeshRenderer meshRenderer = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (IsCanBreak)
            {
                parent.CurrenNumberOfDestructibleBlocks--;
                gameObject.SetActive(false);
            }
            // смерть врезающегося объекта
        }
    }
}
