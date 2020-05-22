using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.EventSystems;


public class SeleccionarPanel : MonoBehaviour
{

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();


    public GameObject PanelEnemigo;
    public GameObject PanelDemo;
    public GameObject video;



    bool VisibleEnemigo = false;
    bool VisibleDemo = false;
    // Start is called before the first frame update
    void Start()
    {
        actions.Add("debilidades", BotonEnemigosPulsado);
        actions.Add("demo", BotonDemoPulsado);
        actions.Add("regresar", BotonRegresoEne);



        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();

    }
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    public void BotonEnemigosPulsado() 
    {
        if (VisibleEnemigo == false)
        {
            PanelEnemigo.SetActive(true);
            VisibleEnemigo = true;
        }
    }

    public void BotonDemoPulsado()
    {
        if (VisibleDemo == false)
        {
            video.GetComponent<DemoVideoScript>().startVideo = true;
            Debug.Log("Inicio video");
            PanelDemo.SetActive(true);
            VisibleDemo = true;
            
        }
    }

    public void BotonRegresoEne()
    {
        if (VisibleEnemigo ==true)
        {

            PanelEnemigo.SetActive(false);
           
            VisibleEnemigo = false;
        }
    }

    public void BotonRegresoDemo()
    {
        if (VisibleDemo == true )
        {
            PanelDemo.SetActive(false);

            VisibleDemo = false;
           
        }
    }


}
