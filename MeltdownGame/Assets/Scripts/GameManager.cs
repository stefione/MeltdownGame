using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int StartTime;
    [SerializeField] List<Transform> SpawnPoints;
    [SerializeField] CharacterController _playerPrefab;
    [SerializeField] CharacterController _botPrefab;
    [SerializeField] Rotator _rotator;
    private List<CharacterController> _characters;
    private bool _isGameOver;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _characters = new List<CharacterController>();
        int startIndex = Random.Range(0, SpawnPoints.Count);
        CharacterController player = Instantiate(_playerPrefab, SpawnPoints[startIndex].position, SpawnPoints[startIndex].rotation);
        _characters.Add(player);
        startIndex = (startIndex + 1) % SpawnPoints.Count;

        for (int i = 1; i < SpawnPoints.Count; i++)
        {
            CharacterController bot = Instantiate(_botPrefab, SpawnPoints[startIndex].position, SpawnPoints[startIndex].rotation);

            _characters.Add(bot);
            startIndex = (startIndex + 1) % SpawnPoints.Count;
        }
        StartCoroutine(Coroutine_CountdownToStart());
    }
    IEnumerator Coroutine_CountdownToStart()
    {
        CountdownScreen.Instance.Open();
        for (int i= StartTime; i>=0; i--)
        {
            CountdownScreen.Instance.ChangeText(i.ToString());
            yield return new WaitForSeconds(1);
        }
        CountdownScreen.Instance.Close();
        _rotator.StartRotating();
    }

    public void EliminateCharacter(CharacterController characterController)
    {
        _characters.Remove(characterController);
        if (_characters.Count == 1)
        {
            GameOver(_characters[0] is PlayerController);
        }
    }

    public void GameOver(bool victory)
    {
        if (_isGameOver)
        {
            return;
        }
        if (!victory)
        {
            GameOverPanel.Instance.Open("You Lost",(_characters.Count+1));
        }
        else
        {
            GameOverPanel.Instance.Open("You Win",1);
        }
        _isGameOver = true;
    }


}
