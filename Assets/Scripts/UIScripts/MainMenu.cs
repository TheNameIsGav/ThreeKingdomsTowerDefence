using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
  public void p(string thing)
  {
		Debug.Log(thing);
	}
  [SerializeField]
  private string playButtonDest;

  public void PlayButton()
  {
    if (playButtonDest != "")
    {
      SceneManager.LoadScene(playButtonDest);
    }
    else
    {
      p("NO SCENE INPUTTED AS DESTINATION");
    }
  }


  private void QuitApp()
  {
    Application.Quit();
  }






}
