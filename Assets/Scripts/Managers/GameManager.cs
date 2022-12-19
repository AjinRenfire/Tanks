using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int NumRoundsToWin = 5;        
    public float StartDelay = 3f;         
    public float EndDelay = 3f;           
    public CameraControl CameraControl;   
    public Text MessageText;              
    public GameObject TankPrefab;         
    public TankManager[] Tanks;           


    private int _roundNumber;              
    private WaitForSeconds _startWait;     
    private WaitForSeconds _endWait;       
/*    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;       
*/

    private void Start()
    {
        _startWait = new WaitForSeconds(StartDelay);
        _endWait = new WaitForSeconds(EndDelay);

        SpawnAllTanks();
        SetCameraTargets();

        StartCoroutine(GameLoop());
    }


    private void SpawnAllTanks()
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            Tanks[i].Instance =
                Instantiate(TankPrefab, Tanks[i].SpawnPoint.position, Tanks[i].SpawnPoint.rotation) as GameObject;
            Tanks[i].PlayerNumber = i + 1;
            Tanks[i].Setup();
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[Tanks.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = Tanks[i].Instance.transform;
        }

        CameraControl.Targets = targets;
    }


    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

/*        if (m_GameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
*/    }


    private IEnumerator RoundStarting()
    {
        yield return _startWait;
    }


    private IEnumerator RoundPlaying()
    {
        yield return null;
    }


    private IEnumerator RoundEnding()
    {
        yield return _endWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < Tanks.Length; i++)
        {
            if (Tanks[i].Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

/*
    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            if (Tanks[i].m_Instance.activeSelf)
                return Tanks[i];
        }

        return null;
    }


    private TankManager GetGameWinner()
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            if (Tanks[i].m_Wins == m_NumRoundsToWin)
                return Tanks[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < Tanks.Length; i++)
        {
            message += Tanks[i].m_ColoredPlayerText + ": " + Tanks[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }
*/

    private void ResetAllTanks()
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            Tanks[i].Reset();
        }
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            Tanks[i].EnableControl();
        }
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            Tanks[i].DisableControl();
        }
    }
}