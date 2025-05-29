using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class VotingManager : MonoBehaviour
{
    [SerializeField] private int _numberOfPlayers;
    [SerializeField] private TMP_InputField _playerInputField;
    [SerializeField] private GameObject _startCanvas;
    [SerializeField] private GameObject _votingCanvas;
    [SerializeField] private GameObject _resultCanvas;
    [SerializeField] private TextMeshProUGUI _votingText;
    [SerializeField] private TextMeshProUGUI _directionText;
    [SerializeField] private TextMeshProUGUI _resultText;
    [SerializeField] private Image _yesBar;
    [SerializeField] private Image _noBar;
    private string[] _directions = new string[4] { "a DIREITA", "a ESQUERDA", "CIMA", "BAIXO" };
    private string _lastDirection;
    private bool[] _votes;
    private int _votesCast;
    [SerializeField] private float _voteWaitTime;
    private YieldInstruction _wfs;

    private void Start()
    {
        _wfs = new WaitForSeconds(_voteWaitTime);
    }

    public void StartVote()
    {
        if (int.TryParse(_playerInputField.text, out int np)) _numberOfPlayers = np;
        else return;

        if (_numberOfPlayers <= 2) return;

        _startCanvas.SetActive(false);

        StartCoroutine(RegisterVotes());
    }
    private string ChooseRandomDirection()
    {
        int rnd = Random.Range(0, 3);
        string dir = _directions[rnd];

        if (_lastDirection == dir) dir = ChooseRandomDirection();

        _lastDirection = dir;

        return dir;
    }
    private IEnumerator RegisterVotes()
    {
        string dir = ChooseRandomDirection();

        _directionText.text = $"Querem Andar Para {dir} ?";
        _votingCanvas.SetActive(true);

        _votes = new bool[_numberOfPlayers];
        _votesCast = 0;

        while (_votesCast < _numberOfPlayers)
        {
            bool? vote = null;
            _votingText.text = $"Jogador {_votesCast + 1} Vota!";
            Debug.Log($"Jogador {_votesCast + 1} Vota!");

            while (vote == null)
            {
                if ( Buttons.InputYes() ) vote = true;
                if ( Buttons.InputNo() ) vote = false;

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

        if (yes == no) result = "NÃO";

        _resultText.text = $"O Resultado é : {result}";

        yield return new WaitForKeyDown(KeyCode.Return);

        _resultCanvas.SetActive(false);

        StartCoroutine(RegisterVotes());
    }

}
