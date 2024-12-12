using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClickHandler : MonoBehaviour {
    public void OnButtonReset() {
        Debug.Log("Reset button clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OnButtonExit() {
        Debug.Log("Exit button clicked");
        Application.Quit();
    }
}
