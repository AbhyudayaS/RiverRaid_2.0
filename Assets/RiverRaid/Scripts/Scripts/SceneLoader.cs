using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private SceneState _scene;

    public void ChangeScene(int s)
    {
        _scene.Value = s;
        SceneManager.LoadScene(s);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
