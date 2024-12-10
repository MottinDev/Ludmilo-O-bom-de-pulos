using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    // Nome ou índice da próxima cena a ser carregada
    public string nextSceneName;
    // ou
    // public int nextSceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou no trigger é o jogador
        if (other.CompareTag("Player"))
        {
            // Carrega a próxima cena quando o jogador entra no trigger
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Carrega a próxima cena com base no nome ou índice
        // SceneManager.LoadScene(nextSceneName);
        // ou
        // SceneManager.LoadScene(nextSceneIndex);
        
        // Neste exemplo, usamos o nome da cena
        SceneManager.LoadScene(nextSceneName);
    }
}
