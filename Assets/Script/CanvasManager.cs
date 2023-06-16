using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class CanvasManager : Singleton<CanvasManager>
{
    public TextMeshProUGUI txtGoalRed, txtGoalBlue, txtGoalGreen, txtGoalYellow;
    public TextMeshProUGUI txtMoves;
    DotController dotController;
    private Dictionary<DotTypes, int> ballCountsByColor;

    private void Awake()
    {
        txtGoalRed = GameObject.Find("Canvas/Panel/TextOutputGoal").GetComponent<TextMeshProUGUI>();
        txtGoalBlue = GameObject.Find("Canvas/Panel/TextOutputGoal (1)").GetComponent<TextMeshProUGUI>();
        txtGoalYellow = GameObject.Find("Canvas/Panel/TextOutputGoal (2)").GetComponent<TextMeshProUGUI>();
        txtGoalGreen = GameObject.Find("Canvas/Panel/TextOutputGoal (3)").GetComponent<TextMeshProUGUI>();
        txtMoves = GameObject.Find("Canvas/Panel/TextOutputMoves").GetComponent<TextMeshProUGUI>();
        txtGoalRed.text = "this goal";
        txtMoves.text = "amount";
    }

    private void Start()
    {
        dotController = FindObjectOfType<DotController>();
        ballCountsByColor = new Dictionary<DotTypes, int>();
        InitiateCanvas();
    }

    public void InitiateCanvas()
    {
        ballCountsByColor.Clear();
        int total = 0;
        foreach (DotTypes dotType in System.Enum.GetValues(typeof(DotTypes)))
        {
            int count = dotController.GetBallCountByColor(dotType);
            ballCountsByColor[dotType] = count;
            total += count;
            if (dotType == DotTypes.Red)
            {
                txtGoalRed.text = count.ToString();
            }
            if (dotType == DotTypes.Blue)
            {
                txtGoalBlue.text = count.ToString();
            }
            if (dotType == DotTypes.Yellow)
            {
                txtGoalYellow.text = count.ToString();
            }
            if (dotType == DotTypes.Green)
            {
                txtGoalGreen.text = count.ToString();
            }
        }

        txtMoves.text = total.ToString();
    }
 
    public void UpdateCanvas(Dictionary<DotTypes, int> destroyedBallColors)
    {
        foreach (KeyValuePair<DotTypes, int> kvp in destroyedBallColors)
        {
            if (ballCountsByColor.ContainsKey(kvp.Key))
            {
                ballCountsByColor[kvp.Key] -= kvp.Value;
            }

            switch (kvp.Key)
            {
                case DotTypes.Red:
                    txtGoalRed.text = ballCountsByColor[kvp.Key].ToString();
                    break;
                case DotTypes.Blue:
                    txtGoalBlue.text = ballCountsByColor[kvp.Key].ToString();
                    break;
                case DotTypes.Green:
                    txtGoalGreen.text = ballCountsByColor[kvp.Key].ToString();
                    break;
                case DotTypes.Yellow:
                    txtGoalYellow.text = ballCountsByColor[kvp.Key].ToString();
                    break;
            }
        }

        bool allZero = ballCountsByColor.Values.All(count => count == 0);

        if (allZero)
        {
            FinishedGame();
            allZero = false;
        }
    }

    public void FinishedGame()
    {
        AudioPlayer.instance.PlaySound3();
        Debug.Log("GameFinished");
    }
}
