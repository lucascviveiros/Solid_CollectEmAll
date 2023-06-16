using System.Collections.Generic;

public interface IInputEndHandler
{
    void HandleInputEnd(BallSelectionManager selectionManager);
}

public class InputEndHandler : IInputEndHandler
{
    public void HandleInputEnd(BallSelectionManager selectionManager)
    {
        if (selectionManager.HasOneSelection())
        {
            Dictionary<DotTypes, int> destroyedBallColors = selectionManager.DestroySelectedBalls();
            CanvasManager.instance.UpdateCanvas(destroyedBallColors);
        }
        else if (selectionManager.HasNotSameColor)
        {
            selectionManager.ClearSelectedBalls();
            selectionManager.BreakingLine = true;
        }
        else if (selectionManager.GetSelectedBalls().Count > 1 && !selectionManager.BreakingLine)
        {
            //Debug.LogWarning("Line On Same Color!");
            Dictionary<DotTypes, int> destroyedBallColors = selectionManager.DestroySelectedBalls();
            CanvasManager.instance.UpdateCanvas(destroyedBallColors);
        }

        BallUtilities.DisableAllOutlines();
        selectionManager.BreakingLine = false;
        selectionManager.ClearSelectedBalls();
    }
}
