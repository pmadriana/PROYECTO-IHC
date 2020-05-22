using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEngine.Windows.Speech;
using System;


public class UserController : MonoBehaviour
{
    /*parte voz*/
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, int> actions = new Dictionary<string, int>();
    public ConfidenceLevel confidence = ConfidenceLevel.Low;
    /*termina*/

    public GameObject[] userPowers;
    private int actualPowerIndex = 1;
    private bool shoot = false;
    private GameObject shootPower;
    Vector3 startPosition;
    public bool trackStatus = false;
    public GameObject userBarrier;
    


    // Use this for initialization
    void Start()
    {

        actions.Add("fuego", 0);
        actions.Add("agua", 1);
        actions.Add("tierra", 2);
        actions.Add("disparar", 3);
        
        userPowers[actualPowerIndex].SetActive(true);
        startPosition = new Vector3(userPowers[actualPowerIndex].transform.position.x, userPowers[actualPowerIndex].transform.position.y, userPowers[actualPowerIndex].transform.position.z);
        List<String> acc = actions.Keys.ToList();
        if (StaticSetCalibration.calibration) {
            acc.Add("Jugar");
        }
        keywordRecognizer = new KeywordRecognizer(acc.ToArray(),confidence);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

        startPosition = new Vector3(userPowers[1].transform.position.x, userPowers[1].transform.position.y, userPowers[1].transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("u"))
            shoot = true;
        if (shoot)
        {
            shootPower = Instantiate(userPowers[actualPowerIndex]);
            if (actualPowerIndex == 0)
            {
                var main = shootPower.GetComponent<ParticleSystem>().main;
                main.scalingMode = ParticleSystemScalingMode.Local;
            }
            shootPower.transform.position = new Vector3(userPowers[actualPowerIndex].transform.position.x + 1, userPowers[actualPowerIndex].transform.position.y, userPowers[actualPowerIndex].transform.position.z);
            shootPower.GetComponent<ShootPower>().move = shoot;
            shoot = false;

        }

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        if (speech.text.Equals("Jugar")) {
            Debug.Log("Entrostart");
            StaticSetCalibration.calibration = false;
            userBarrier.GetComponent<UserBoxCollider>().invinsible = false;
        }
        else if (actions[speech.text] == 3)
        {   
            shoot = true;
        }
        else if(actions.ContainsKey(speech.text))
        {
            userPowers[actualPowerIndex].SetActive(false);
            actualPowerIndex = actions[speech.text];
            userPowers[actualPowerIndex].SetActive(true);
        }
        
    }

    public void trackDetected() {
        trackStatus = true;
    }

    public void trackLost()
    {
        trackStatus = false;
    }
}