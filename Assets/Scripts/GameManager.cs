using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _titleScreen;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private GameObject _endGameScreen;
    [SerializeField]
    private AudioSource _backgroundMusic;

    public GameObject enemyPrefab;
    public GameObject speedupPrefab;
    public Transform playerTransform;

    private float _score = 0;
    private float _enemySpawnBoundsZ = 50.0f;
    private float _enemySpawnBoundsX = 10.0f;
    private float _speedupSpawnBoundsX = 6.0f;
    private float _speedupSpawnBoundsZ = 20.0f;
    private float _enemySpawnRate = 5.0f;
    private float _speedupSpawnRate = 15.0f;

    private int _enemyCount;
    private int _maxEnemyAmount = 4;

    public bool isGameActive;

    public void StartGame()
    {
        _titleScreen.SetActive(false);

        isGameActive = true;
        StartCoroutine(AddScore());
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnSpeedup());
        _backgroundMusic.Play();
    }

    public void GameOver()
    {
        isGameActive = false;
        _endGameScreen.SetActive(true);
        _backgroundMusic.Stop();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator AddScore()
    {
        while (isGameActive)
        {
            _scoreText.text = "Score: " + _score;
            yield return new WaitForSeconds(1);
            _score += 1;
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (isGameActive)
        {
            _enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (_enemyCount < _maxEnemyAmount)
            {
                Instantiate(enemyPrefab, GenerateEnemySpawnPosition(), enemyPrefab.transform.rotation);
            }

            yield return new WaitForSeconds(_enemySpawnRate);
        }
    }

    IEnumerator SpawnSpeedup()
    {
        while (isGameActive)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-_speedupSpawnBoundsX, _speedupSpawnBoundsX), speedupPrefab.transform.position.y, Random.Range(-_speedupSpawnBoundsZ, _speedupSpawnBoundsZ));
            Instantiate(speedupPrefab, spawnPosition, speedupPrefab.transform.rotation);
            yield return new WaitForSeconds(_speedupSpawnRate);
        }
    }

    Vector3 GenerateEnemySpawnPosition()
    {
        return playerTransform.position + new Vector3(Random.Range(-_enemySpawnBoundsX, _enemySpawnBoundsX), 0, _enemySpawnBoundsZ);
    }
}
