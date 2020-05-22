using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Vuforia;

public class EnemyT : MonoBehaviour
{
    private Animator animator;
    private Vector3 originalPos;
    private int var_c = 0;
    public bool walk = false;
    public string type;
    public int life = 3;
    public GameObject lifeBar;
    public string markerId;

    public GameObject sonido_golpe;
    public GameObject sonido_muerte;
    public GameObject sonido_risa;

    public GameObject textLanzar;
    public GameObject textPoderes;
    public GameObject textGema;
    public GameObject textJuega;
    public GameObject textMatar;
    public GameObject textFuego;
    public GameObject textAgua;
    public GameObject textTierra;
    

    public GameObject bool_s;
    public GameObject bool_g;
    public GameObject bool_j;
    public GameObject bool_m;

    private int puntos = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("battle", 1);
        animator.SetInteger("moving", 2);
        if (bool_s.activeSelf)
        {
            StartCoroutine(showTextStart());
            bool_s.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (walk)
        {
            transform.Translate((Vector3.forward * Time.deltaTime) / 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        GameObject obj = other.gameObject; //poder
        Debug.Log(obj.tag);
        if ((obj.CompareTag("water") && type == "fire") || (obj.CompareTag("fire") && type == "earth") || (obj.CompareTag("rock") && type == "thunder"))
        {

            Debug.Log("Entro20");
            life--;
            Destroy(obj);

            if (life <= 0) ///muerte
            {
                puntos++;
               
                    textGema.SetActive(false);
                    textAgua.SetActive(false);
                    textTierra.SetActive(false);
                    textFuego.SetActive(false);
                    textPoderes.SetActive(false);
                    textMatar.SetActive(false);
                    textJuega.SetActive(true);
                bool_m.SetActive(false);
                bool_g.SetActive(false);



                Instantiate(sonido_muerte);
                Destroy(this.gameObject);
            }
            else ///golpe
            {
                if (bool_m.activeSelf)
                {
                    textAgua.SetActive(false);
                    textTierra.SetActive(false);
                    textFuego.SetActive(false);
                    textGema.SetActive(false);
                    textPoderes.SetActive(false);
                    textMatar.SetActive(true); // Enable the text so it shows
                      
                }


                lifeBar.transform.localScale = new Vector3(lifeBar.transform.localScale.x - 0.9f, lifeBar.transform.localScale.y, lifeBar.transform.localScale.z);
                StartCoroutine(playHitAnimation());
                Instantiate(sonido_golpe);
                var lifeRenderer = lifeBar.GetComponent<Renderer>();
                if (life == 2)
                    lifeRenderer.material.SetColor("_Color", Color.yellow);
                else if (life == 1)
                    lifeRenderer.material.SetColor("_Color", Color.red);
            }

        }

        if ((obj.CompareTag("water") && type != "fire") || (obj.CompareTag("rock") && type != "thunder") || (obj.CompareTag("fire") && type != "earth"))
        {
            if (bool_g.activeSelf)
            {
                textAgua.SetActive(false);
                textTierra.SetActive(false);
                textFuego.SetActive(false);
                textMatar.SetActive(false);
                textPoderes.SetActive(false);
                textGema.SetActive(true);

                bool_g.SetActive(false);
            }
            else
            {
                textGema.SetActive(false);
                textMatar.SetActive(false);
                textPoderes.SetActive(false);
                textAgua.SetActive(false);
                textTierra.SetActive(false);
                textFuego.SetActive(false);
                if (type == "fire")
                {
                    textAgua.SetActive(true);
                }
                if (type=="earth")
                {
                    textFuego.SetActive(true);
                }
                if (type=="thunder")
                {
                    textTierra.SetActive(true);
                }
            }
          

            StartCoroutine(playDeffendAnimation());
            Instantiate(sonido_risa);
            Destroy(obj);
        }




    }

    IEnumerator showTextStart()
    {

        textLanzar.SetActive(true); // Enable the text so it shows
        yield return new WaitForSeconds(5);
        textLanzar.SetActive(false); // Disable the text so it is hidden
        textPoderes.SetActive(true);


    }

    IEnumerator showTextGema()
    {
        textGema.SetActive(true); // Enable the text so it shows
        yield return new WaitForSeconds(5);
        textGema.SetActive(false); // Disable the text so it is hidden
    }
    IEnumerator showTextJugar()
    {
        textJuega.SetActive(true); // Enable the text so it shows
        yield return new WaitForSeconds(5);
        textJuega.SetActive(false); // Disable the text so it is hidden
    }
  
    IEnumerator playHitAnimation()
    {
        animator.SetInteger("moving", 11);
        yield return new WaitForSeconds(0.1f);
        animator.SetInteger("battle", 1);
        animator.SetInteger("moving", 2);

    }
    IEnumerator playDeffendAnimation()
    {
        animator.SetInteger("moving", 10);

        yield return new WaitForSeconds(0.1f);
        animator.SetInteger("battle", 1);
        animator.SetInteger("moving", 2);

        // animator.SetInteger("moving", 2);


    }


}

