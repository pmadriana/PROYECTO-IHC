using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEngine.Windows.Speech;
using System;
using Vuforia;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
public class ResultController : MonoBehaviour
{
    public GameObject winText;
    public GameObject loseText;
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    // Start is called before the first frame update
    void Start()
    {
        actions.Add("Menu", BotonMenu);
        actions.Add("Salir", BotonSalir);

        Debug.Log("Start");
        Debug.Log(StaticSetCalibration.gameResult);

        if (StaticSetCalibration.gameResult)
        {
            winText.SetActive(true);
        }
        else {
            loseText.SetActive(true);
        }

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }
    public void BotonSalir()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void BotonMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
