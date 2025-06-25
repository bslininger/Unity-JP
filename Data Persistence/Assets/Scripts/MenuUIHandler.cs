using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_Text highScoreListNames;
    [SerializeField] private TMP_Text highScoreListScores;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<DataManager.HighScore> highScores = DataManager.Instance.HighScores;
        FillInHighScoreText();
        nameInput.onValueChanged.AddListener(OnNameInputChanged);
        startButton.interactable = false;
    }

    void OnNameInputChanged(string nameInput)
    {
        // Make the start button active only if the name field is not blank (or just whitespace)
        startButton.interactable = !string.IsNullOrWhiteSpace(nameInput);
    }

    void FillInHighScoreText()
    {
        List<DataManager.HighScore> highScores = DataManager.Instance.HighScores;
        string names = "";
        string scores = "";
        int index = 1;
        foreach (DataManager.HighScore highScoreData in highScores)
        {
            names += $"{index.ToString()}. {highScoreData.name}\n";
            scores += $"{highScoreData.score.ToString()}\n";
            ++index;
        }
        highScoreListNames.text = names;
        highScoreListScores.text = scores;
    }

    public void StartGame()
    {
        string name = nameInput.text;  // Gets the TextMeshPro input field for the name, the only input field on the canvas
        DataManager.Instance.Name = name.Trim();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        DataManager.Instance.SaveHighScores();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
