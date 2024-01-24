using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorCena : MonoBehaviour {
	private const string PASTA_CENAS = "Scenes/";
	public string cena;
	public bool loadOnTrigger = false;

	public void CarregarCena() {
		SceneManager.LoadScene(PASTA_CENAS + cena,LoadSceneMode.Single);
	}

	public static void CarregarCena(string cena) {
		SceneManager.LoadScene(PASTA_CENAS + cena,LoadSceneMode.Single);
        Time.timeScale = 1;
        DestravarCursor();
        AudioManager.InstanciaAudioManager.IniciarSonsCenaAtual();
    }

    public static void CarregarCenaAditivo(string cena) {
        SceneManager.LoadScene(PASTA_CENAS + cena, LoadSceneMode.Additive);
        Time.timeScale = 1;
        DestravarCursor();
    }

    public static string NomeCenaAtual(){
		return SceneManager.GetActiveScene().name;
	}

    public static void DestravarCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
