using System.Collections;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    public GameObject EnemyPrefab; // Assign this prefab in the Inspector
    private GameObject CurrentEnemy;

    void Start()
    {
        // Initial setup: Instantiate the first enemy
        CurrentEnemy = Instantiate(EnemyPrefab, transform.position, transform.rotation, transform);
        Debug.Log("Initial enemy setup complete.");
    }

    void Update()
    {
        // Check if the current enemy exists and has "died"
        if (CurrentEnemy != null && CurrentEnemy.GetComponent<EnemyDamage>().CurrentHealth <= 0)
        {
            Debug.Log("Enemy died. Respawning...");

            // Start the respawn coroutine
            StartCoroutine(RespawnEnemy());
        }
    }

    IEnumerator RespawnEnemy()
    {
        // Enable particle system for visual feedback
        GameObject particleSystem = this.transform.Find("Particle System").gameObject;
        particleSystem.SetActive(true);

        Debug.Log("Waiting to respawn...");
        yield return new WaitForSeconds(3f);
        Debug.Log("Wait complete. Respawning enemy...");

        // Disable particle system
        particleSystem.SetActive(false);

        // Destroy the old enemy if it exists
        if (CurrentEnemy != null)
        {
            Destroy(CurrentEnemy);
        }

        // Respawn a new instance of the enemy prefab
        CurrentEnemy = Instantiate(EnemyPrefab, transform.position, transform.rotation, transform);

        // Reset the health of the newly spawned enemy
        var enemyDamage = CurrentEnemy.GetComponent<EnemyDamage>();
        if (enemyDamage != null)
        {
            enemyDamage.CurrentHealth = enemyDamage.MaxHealth;
        }

        Debug.Log("Enemy respawned successfully.");
    }
}
