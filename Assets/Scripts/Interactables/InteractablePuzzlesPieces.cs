using UnityEngine;
using UnityEngine.UI;

public class InteractablePuzzlesPieces : MonoBehaviour
{
    private RectTransform rectTransform;         // The RectTransform of the piece
    private Image image;                         // Image component for visual effects
    private bool isSelected = false;             // Tracks if this piece is currently selected
    public static InteractablePuzzlesPieces selectedPiece; // Tracks the currently selected piece

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        // Handle piece navigation if this piece is selected
        if (isSelected)
        {
            HandleRotation(); // Handle rotation with Space or Enter
        }
    }

    private void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            float currentRotation = rectTransform.rotation.eulerAngles.z;
            float newRotation = (currentRotation + 90) % 360; // Rotate in 90-degree increments
            rectTransform.rotation = Quaternion.Euler(0, 0, newRotation);
        }
    }

    public void SelectPiece()
    {
        // Mark this piece as selected
        isSelected = true;
        image.color = Color.blue; // Change border color to blue or highlight it
        selectedPiece = this;    // Update the static reference
    }

    public void DeselectPiece()
    {
        // Mark this piece as deselected
        isSelected = false;
        image.color = Color.white; // Reset border or highlight color
    }

    public static void NavigatePieces(InteractablePuzzlesPieces[] pieces, Vector2 direction)
    {
        if (selectedPiece == null) return;

        // Find the index of the currently selected piece
        int currentIndex = System.Array.IndexOf(pieces, selectedPiece);
        if (currentIndex == -1) return;

        // Determine the new index based on direction
        int newIndex = currentIndex;
        if (direction == Vector2.up) newIndex -= 1;      // Navigate up
        else if (direction == Vector2.down) newIndex += 1; // Navigate down
        else if (direction == Vector2.left) newIndex -= 1; // Navigate left
        else if (direction == Vector2.right) newIndex += 1; // Navigate right

        // Clamp the new index within valid bounds
        newIndex = Mathf.Clamp(newIndex, 0, pieces.Length - 1);

        // Deselect the current piece and select the new one
        selectedPiece.DeselectPiece();
        pieces[newIndex].SelectPiece();
    }
}
