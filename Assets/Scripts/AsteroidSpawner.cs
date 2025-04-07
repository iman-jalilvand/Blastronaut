using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Array of different asteroid types
    public float spawnRate = 2f; // Time between spawns
    public float spawnDistance = 30f; // Distance from the camera to spawn
    public float asteroidSpeed = 5f; // Speed at which asteroids move

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            SpawnAsteroid();
            timer = 0f;
        }
    }

    void SpawnAsteroid()
    {
        if (asteroidPrefabs.Length == 0) return;

        // Pick a random direction in front of the camera
        Vector3 spawnDirection = Random.onUnitSphere;
        spawnDirection.z = Mathf.Abs(spawnDirection.z); // Force them to come forward

        Vector3 spawnPosition = Camera.main.transform.position + spawnDirection * spawnDistance;

        // Pick a random asteroid prefab
        int index = Random.Range(0, asteroidPrefabs.Length);
        GameObject selectedAsteroid = asteroidPrefabs[index];

        // Instantiate asteroid at position with random rotation
        GameObject asteroid = Instantiate(selectedAsteroid, spawnPosition, Random.rotation);

        // Add force toward the camera/player
        Rigidbody rb = asteroid.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 forceDirection = (Camera.main.transform.position - spawnPosition).normalized;
            rb.linearVelocity = forceDirection * asteroidSpeed;
        }
    }
}
