using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomEnemy : DefaultTrackableEventHandler
{
    public GameObject[] enemies;
    public GameObject renderReference;
    private ArrayList enemiesOnScreen = new ArrayList();
    public GameObject wallLimit;
    protected override void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            wallLimit.GetComponent<Collider>().enabled = true;
            InvokeRepeating("GenerateEnemies", 3.0f, 5.0f);
            //GenerateEnemies();
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
                
        }
    }
    void GenerateEnemies() {
        int enemyId = Random.Range(0,100)%3;
        Vector3 pos = new Vector3(renderReference.transform.position.x, renderReference.transform.position.y, renderReference.transform.position.z);
        GameObject enemy = Instantiate(enemies[enemyId],pos, Quaternion.Euler(renderReference.transform.rotation.x, -453.0f, renderReference.transform.rotation.z));
        enemy.transform.parent = this.transform;
        enemy.GetComponent<Enemy>().walk = true;
        enemy.GetComponent<Enemy>().markerId = this.name;


        enemiesOnScreen.Add(enemy);
        
    }
}
