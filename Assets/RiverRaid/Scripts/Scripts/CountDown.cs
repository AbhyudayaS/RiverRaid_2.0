using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private GameState gameState;
    float currentTime = 0f;
    float startingTime = 3f;
    [SerializeField]
    private TextMeshProUGUI _countDownText;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime -= 1*Time.deltaTime;
        _countDownText.text = currentTime.ToString("0");
        if (currentTime <= 0)
        {
            currentTime = 0;
            StartCoroutine(DisableObject());
        }
    }

    IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(1f); 
        _countDownText.gameObject.SetActive(false);
        gameState.Value = States.PLAY;

    }
}
