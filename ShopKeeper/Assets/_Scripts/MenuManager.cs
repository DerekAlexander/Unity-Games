using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public static MenuManager instance;

    public Button startButton;
    public Button controlsButton;
	public GameObject  controlsText;


    public Button endlessButton;
    public Button normalButton;
    public Button EasyButton;
    public Button MediumButton;
    public Button HardButton;

    static public string mode;
    static public string difficulty;

    private void Start()
    {
        instance = this;

    }
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void LoadInfo()
    {
        SceneManager.LoadScene("InfoScene");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void StartButton()
    {
        
        controlsButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);

        endlessButton.gameObject.SetActive(true);
        normalButton.gameObject.SetActive(true);

    }
    public void ModeButton(string s)
    {
        mode = s;
        endlessButton.gameObject.SetActive(false);
        normalButton.gameObject.SetActive(false);

        EasyButton.gameObject.SetActive(true);
        MediumButton.gameObject.SetActive(true);
        HardButton.gameObject.SetActive(true);


    }

    public void DifficultyButton(string s)
    {
        difficulty = s;
        LoadInfo();

    }
    public string Mode()
    {
        if (mode != null)
            return mode;
        else
            return "Normal";
    }

    public string Difficulty()
    {
        if (difficulty != null)
            return difficulty;
        else
            return "Easy";
    }

    public void Controls()
    {
		if (controlsText.activeSelf == false)
			controlsText.SetActive (true);
		
		else
			controlsText.SetActive (false);
    }
	public void Tutorial()
	{
		LoadTutorial ();
	}

    public void RestockButton()
    {
        GameObject restockPanel = Restock.instance.scrollView;

        #pragma warning disable 0618    
        restockPanel.active = !restockPanel.active;
        #pragma warning restore 0618

    }
    public void InventoryButton()
    {
        GameObject inventoryPanel = Inventory.instance.inventoryPanel;

        #pragma warning disable 0618
        inventoryPanel.active = !inventoryPanel.active;
        #pragma warning restore 0618

    }


}
