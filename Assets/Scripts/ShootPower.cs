using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPower : MonoBehaviour
{
    public bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move) {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        
    }
}
