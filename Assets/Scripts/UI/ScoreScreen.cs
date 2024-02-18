using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using LootLocker.Requests;
using System.Text.RegularExpressions;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class ScoreScreen : MonoBehaviour
{
    private bool isDead;
    private int metalMeasured;
    private float timeMeasured;

    private string metal;
    private string time;
    private string score;

    [SerializeField] private TextMeshProUGUI endMessage;

    [SerializeField] private TextMeshProUGUI metalAmount;
    [SerializeField] private TextMeshProUGUI timeAmount;
    [SerializeField] private TextMeshProUGUI scoreAmount;

    [SerializeField] private TextMeshProUGUI playerNames;
    [SerializeField] private TextMeshProUGUI playerScores;

    [SerializeField] private TMP_InputField playerNameInputfield;
    private readonly string leaderboardID = "globalHighscore";

    // Start is called before the first frame update
    void Start()
    {
        isDead = PlayerPrefs.GetInt("isDead") == 1;
        metalMeasured = PlayerPrefs.GetInt("currency");
        timeMeasured = PlayerPrefs.GetFloat("time");

        if (isDead)
        {
            endMessage.text = "YOU DIED!";
        }

        metal = $"{metalMeasured}";

        string timeMM = $"{(int)(timeMeasured / 60)}";
        string timeSS = $"{(int)(timeMeasured % 60)}".PadLeft(2, '0');
        time = $"{timeMM}:{timeSS}";

        if (!isDead)
            score = $"{CalcScore(metalMeasured, timeMeasured)}";
        else
            score = "0";

        metalAmount.text = metal;
        timeAmount.text = time;
        scoreAmount.text = score;

        StartCoroutine(SetupRoutine());
    }

    private int CalcScore(int metalMeasured, float timeMeasured)
    {
        float timeBasedScore = 9050f * Mathf.Exp(-(1f / 400) * timeMeasured);
        float metalFactor = Mathf.Pow(30 * metalMeasured, 0.75f) / 10;
        return (int)(timeBasedScore * metalFactor);
    }

    public void SubmitScore()
    {
        SetPlayerName();
        StartCoroutine(SubmitScoreRoutine(isDead ? 0 : CalcScore(metalMeasured, timeMeasured)));
        StartCoroutine(UpdateLeaderboard());
    }

    private void SetPlayerName()
    {
        LootLockerSDKManager.SetPlayerName(playerNameInputfield.text, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set player name");
            }
            else
            {
                Debug.Log("Could not set player name" + response.errorData.message);
            }
        });
    }

    public void CleanUpInput()
    {
        string text = playerNameInputfield.text;
        text = Regex.Replace(text, "[^a-zA-Z0-9]", "");
        text = text.Length > 8 ? text.Substring(0, 8) : text;
        text = text.ToLower();
        playerNameInputfield.text = text;
    }

    IEnumerator SetupRoutine()
    {
        Debug.Log("Started Setup Routine");
        yield return LoginRoutine();
        yield return FetchTopHighscoresRoutine();
    }

    IEnumerator UpdateLeaderboard()
    {
        yield return FetchTopHighscoresRoutine();
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully uploaded score");
                done = true;
            }
            else
            {
                Debug.Log("Failed" + response.errorData.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public IEnumerator FetchTopHighscoresRoutine()
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 10, 0, (response) =>
        {
            if (response.success)
            {
                string tempPlayerNames = "";
                string tempPlayerScores = "";

                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    tempPlayerNames += members[i].rank + ": ";
                    if (members[i].player.name != "")
                    {
                        tempPlayerNames += members[i].player.name;
                    }
                    else
                    {
                        tempPlayerNames += members[i].player.id;
                    }
                    tempPlayerScores += members[i].score + "\n";
                    tempPlayerNames += "\n";
                }
                done = true;
                playerNames.text = tempPlayerNames;
                playerScores.text = tempPlayerScores;
            }
            else
            {
                Debug.Log("Failed" + response.errorData.message);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
