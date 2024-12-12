using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNavigationManager : MonoBehaviour
{
    public InteractablePuzzlesPieces[] puzzleSprites; // Array of all puzzle pieces

    void Start()
    {
        Debug.Log(puzzleSprites);
        if (puzzleSprites.Length > 0){
            
            // Select the first piece by default
            puzzleSprites[0].SelectPiece();
            InteractablePuzzlesPieces.selectedPiece = puzzleSprites[0];
        }

    }

    void Update()
    {
        // Listen for arrow key inputs to navigate
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            InteractablePuzzlesPieces.NavigatePieces(puzzleSprites, Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            InteractablePuzzlesPieces.NavigatePieces(puzzleSprites, Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InteractablePuzzlesPieces.NavigatePieces(puzzleSprites, Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            InteractablePuzzlesPieces.NavigatePieces(puzzleSprites, Vector2.right);
        }
    }
}
