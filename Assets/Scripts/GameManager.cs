using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject colourChanger;
    public GameObject[] easyObstacles;
    public GameObject[] mediumObstacles;
    public GameObject[] hardObstacles;

    public List<GameObject> currentObstacles;
    public TextMeshProUGUI scoreText;
    private GameObject player;
    private float objectOffset = 3.5f;
    private float currentHeight = 0;
    private int score = 0;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        spawnNewObstacle();
    }

    void Update() {
        if (currentObstacles != null) deleteOldObstacles();
        if (currentObstacles.Count < 5) spawnNewObstacle();
    }

    private void spawnNewObstacle() {

        // Create Colour Changer
        Instantiate(colourChanger, new Vector3(0, currentHeight - 1.5f, 0), Quaternion.identity);

        // Create Obstacle
        GameObject toSpawn = chooseObstacleToSpawn();
        Vector3 spawnLocation = new Vector3(0, currentHeight + (toSpawn.transform.lossyScale.y/2), 0);
        GameObject newObstacle = Instantiate(toSpawn, spawnLocation, Quaternion.identity);
        currentObstacles.Add(newObstacle);
        currentHeight += newObstacle.transform.lossyScale.y + objectOffset;
    }

    private GameObject chooseObstacleToSpawn() {
        int difficulty = Random.Range(0,50);
        switch(difficulty) {
            case <= 25:
                return easyObstacles[Random.Range(0, easyObstacles.Length)];
            case > 25 and < 42:
                return mediumObstacles[Random.Range(0, mediumObstacles.Length)];
            case >= 42:
                return hardObstacles[Random.Range(0, hardObstacles.Length)];
        }
    }

    private void deleteOldObstacles() {
        for (int i = 0; i < currentObstacles.Count; i++) {
            if (currentObstacles[i].transform.position.y < player.transform.position.y - 10) {
                Destroy(currentObstacles[i]);
                currentObstacles.RemoveAt(i);
            }
        }
    }

    public void updateScore() {
        score += 1;
        scoreText.text = score.ToString();
    }

    public int getScore() {
        return score;
    }
}
