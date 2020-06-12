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

public class Controlador : MonoBehaviour
{

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
    public string[] Keywords_array;

    public GameObject PanelMenu;
    public GameObject PanelJugar;
    public GameObject PanelManual;
    public GameObject VideoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        actions.Add("Jugar", BotonJugar);
        actions.Add("Manual", BotonManual);
        actions.Add("Salir", BotonSalir);
        actions.Add("Menu", BotonMenu);
        actions.Add("Calibrar", BotonCalibrar);
        actions.Add("Entrenar", BotonEntrenar);
        actions.Add("Entrar a multijugador", BotonMulti);
        actions.Add("Entrar a normal", BotonNormal);


        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Keyword: " + args.text + "; Confidence: " + args.confidence + "; Start Time: " + args.phraseStartTime + "; Duration: " + args.phraseDuration);
        // write your own logic
    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log("Keyword: " + speech.text + "; Confidence: " + speech.confidence);
        // write your own logic
        actions[speech.text].Invoke();
    }

    public void BotonManual()
    {
        PanelManual.SetActive(true);
        PanelJugar.SetActive(false);
        PanelMenu.SetActive(false);
       
    }
    public void BotonJugar()
    {

        PanelManual.SetActive(false);
        PanelJugar.SetActive(true);
        PanelMenu.SetActive(false);

    }

    public void BotonNormal()
    {
        StaticSetCalibration.calibration = false;
        SceneManager.LoadScene(1);

    }

    public void BotonMulti()
    {
        StaticSetCalibration.calibration = false;
        SceneManager.LoadScene(1);
    }

    public void BotonMenu()
    {
        SceneManager.LoadScene("MainMenu");
        PanelManual.SetActive(false);
        PanelJugar.SetActive(false);
        PanelMenu.SetActive(true);

    }

    public void BotonCalibrar()
    {

        StaticSetCalibration.calibration = true;
        SceneManager.LoadScene(1);
    }
    public void BotonSalir()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void BotonEntrenar()
    {
        SceneManager.LoadScene("EntrenamientoScene");

    }

}
