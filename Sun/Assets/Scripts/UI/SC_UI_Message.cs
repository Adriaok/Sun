using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_UI_Message : MonoBehaviour
{
    private float timeSinceSpawn = 0.0f;
    private float lifespan = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        CheckIfLifeIsOver();
    }

    private void CheckIfLifeIsOver()
    {
        if (timeSinceSpawn > lifespan)
            Destroy(this.gameObject);
    }
}
