using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Vector3 originalPos;
    public bool walk = false;
    public string type;
    public int life = 3;
    public string markerId;
    public GameObject lifeBar;

    public GameObject sonido_golpe;
    public GameObject sonido_muerte;
    public GameObject sonido_risa;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("battle", 1);
        animator.SetInteger("moving", 2);
       

    }

    // Update is called once per frame
    void Update()
    {
        if (walk) {
            transform.Translate((Vector3.forward * Time.deltaTime) / 3);
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject; //poder
        Debug.Log(obj.tag);
        if ( (obj.CompareTag("water") && type == "fire") || (obj.CompareTag("fire") && type == "earth") || (obj.CompareTag("rock") && type == "thunder"))
        {
           
            Debug.Log("Entro20");
            life--;
            Destroy(obj);

            if (life <= 0)
            {
                Instantiate(sonido_muerte);
                Destroy(this.gameObject);
            }
            else {
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

        if((obj.CompareTag("water") && type != "fire") || (obj.CompareTag("rock") && type != "thunder") || (obj.CompareTag("fire") && type != "earth"))
        {
            StartCoroutine(playDeffendAnimation());
            Instantiate(sonido_risa);
            Destroy(obj);
        }
      



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
