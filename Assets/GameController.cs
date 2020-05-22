using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject enemyTarget1;
    public GameObject enemyTarget2;
    public GameObject enemyTarget3;
    public GameObject userBarrier;
    public GameObject TextCalibration;
    public GameObject TextCalibrated;
    public static int score = 0;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (StaticSetCalibration.calibration) {
            TextCalibration.SetActive(true);
            userBarrier.GetComponent<UserBoxCollider>().invinsible = true;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyTarget1.GetComponent<GenerateRandomEnemy>().tracking && enemyTarget2.GetComponent<GenerateRandomEnemy>().tracking && enemyTarget3.GetComponent<GenerateRandomEnemy>().tracking)
        {
            if (StaticSetCalibration.calibration)
            {
                TextCalibrated.SetActive(true);
                TextCalibration.SetActive(false);
            }
            else
            {
                TextCalibrated.SetActive(false);
            }
        }
        else {
            if (StaticSetCalibration.calibration)
            {
                TextCalibrated.SetActive(false);
                TextCalibration.SetActive(true);
            }
            else
            {
                TextCalibration.SetActive(false);
            }

        }

    }
    public void updateScore(int scoreEnemy) {
        score += scoreEnemy;
        scoreText.GetComponent<Text>().text = "Puntaje: "+score.ToString();
        if (score >= 50)
        {
            StaticSetCalibration.gameResult = true;
            SceneManager.LoadScene(2);
        }
    }

    void updateText() { }

}
