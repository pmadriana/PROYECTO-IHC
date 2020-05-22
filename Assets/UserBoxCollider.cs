using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserBoxCollider : MonoBehaviour
{
    public bool invinsible = false;
    public int life = 3;
    public int score;
    public Material yellow;
    public Material red;
    public GameObject shield;

    private void Start()
    {
        invinsible = StaticSetCalibration.calibration;
    }

    private void Update()
    {
   
        if (score >= 50) {
            StaticSetCalibration.gameResult = true;
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!invinsible)
            {
                if (life > 0)
                {
                    life--;
                }
                if (life == 0)
                {
                    StaticSetCalibration.gameResult = false;
                    SceneManager.LoadScene(2);
                }
                else if (life == 1)
                {
                    Material[] mats = shield.GetComponent<Renderer>().materials;
                    mats[0] = red;
                    shield.GetComponent<Renderer>().materials = mats;
                }
                else if (life == 2)
                {
                    Material[] mats = shield.GetComponent<Renderer>().materials;
                    mats[0] = yellow;
                    shield.GetComponent<Renderer>().materials = mats;
                }
            }
            
            Destroy(other.gameObject);
        }
    }
}
