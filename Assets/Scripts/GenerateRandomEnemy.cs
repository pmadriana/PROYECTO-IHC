using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomEnemy : DefaultTrackableEventHandler
{
    public GameObject[] enemies;
    public GameObject renderReference;
    private ArrayList enemiesOnScreen = new ArrayList();
    public GameObject wallLimit;
    public GameObject Entrenamiento;
    public bool tracking = false;
    protected override void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            wallLimit.GetComponent<Collider>().enabled = true;
            InvokeRepeating("GenerateEnemies", 3.0f, 5.0f);
            //GenerateEnemies();
            tracking = true;
        }

        if (OnTargetFound != null)
            OnTargetFound.Invoke();
    }
    protected override void OnTrackingLost()
    {
        CancelInvoke("GenerateEnemies");
        if (mTrackableBehaviour)
        {
            wallLimit.GetComponent<Collider>().enabled = false;

            foreach (Transform child in transform) {
                if (child.gameObject.CompareTag("Enemy")) {
                     Destroy(child.gameObject);
                }
            }
            tracking = false;
                
        }
    }
    void GenerateEnemies() {
        int enemyId = Random.Range(0,100)%3;
        Vector3 pos = new Vector3(renderReference.transform.position.x, renderReference.transform.position.y, renderReference.transform.position.z);
        GameObject enemy = Instantiate(enemies[enemyId],pos, Quaternion.Euler(renderReference.transform.rotation.x, -453.0f, renderReference.transform.rotation.z));
        enemy.transform.parent = this.transform;
        if (Entrenamiento.activeSelf)
        {
            enemy.SetActive(true);
            enemy.GetComponent<EnemyT>().walk = true;
            enemy.GetComponent<EnemyT>().markerId = this.name;
        }
        else
        {
            enemy.GetComponent<Enemy>().walk = true;
            enemy.GetComponent<Enemy>().markerId = this.name;
        }


        enemiesOnScreen.Add(enemy);
        
    }
}
