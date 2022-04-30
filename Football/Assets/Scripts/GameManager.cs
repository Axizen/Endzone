using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const float Y = -0.75f;
    [SerializeField] List<SteeringAgent> _enemies;


    public GameObject _titleScreen;
    public GameObject _gameBallPrefab;
    Vector3 _playerPosAtSpawn = new Vector3(0, Y, -3);
    Vector3 _ballPosAtSpawn = new Vector3(20 , Y, 10);

    private int obstacleCount = 3;

    private int goals;
    public bool gameActive;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button exitButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            Destroy(_gameBallPrefab.gameObject);
            Respawn();
        }
    }

    //Generate a random spawn position for obstacles & enemies
    private Vector3 GenerateSpawnPos()
    {
        float spawnX = Random.Range(-12, 12);
        float spawnZ = Random.Range(10, 20);

        Vector3 randomPos = new Vector3(spawnX, 0, spawnZ);

        return randomPos;
    }

    //Spawn enemies
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(_enemies[enemiesToSpawn], GenerateSpawnPos(), _enemies[enemiesToSpawn].transform.rotation);

            //Set a random moving speed for each enemy spawned
            _enemies[enemiesToSpawn].minSpeed = Random.Range(1, 10);
            _enemies[enemiesToSpawn].maxSpeed = Random.Range(11, 20);
        }
    }

    //Update goal counter after scored
    public void UpdateGoals(int goalsToAdd)
    {
        goals += goalsToAdd;
        scoreText.text = "Goals: " + goals;
    }

    public void ExitGame()
    {
        //Resets the scene again to title screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        exitButton.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
    }

    private void PauseApplication()
    {
        //gameManager.TogglePaused();
    }

    private void QuitApplication() { Application.Quit(); }

    //Respawn player after score
    public void Respawn()
    {
        transform.position = _playerPosAtSpawn;
        Instantiate(_gameBallPrefab, _ballPosAtSpawn, transform.rotation);
    }

    //Called after difficulty selection
    public void StartGame(int difficulty)
    {
        //Used to send info to other classes
        gameActive = true;

        //Spawn game ball
        Instantiate(_gameBallPrefab, _ballPosAtSpawn, transform.rotation);

      
        SpawnEnemyWave(difficulty);

        //Hide title screen after start
        _titleScreen.gameObject.SetActive(false);
        //Display the exit button and the goal counter
        exitButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }
}
