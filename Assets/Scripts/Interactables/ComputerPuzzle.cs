using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ComputerPuzzle : Interactable
{
    [SerializeField] GameObject door;            // The door to open
    [SerializeField] GameObject puzzleCanvas;    // Puzzle Canvas
    [SerializeField] InteractablePuzzlesPieces[] puzzlePieces;  // Array of puzzle pieces

    private bool doorOpen;                       // Tracks door state
    private bool puzzleActive;                   // Tracks puzzle state

    private Quaternion[] correctRotations;       // Array to store the correct rotation for each piece
    public GameObject puzzlePiecePrefab; // Prefab of the puzzle piece
    public Transform puzzlePanel;        // Parent panel for the pieces
    public Sprite[] puzzleSprites;       // Array of sprites for pieces
    public PuzzleNavigationManager puzzleNavigationManager; // Reference to PuzzleNavigationManager

    void Start()
    {
        // Initialize the correct rotations based on your puzzle setup
        correctRotations = new Quaternion[puzzleSprites.Length];
        for (int i = 0; i < puzzleSprites.Length; i++)
        {
            // Here you can assign the correct rotation for each puzzle piece
            // For example, all pieces start at 0 degrees, or you can set them based on your puzzle design
            correctRotations[i] = Quaternion.Euler(0, 0, 0); // Adjust as needed
        }
    }

    void Update()
    {
        // Check for ESC key while the puzzle is active
        if (puzzleActive && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePuzzle();
        }

        // Check if all pieces are in the correct rotation
        if (puzzleActive && AllPiecesCorrectlyRotated())
        {
            SolvePuzzle();
        }
    }

    private void PopulatePuzzle(){
        // Clear previous puzzle pieces in the puzzlePanel
        foreach (Transform child in puzzlePanel)
        {
            Destroy(child.gameObject); // Remove all previous pieces
        }

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

        puzzlePieces = puzzlePiecesList.ToArray(); // Assign the newly created pieces to puzzlePieces

        Debug.Log(puzzlePieces.Length + " puzzle pieces created.");

        // Set the puzzleSprites in the PuzzleNavigationManager
        puzzleNavigationManager.puzzleSprites = puzzlePieces;
    }

    protected override void Interact()
    {
        Debug.Log("Interacted with computer console!");

        if (!doorOpen)
        {
            OpenPuzzle();
        }
    }

    private void OpenPuzzle()
    {
        Debug.Log("Opening puzzle canvas...");
        puzzleCanvas.SetActive(true); // Show the puzzle UI
        Time.timeScale = 0;           // Pause the game
        
        PopulatePuzzle();
        puzzleActive = true;          // Track that puzzle is active
    }

    public void ClosePuzzle()
    {
        Debug.Log("Closing puzzle canvas...");
        puzzleCanvas.SetActive(false); // Hide the puzzle UI
        Time.timeScale = 1;            // Resume the game
        puzzleActive = false;          // Track that puzzle is inactive
    }

    public void SolvePuzzle()
    {
        Debug.Log("Puzzle solved!");
        ClosePuzzle(); // Close the puzzle UI

        // Open the door
        doorOpen = true;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
    }

    private bool AllPiecesCorrectlyRotated()
    {
        if (puzzlePieces.Length == 0)
        {
            Debug.LogWarning("No puzzle pieces available to check rotation.");
            return false; // If no pieces exist, return false to prevent an immediate solve
        }

        for (int i = 0; i < puzzlePieces.Length; i++)
        {
            // Check if the piece's rotation is the same as the correct rotation (with a small tolerance)
            if (Quaternion.Angle(puzzlePieces[i].transform.rotation, correctRotations[i]) > 1f)
            {
                return false; // Return false if any piece is not correctly rotated
            }
        }
        return true; // All pieces are correctly rotated
    }
}
