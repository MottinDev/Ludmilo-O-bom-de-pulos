using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopulatePuzzle : MonoBehaviour
{
    public GameObject puzzlePiecePrefab; // Prefab of the puzzle piece
    public Transform puzzlePanel;        // Parent panel for the pieces
    public Sprite[] puzzleSprites;       // Array of sprites for pieces
    public PuzzleNavigationManager puzzleNavigationManager; // Reference to PuzzleNavigationManager

    void Start()
    {
        List<InteractablePuzzlesPieces> puzzlePiecesList = new List<InteractablePuzzlesPieces>();

        foreach (var sprite in puzzleSprites)
        {
            // Instantiate the puzzle piece
            GameObject piece = Instantiate(puzzlePiecePrefab, puzzlePanel);

            // Assign the sprite to the piece
            piece.GetComponent<Image>().sprite = sprite;

            // Set a random rotation
            float randomRotation = Random.Range(0, 4) * 90; // Random rotation (0, 90, 180, 270 degrees)
            piece.transform.rotation = Quaternion.Euler(0, 0, randomRotation);

            // Add the reference to the InteractablePuzzlesPieces component to the list
            InteractablePuzzlesPieces pieceComponent = piece.GetComponent<InteractablePuzzlesPieces>();
            puzzlePiecesList.Add(pieceComponent);
        }

        // Set the puzzleSprites in the PuzzleNavigationManager
        puzzleNavigationManager.puzzleSprites = puzzlePiecesList.ToArray();
    }
}
