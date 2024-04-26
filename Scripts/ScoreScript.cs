using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
   public enum Score
    {
        AiScore, PlayerScore
    }

    public Text AiScoreTxt, PlayerScoreTxt;

    public UIManagerScript uiManager;

    public int MaxScore;


    private int aiScore, playerScore;

    private int AiScore
    {
        get { return aiScore; }
        set
        {
            aiScore = value;
            if (value == MaxScore) 
            { 
                uiManager.ShowRestartCanvas(true);
            }
        }
    }

    private int PlayerScore
    {
        get { return playerScore; }
        set
        {
            playerScore = value;
            if (value == MaxScore)
            {
                uiManager.ShowRestartCanvas(false);
            }
        }
    }

    public void Increment(Score whichScore)
    {
        if(whichScore == Score.AiScore)
        
            AiScoreTxt.text = (++AiScore).ToString();
        else
            PlayerScoreTxt.text = (++PlayerScore).ToString();
    }

    public void Decrement(Score whichScore)
    {
        if (whichScore == Score.AiScore)
            if (AiScore == 0)
                return;
            else
            { 
                AiScoreTxt.text = (--AiScore).ToString();
            }

        else
            if (PlayerScore == 0)
                return;
            else
            {
                PlayerScoreTxt.text = (--PlayerScore).ToString();
            }
            
    }


    public void ResetScores()
    {
        AiScore = PlayerScore = 0;
        AiScoreTxt.text = PlayerScoreTxt.text = "0";
    }
}
