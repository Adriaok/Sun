using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LightUp()
    {
        this.gameObject.SetActive(false);
    }

    public void LightDown()
    {
        this.gameObject.SetActive(true);
    }
}
