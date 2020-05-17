using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitObject : MonoBehaviour
{
    public string markerId = null;
    public string objectTag;
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
        if (!markerId.Equals("water"))
        {
            if (other.gameObject.CompareTag(objectTag) && other.gameObject.GetComponent<Enemy>().markerId.Equals(markerId)&& other.gameObject.name!="Wall")
            {
                Destroy(other.gameObject);
            }
        }
        if (!markerId.Equals("fire"))
        {
            if (other.gameObject.CompareTag(objectTag) && other.gameObject.GetComponent<Enemy>().markerId.Equals(markerId) && other.gameObject.name != "Wall")
            {
                Destroy(other.gameObject);
            }
        }
        if (!markerId.Equals("rock"))
        {
            if (other.gameObject.CompareTag(objectTag) && other.gameObject.GetComponent<Enemy>().markerId.Equals(markerId) && other.gameObject.name != "Wall")
            {
                Destroy(other.gameObject);
            }
        }
        else
        {
            if (other.gameObject.CompareTag(objectTag))
            {
                Destroy(other.gameObject);
            }
        }
        
        
    }
}
