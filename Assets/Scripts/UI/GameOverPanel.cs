using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    string GameOverMessage = "Your caravan survived {0} days and walked {1} steps.";

    public GameObject GameOverPanelGO;
    public Caravan Caravan;
    public TextMeshProUGUI ScoreMessage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        if (Caravan.Days <= 1)
            ScoreMessage.text = string.Format("Your caravan survived {0} day and walked {1} steps.", Caravan.Days, Caravan.Steps);
        else
            ScoreMessage.text = string.Format(GameOverMessage, Caravan.Days, Caravan.Steps);

        GameOverPanelGO.SetActive(true);
    }

    public void PlayAgain()
    {
        Caravan.Reset();
    }
}
