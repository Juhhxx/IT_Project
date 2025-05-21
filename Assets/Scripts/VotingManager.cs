using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;

public class VotingManager : MonoBehaviour
{
    [SerializeField] private int _numberOfPlayers;
    [SerializeField] private GameObject _votingCanvas;
    [SerializeField] private GameObject _resultCanvas;
    [SerializeField] private TextMeshProUGUI _votingText;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Image _yesBar;
    [SerializeField] private Image _noBar;
    private bool[] _votes;
    private int _votesCast;
    [SerializeField] private float _voteWaitTime;
    private YieldInstruction _wfs;

    private void Start()
    {
        _wfs = new WaitForSeconds(_voteWaitTime);

        StartVote();
    }

    private void StartVote()
    {
        _votes = new bool[_numberOfPlayers];
        _votesCast = 0;

        StartCoroutine(RegisterVotes());
    }
    private IEnumerator RegisterVotes()
    {
        _votingCanvas.SetActive(true);

        while (_votesCast < _numberOfPlayers)
        {
            bool? vote = null;
            _votingText.text = $"Jogador {_votesCast + 1} Vota!";
            Debug.Log($"Jogador {_votesCast + 1} Vota!");

            while (vote == null)
            {
                if (Input.GetKeyDown(KeyCode.Y)) vote = true;
                if (Input.GetKeyDown(KeyCode.N)) vote = false;

                yield return null;
            }

            _votes[_votesCast] = vote.Value;

            Debug.Log($"VOTE {_votesCast + 1} CAST, {vote}");

            _votesCast++;

            yield return null;
        }

        _votingCanvas.SetActive(false);

        StartCoroutine(ShowVotes());
    }

    private IEnumerator ShowVotes()
    {
        _resultText.text = "";
        _resultCanvas.SetActive(true);

        int i = 0;
        float yes = 0;
        float no = 0;

        while (i < _numberOfPlayers)
        {
            if (_votes[i]) yes++;
            else no++;

            _yesBar.fillAmount = yes / _numberOfPlayers;
            _noBar.fillAmount = no / _numberOfPlayers;

            i++;

            yield return _wfs;
        }

        string result = yes > no ? "SIM" : "NÃO";

        _resultText.text = $"O Resultado é : {result}";

        yield return new WaitForKeyDown(KeyCode.Return);

        _resultCanvas.SetActive(false);

        StartVote();
    }

}
