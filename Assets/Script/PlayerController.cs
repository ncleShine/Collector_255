using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject coinPrefab; // Reference to the coin prefab
    public float coinSpawnInterval = 1f; // Time interval for spawning coins
    private List<GameObject> collectedCoins = new List<GameObject>(); // List to store collected coins
    private int score = 0; // Player's score

    void Start()
    {
        InvokeRepeating("SpawnCoin", 0f, coinSpawnInterval); // Call SpawnCoin every interval
    }

    void Update()
    {
        MovePlayer();
        CheckCoinCollection(); // Check for coin collection
    }

    void MovePlayer()
    {
        float moveSpeed = 5f;
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0, 0);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void SpawnCoin()
    {
        float randomX = Random.Range(-7f, 7f); // Random position for coin spawn
        Instantiate(coinPrefab, new Vector3(randomX, 5, 0), Quaternion.identity); // Spawn coin
    }

    void CheckCoinCollection()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); // Check for nearby coins
        foreach (var hitCollider in hitColliders) // Iterate through colliders
        {
            if (hitCollider.CompareTag("Coin")) // Check if the object is a coin
            {
                collectedCoins.Add(hitCollider.gameObject); // Add to the list of collected coins
                Destroy(hitCollider.gameObject); // Remove coin from scene
                score++; // Increase score
                Debug.Log("Score: " + score); // Log score to console
            }
        }
    }
}
