using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameMenuScript : MonoBehaviour
{

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    void Start()
    {
        actions.Add("regresar", returnToMainMenu);
        actions.Add("salir", exitGame);



        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void exitGame() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }


}
