using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas startMenu;
	public Canvas settingsMenu;
	public AudioSource tickSound;

	void Start () {
        // Initialize game objects
        quitMenu = quitMenu.GetComponent<Canvas>();
        startMenu = startMenu.GetComponent<Canvas>();
		settingsMenu = settingsMenu.GetComponent<Canvas>();
		tickSound = tickSound.GetComponent<AudioSource>();
        quitMenu.gameObject.SetActive(false);
		settingsMenu.gameObject.SetActive(false);
		
		Screen.lockCursor = false;
    }
	
    // Make Quit Menu active, other inactive
    public void ExitPress(){
        startMenu.gameObject.SetActive(false);
		settingsMenu.gameObject.SetActive(false);
		quitMenu.gameObject.SetActive(true);
		tickSound.Play();
	}

    // Make Settings Menu active, others inactive
	public void SettingsPress(){
		quitMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(false);
		settingsMenu.gameObject.SetActive(true);
		tickSound.Play();
	}

    // Make Start Menu active from Settings Menu
	public void SettingsExit(){
        quitMenu.gameObject.SetActive(false);
		settingsMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
		tickSound.Play();
	}
	
    // Make Start menu active from Quit Menu
    public void NoPress(){
        quitMenu.gameObject.SetActive(false);
		settingsMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
		tickSound.Play();
    }

    // Go to scene in build defined by sceneNumber
    public void StartLevel(int sceneNumber){
		tickSound.Play();
        if (sceneNumber >= 0)
            SceneManager.LoadScene(sceneNumber);
    }

    // Close the game
    public void QuitGame(){
		tickSound.Play();
        Application.Quit();
    }

	void Update () {
		
	}
}
