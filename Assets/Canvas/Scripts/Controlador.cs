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



        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
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
