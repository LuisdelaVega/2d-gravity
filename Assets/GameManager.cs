using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform planetTransform;
    public GameObject asteroidPrefab;
    //[SerializeField] private float asteroidSpeed = 2;

    private bool spawnOnCooldown = false;

    // Update is called once per frame
    void Update()
    {
        if (!spawnOnCooldown)
            StartCoroutine(SpawnAsteroid());
    }

    private IEnumerator SpawnAsteroid()
    {
        spawnOnCooldown = true;
        yield return new WaitForSeconds(4);
        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);
        GameObject asteroid = Instantiate(asteroidPrefab, new Vector2(x, y), quaternion.identity);
        Vector3 v = planetTransform.position - asteroid.transform.position;
        asteroid.GetComponent<Rigidbody2D>().AddForce(v * 2);
        spawnOnCooldown = false;
    }
}
