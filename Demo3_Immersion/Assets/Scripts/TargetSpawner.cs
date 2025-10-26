using UnityEngine;

// Spawns targets at intervals within a defined range
public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;              // Prefab to spawn
    public float spawnRate = 1.0f;               // Time between spawns (seconds)
    public Vector2 xRange = new Vector2(-1.5f, 1.5f);
    public Vector2 yRange = new Vector2(1.0f, 2.0f);
    public float forwardSpeed = 1.5f;            // Speed assigned to TargetMover

    float timer;                                 // Spawn timer

    void Update()
    {
        // Spawn target at set intervals
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            timer = 0f;

            // Random spawn position within range
            var pos = transform.position +
                      new Vector3(Random.Range(xRange.x, xRange.y),
                                  Random.Range(yRange.x, yRange.y),
                                  0f);

            // Instantiate target and assign movement
            var go = Instantiate(targetPrefab, pos, transform.rotation);
            var mover = go.GetComponent<TargetMover>();
            if (!mover) mover = go.AddComponent<TargetMover>();
            mover.speed = forwardSpeed;

            // Ensure collider acts as a trigger
            var col = go.GetComponent<SphereCollider>();
            if (col) col.isTrigger = true;
        }
    }
}
