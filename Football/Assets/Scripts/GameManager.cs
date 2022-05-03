using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IWorldContextInfoProvider
{
    private IAgent[] obstacles;
    [SerializeField] private SteeringAgent [] npcs;
    private Agent player;
    private AgentsGroup npcGroup;
    [SerializeField] private SimpleGrab _playerHands;

    private const float Y = -0.75f;
    [SerializeField] private PickableItem _ball;

    public GameObject _titleScreen;

    Vector3 _playerPosAtSpawn = new Vector3(15, Y, 10);
    Vector3 _ballPosAtSpawn = new Vector3(20 , Y, 10);

    private int goals;
    public bool gameActive;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Button exitButton;

    void Start()
    {
        retrieveWorldInfo();
        createTestGroup();
    }

    private void retrieveWorldInfo()
    {
        var playerGameObject = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObject)
        {
            player = playerGameObject.GetComponent<Agent>();
            player.initialise(this);
        }

        var obstacleGameObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        obstacles = new IAgent[obstacleGameObjects.Length];
        for (var i = 0; i < obstacles.Length; i++)
        {
            var obs = obstacleGameObjects[i].GetComponent<IAgent>();
            obs.initialise(this);
            obstacles[i] = obs;
        }

        var nonPlayingCharacters = GameObject.FindGameObjectsWithTag("NPC");
        npcs = new SteeringAgent[nonPlayingCharacters.Length];
        for (var i = 0; i < npcs.Length; i++)
        {
            var npc = nonPlayingCharacters[i].GetComponent<SteeringAgent>();
            npc.initialise(this);
            npc.target = player;

            npcs[i] = npc;
        }

    }

    private void createTestGroup()
    {
        npcGroup = new AgentsGroup();
        if (npcs.Length > 0)
        {
            var leader = npcs[0];
            npcGroup.setLeader(leader);

            npcGroup.addMembers(npcs);
        }
    }

    void Update()
    {
        float dt = Time.deltaTime;

        player.update(dt);

        foreach (var n in npcs)
        {
            n.update(dt);
        }
    }

    public IAgent[] getObstaclesForSector(Vector3 agentPosition)
    {
        return obstacles;
    }

    public IAgent getPlayerAgent()
    {
        return player;
    }

    public IAgent[] getNonPlayingAgents()
    {
        return npcs;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHands.DropItem(_ball);

            Destroy(_ball.gameObject);
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
            Instantiate(npcs[enemiesToSpawn], GenerateSpawnPos(), npcs[enemiesToSpawn].transform.rotation);

            //Set a random moving speed for each enemy spawned
            npcs[enemiesToSpawn].minSpeed = Random.Range(1, 10);
            npcs[enemiesToSpawn].maxSpeed = Random.Range(11, 20);
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
        player.transform.position = _playerPosAtSpawn;
        Instantiate(_ball, _ballPosAtSpawn, transform.rotation);
    }

    //Called after difficulty selection
    public void StartGame(int difficulty)
    {
        //Used to send info to other classes
        gameActive = true;

        //Spawn game ball
        Instantiate(_ball, _ballPosAtSpawn, transform.rotation);

      
        SpawnEnemyWave(difficulty);

        //Hide title screen after start
        _titleScreen.gameObject.SetActive(false);
        //Display the exit button and the goal counter
        exitButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
    }
}
