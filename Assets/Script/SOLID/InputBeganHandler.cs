using UnityEngine;

public interface IInputBeganHandler
{
    void HandleInputBegan(Vector2 inputPosition, BallSelectionManager selectionManager);
}

public class InputBeganHandler : IInputBeganHandler
{
    public void HandleInputBegan(Vector2 inputPosition, BallSelectionManager selectionManager)
    {
        selectionManager.ClearSelectedBalls();
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.collider.gameObject;

            if (objectHit.CompareTag("Ball"))
            {
                selectionManager.SelectedBall = objectHit;
                selectionManager.firstColor = BallUtilities.GetBallColor(objectHit);
                selectionManager.OneSelection = true;
                selectionManager.AddSelectedBall(objectHit);
                BallUtilities.EnableOutline(objectHit);
            }
        }
    }
}

