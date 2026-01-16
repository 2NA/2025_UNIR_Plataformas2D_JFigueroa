using System.Collections;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenEnemyInstantiations = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(InstantiateEnemies(timeBetweenEnemyInstantiations));
    }

    IEnumerator InstantiateEnemies(float timeBetweenInstantiations)
    {
        while (true)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity); //Instanciamos enemigo
            EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
            enemyController.player = player;
        
            yield return new WaitForSeconds(timeBetweenInstantiations);
        }
    }
}
