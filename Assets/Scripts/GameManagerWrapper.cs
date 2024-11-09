using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerWrapper : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.OnStateEntered += GameManager_OnStateEntered;
        GameManager.CurrentBattleMap = TestMap;
    }

    public GameObject TestMap;

    private void GameManager_OnStateEntered(State state) {
        if (state == State.HomeScreen) {
            SceneManager.LoadScene("GameStart");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if(GameManager.CurrentState == State.GameStart) {
                GameManager.StateEnter(State.HomeScreen);
            }
        }
    }
}
