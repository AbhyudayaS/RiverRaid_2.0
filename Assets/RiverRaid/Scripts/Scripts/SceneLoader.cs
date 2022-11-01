using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private SceneState _scene;
    [SerializeField]
    private PlayerState _playerState;
    [SerializeField]
    private Timer _timer;
    [SerializeField]
    private ScoreState _scoreState;

    public void ChangeScene(int s)
    {
        _scene.Value = s;
        SceneManager.LoadScene(s);
        _scoreState.Value = 0;
        _timer.Value = "";
        _playerState.Value = PState.SAFE;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
