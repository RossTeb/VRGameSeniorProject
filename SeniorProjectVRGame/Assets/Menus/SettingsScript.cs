using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {
	
	public Dropdown qualityOptions;
	public Dropdown screenResolutionOptions;
	public Toggle fullScreenToggle;
	public Dropdown antialiasingOptions;
	public Dropdown shadowsOptions;
	public Slider songVolumeSlider;
	public Toggle songToggle;
	public Slider soundsVolumeSlider;
	public Toggle soundsToggle;
    public Button visualsAndAudioButton;
    public Button gameButton;
    public Canvas visualsAndAudioCanvas;
    public Canvas gameCanvas;
    public Slider playTimeSlider;
    public InputField playTimeField;
    public Slider buildTimeSlider;
    public InputField buildTimeField;
    public Slider playerLivesSlider;
    public InputField playerLivesField;
    public Slider queenAmountSlider;
    public InputField queenAmountField;
    public Slider bishopAmountSlider;
    public InputField bishopAmountField;
    public Slider mineAmountSlider;
    public InputField mineAmountField;
    public Slider tarAmountSlider;
    public InputField tarAmountField;
    public Slider barrelAmountSlider;
    public InputField barrelAmountField;
    public AudioSource openingSong;
	public AudioSource clickNoise;
	private AudioSource[] audioSources;
	
	private int[] antialisingList = {0, 2, 4, 8};
	private string[] shadowsList = {"Disable", "HardOnly", "All"};
	
	void Start () {
		// Get Components
		qualityOptions = qualityOptions.GetComponent<Dropdown>();
		screenResolutionOptions = screenResolutionOptions.GetComponent<Dropdown>();
		fullScreenToggle = fullScreenToggle.GetComponent<Toggle>();
		antialiasingOptions = antialiasingOptions.GetComponent<Dropdown>();
		songVolumeSlider = songVolumeSlider.GetComponent<Slider>();
		songToggle = songToggle.GetComponent<Toggle>();
		soundsVolumeSlider = soundsVolumeSlider.GetComponent<Slider>();
		soundsToggle = soundsToggle.GetComponent<Toggle>();
        visualsAndAudioButton = visualsAndAudioButton.GetComponent<Button>();
        gameButton = gameButton.GetComponent<Button>();
        visualsAndAudioCanvas = visualsAndAudioCanvas.GetComponent<Canvas>();
        gameCanvas = gameCanvas.GetComponent<Canvas>();
        playTimeSlider = playTimeSlider.GetComponent<Slider>();
        playTimeField = playTimeField.GetComponent<InputField>();
        buildTimeSlider = buildTimeSlider.GetComponent<Slider>();
        buildTimeField = buildTimeField.GetComponent<InputField>();
        playerLivesSlider = playerLivesSlider.GetComponent<Slider>();
        playerLivesField = playerLivesField.GetComponent<InputField>();
        queenAmountSlider = queenAmountSlider.GetComponent<Slider>();
        queenAmountField = queenAmountField.GetComponent<InputField>();
        bishopAmountSlider = bishopAmountSlider.GetComponent<Slider>();
        bishopAmountField = bishopAmountField.GetComponent<InputField>();
        mineAmountSlider = mineAmountSlider.GetComponent<Slider>();
        mineAmountField = mineAmountField.GetComponent<InputField>();
        tarAmountSlider = tarAmountSlider.GetComponent<Slider>();
        tarAmountField = tarAmountField.GetComponent<InputField>();
        barrelAmountSlider = barrelAmountSlider.GetComponent<Slider>();
        barrelAmountField = barrelAmountField.GetComponent<InputField>();
        openingSong = openingSong.GetComponent<AudioSource>();
		clickNoise = clickNoise.GetComponent<AudioSource>();
		audioSources = UnityEngine.Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        // Hide Game Settings and make Visual Game Settings Button Untargetable
        visualsAndAudioCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        visualsAndAudioButton.interactable = false;

        // Initialize global variables
        game_settings.soundVolume = soundsVolumeSlider.value;
        game_settings.muteSounds = !soundsToggle.isOn;
        game_settings.playTime = (int)playTimeSlider.value;;
        game_settings.buildTime = (int)buildTimeSlider.value;
        game_settings.playerLives = (int)playerLivesSlider.value;
        game_settings.queenAmount = (int)queenAmountSlider.value;
        game_settings.bishopAmount = (int)bishopAmountSlider.value;
        game_settings.mineAmount = (int)mineAmountSlider.value;
        game_settings.tarAmount = (int)tarAmountSlider.value;
        game_settings.barrelAmount = (int)barrelAmountSlider.value;

        // Initialize the text of the input fields in Game Settings
        playTimeField.text = playTimeSlider.value.ToString();
        buildTimeField.text = buildTimeSlider.value.ToString();
        playerLivesField.text = playerLivesSlider.value.ToString();
        queenAmountField.text = queenAmountSlider.value.ToString();
        bishopAmountField.text = bishopAmountSlider.value.ToString();
        mineAmountField.text = mineAmountSlider.value.ToString();
        tarAmountField.text = tarAmountSlider.value.ToString();
        barrelAmountField.text = barrelAmountSlider.value.ToString();

        // Add listener to Full Screen Toggle
        fullScreenToggle.onValueChanged.AddListener(delegate {
			ToggleFullScreen(fullScreenToggle);
		});
		
		// Add options to Quality Dropdown and add listener
		qualityOptions.ClearOptions();
		int i = 0;
		foreach(string option in QualitySettings.names){
			qualityOptions.options.Add(new Dropdown.OptionData() {text = option});
			if(QualitySettings.GetQualityLevel() == i){
				qualityOptions.captionText.text = option;
				qualityOptions.value = i;
			}
			i++;	
		}
		qualityOptions.onValueChanged.AddListener(delegate {
			ChangeQuality(qualityOptions);
		});

		// Add options to Screen Resolution Dropdown and add listener
		screenResolutionOptions.ClearOptions();
		i = 0;
		foreach(Resolution option in Screen.resolutions){
			screenResolutionOptions.options.Add(new Dropdown.OptionData() {text = option.ToString()});
			if(Resolution.Equals(Screen.currentResolution, option)){
				screenResolutionOptions.captionText.text = option.width + " x " + option.height;
				screenResolutionOptions.value = i;
			}
			i++;	
		}
		screenResolutionOptions.onValueChanged.AddListener(delegate {
			ChangeScreenResolution(screenResolutionOptions);
		});
		
		// Add options to Antialiasing Dropdown and add listener
		antialiasingOptions.ClearOptions();
		i = 0;
		foreach(int option in antialisingList){
			antialiasingOptions.options.Add(new Dropdown.OptionData() {text = option.ToString() + "x Multi Sampling"});
			if(QualitySettings.antiAliasing == option){
				antialiasingOptions.captionText.text = option.ToString() + "x Multi Sampling";
				antialiasingOptions.value = i;
			}
			i++;	
		}
		antialiasingOptions.onValueChanged.AddListener(delegate {
			ChangeAntialaising(antialiasingOptions);
		});
		
		// Add options to Shadows Dropdown and add listener
		shadowsOptions.ClearOptions();
		i = 0;
		foreach(string option in shadowsList){
			shadowsOptions.options.Add(new Dropdown.OptionData() {text = option.ToString()});
			if(string.Equals(QualitySettings.shadows.ToString(), option)){
				shadowsOptions.captionText.text = option.ToString();
				shadowsOptions.value = i;
			}
			i++;	
		}
		shadowsOptions.onValueChanged.AddListener(delegate {
			ChangeShadows(shadowsOptions);
		});
		
		// Add listener to Song Volume Slider
		songVolumeSlider.onValueChanged.AddListener(delegate {
			ChangeSongVolume(songVolumeSlider);
		});
		songVolumeSlider.value = openingSong.volume;
		
		// Add listener to Song Toggle
		songToggle.onValueChanged.AddListener(delegate {
			StopOrPlayMusic(songToggle);
		});
		
		// Add listener to Song Volume Slider
		soundsVolumeSlider.onValueChanged.AddListener(delegate {
			ChangeSoundsVolume(soundsVolumeSlider);
		});
		foreach(AudioSource a in audioSources){
			if(!a.Equals(openingSong)){
				soundsVolumeSlider.value = a.volume;
				break;
			}
		}
		
		// Add listener to Song Toggle
		soundsToggle.onValueChanged.AddListener(delegate {
			StopOrPlaySounds(soundsToggle);
		});

        // Add listener to Visuals and Audio Settings Button
        visualsAndAudioButton.onClick.AddListener(delegate
        {
            HideGameSettings();
        });

        // Add listener to Game Settings Button
        gameButton.onClick.AddListener(delegate
        {
            HideVisualAndAudioSettings();
        });

        // Add listener to Play Time Slider
        playTimeSlider.onValueChanged.AddListener(delegate
        {
            ChangePlayTimeOnSlider(playTimeSlider);
        });

        // Add listener to Play Time Input Field
        playTimeField.onValueChanged.AddListener(delegate
        {
            ChangePlayTimeOnField(playTimeField);
        });

        // Add listener to Build Time Slider
        buildTimeSlider.onValueChanged.AddListener(delegate
        {
            ChangeBuildTimeOnSlider(buildTimeSlider);
        });

        // Add listener to Play Time Input Field
        buildTimeField.onValueChanged.AddListener(delegate
        {
            ChangeBuildTimeOnField(buildTimeField);
        });

        // Add listener to Player Lives Slider
        playerLivesSlider.onValueChanged.AddListener(delegate
        {
            ChangePlayerLivesOnSlider(playerLivesSlider);
        });

        // Add listener to Player Lives Input Field
        playerLivesField.onValueChanged.AddListener(delegate
        {
            ChangePlayerLivesOnField(playerLivesField);
        });

        // Add listener to Queen Amount Slider
        queenAmountSlider.onValueChanged.AddListener(delegate
        {
            ChangeQueenAmountOnSlider(queenAmountSlider);
        });

        // Add listener to Queen Amount Input Field
        queenAmountField.onValueChanged.AddListener(delegate
        {
            ChangeQueenAmountOnField(queenAmountField);
        });

        // Add listener to Bishop Amount Slider
        bishopAmountSlider.onValueChanged.AddListener(delegate
        {
            ChangeBishopAmountOnSlider(bishopAmountSlider);
        });

        // Add listener to Bishop Amount Input Field
        bishopAmountField.onValueChanged.AddListener(delegate
        {
            ChangeBishopAmountOnField(bishopAmountField);
        });

        // Add listener to Mine Amount Slider
        mineAmountSlider.onValueChanged.AddListener(delegate
        {
            ChangeMineAmountOnSlider(mineAmountSlider);
        });

        // Add listener to Mine Amount Input Field
        mineAmountField.onValueChanged.AddListener(delegate
        {
            ChangeMineAmountOnField(mineAmountField);
        });

        // Add listener to Tar Amount Slider
        tarAmountSlider.onValueChanged.AddListener(delegate
        {
            ChangeTarAmountOnSlider(tarAmountSlider);
        });

        // Add listener to Tar Amount Input Field
        tarAmountField.onValueChanged.AddListener(delegate
        {
            ChangeTarAmountOnField(tarAmountField);
        });

        // Add listener to Barrel Amount Slider
        barrelAmountSlider.onValueChanged.AddListener(delegate
        {
            ChangeBarrelAmountOnSlider(barrelAmountSlider);
        });

        // Add listener to Barrel Amount Input Field
        barrelAmountField.onValueChanged.AddListener(delegate
        {
            ChangeBarrelAmountOnField(barrelAmountField);
        });

    }
	
	// Change the quality of the game on Dropdown option click; changing the quality will also change the antialiasing and the shadows
	private void ChangeQuality(Dropdown target){
		QualitySettings.SetQualityLevel(target.value, true);
		antialiasingOptions.captionText.text = QualitySettings.antiAliasing.ToString() + "x Multi Sampling";
		antialiasingOptions.value = QualitySettings.antiAliasing / 2;
		shadowsOptions.captionText.text = QualitySettings.shadows.ToString();
		shadowsOptions.value = Array.IndexOf(shadowsList, shadowsOptions.captionText.text);
		clickNoise.Play();
	}

	// Change the screen resolution on Dropdown option click; will change the screen resolution, but will keep the predetermined fullscreen settings 
	private void ChangeScreenResolution(Dropdown target){
		Resolution chosenResolution = Screen.resolutions[target.value];
		Screen.SetResolution(chosenResolution.width, chosenResolution.height, fullScreenToggle.isOn);
		clickNoise.Play();
	}
	
	// Toggle full screen on or off (default on); will keep the screen resolution settings on the current screen resolution
	private void ToggleFullScreen(Toggle target){
		Resolution current = Screen.resolutions[screenResolutionOptions.value];
		Screen.SetResolution(current.width, current.height, target.isOn);
	}
	
	// Change the antialiasing to either 0, 2, 4, or 8; default quality settings only use 0 and 2
	private void ChangeAntialaising(Dropdown target){
		QualitySettings.antiAliasing = antialisingList[target.value];
		clickNoise.Play();
	}
	
	// Change the shadows to disabled, hard only, or both soft and hard shadows
	private void ChangeShadows(Dropdown target){
		switch(target.value){
			case 1: 
				QualitySettings.shadows = ShadowQuality.HardOnly;
				break;
			case 2:
				QualitySettings.shadows = ShadowQuality.All;
				break;
			default:
				QualitySettings.shadows = ShadowQuality.Disable;
				break;
		}
		clickNoise.Play();
	}
	
    // Change the song volume to the value of the slider
	private void ChangeSongVolume(Slider target){
		openingSong.volume = target.value;
	}
	
    // Stop or play the song based on the value of the toggle
	private void StopOrPlayMusic(Toggle target){
		if(target.isOn)
			openingSong.Play();
		else
			openingSong.Stop();
	}
	
    // Change the volume of all sounds other than the song to the value of the slider
	private void ChangeSoundsVolume(Slider target){
		foreach(AudioSource a in audioSources){
			if(!a.Equals(openingSong))
				a.volume = target.value;
		}
		game_settings.soundVolume = target.value;
	}
	
    // Mute or unmute all sounds other then the song based on the toggle
	private void StopOrPlaySounds(Toggle target){
        if (target.isOn)
        {
            foreach (AudioSource a in audioSources)
            {
                if (!a.Equals(openingSong))
                    a.mute = false;
            }
            game_settings.muteSounds = false;
        }
        else
        {
            foreach (AudioSource a in audioSources)
            {
                if (!a.Equals(openingSong))
                    a.mute = true;
            }
            game_settings.muteSounds = true;
        }
	}

    // Hide the Game Settings canvas, show the Visuals & Audio Settings canvas, and set the V&A button to untargetable
    private void HideGameSettings()
    {
        visualsAndAudioCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        visualsAndAudioButton.interactable = false;
        gameButton.interactable = true;
    }

    // Hide the Visual & Audio Settings canvas, show the Game Settings canvas, and set the Game button to untargetable
    private void HideVisualAndAudioSettings()
    {
        visualsAndAudioCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(true);
        visualsAndAudioButton.interactable = true;
        gameButton.interactable = false;
    }

    // Change the value of play time to the value of the slider
    private void ChangePlayTimeOnSlider(Slider target)
    {
        game_settings.playTime = (int)target.value;
        playTimeField.text = target.value.ToString();
    }

    // Change the value of play time to the value of the input field
    private void ChangePlayTimeOnField(InputField target)
    {
        if (int.Parse(target.text) >= 300 & int.Parse(target.text) <= 600)
        {
            game_settings.playTime = int.Parse(target.text);
            playTimeSlider.value = float.Parse(target.text);
        }
    }

    // Change the value of build time to the value of the slider
    private void ChangeBuildTimeOnSlider(Slider target)
    {
        game_settings.buildTime = (int)target.value;
        buildTimeField.text = target.value.ToString();
    }

    // Change the value of build time to the value of the input field
    private void ChangeBuildTimeOnField(InputField target)
    {
        if (int.Parse(target.text) >= 60 & int.Parse(target.text) <= 300)
        {
            game_settings.buildTime = int.Parse(target.text);
            buildTimeSlider.value = float.Parse(target.text);
        }
    }

    // Change the value of player lives to the value of the slider
    private void ChangePlayerLivesOnSlider(Slider target)
    {
        game_settings.playerLives = (int)target.value;
        playerLivesField.text = target.value.ToString();
    }

    // Change the value of player lives to the value of the input field
    private void ChangePlayerLivesOnField(InputField target)
    {
        if (int.Parse(target.text) > 0 & int.Parse(target.text) <= 5)
        {
            game_settings.playerLives = int.Parse(target.text);
            playerLivesSlider.value = float.Parse(target.text);
        }
    }

    // Change the amount of Queen Enemies allowed to the value of the slider
    private void ChangeQueenAmountOnSlider(Slider target)
    {
        game_settings.queenAmount = (int)target.value;
        queenAmountField.text = target.value.ToString();
    }

    // Change the amount of Queen Enemies allowed to the value of the input field
    private void ChangeQueenAmountOnField(InputField target)
    {
        if (int.Parse(target.text) >= 0 & int.Parse(target.text) <= 5)
        {
            game_settings.queenAmount = int.Parse(target.text);
            queenAmountSlider.value = float.Parse(target.text);
        }
    }

    // Change the amount of Bishop Enemies allowed to the value of the slider
    private void ChangeBishopAmountOnSlider(Slider target)
    {
        game_settings.bishopAmount = (int)target.value;
        bishopAmountField.text = target.value.ToString();
    }

    // Change the amount of Bishop Enemies allowed to the value of the input field
    private void ChangeBishopAmountOnField(InputField target)
    {
        if (int.Parse(target.text) >= 0 & int.Parse(target.text) <= 5)
        {
            game_settings.bishopAmount = int.Parse(target.text);
            bishopAmountSlider.value = float.Parse(target.text);
        }
    }

    // Change the amount of Mine Traps allowed to the value of the slider
    private void ChangeMineAmountOnSlider(Slider target)
    {
        game_settings.mineAmount = (int)target.value;
        mineAmountField.text = target.value.ToString();
    }

    // Change the amount of Mine Traps allowed to the value of the input field
    private void ChangeMineAmountOnField(InputField target)
    {
        if (int.Parse(target.text) >= 0 & int.Parse(target.text) <= 5)
        {
            game_settings.mineAmount = int.Parse(target.text);
            mineAmountSlider.value = float.Parse(target.text);
        }
    }

    // Change the amount of Tar Traps allowed to the value of the slider
    private void ChangeTarAmountOnSlider(Slider target)
    {
        game_settings.tarAmount = (int)target.value;
        tarAmountField.text = target.value.ToString();
    }

    // Change the amount of Tar Traps allowed to the value of the input field
    private void ChangeTarAmountOnField(InputField target)
    {
        if (int.Parse(target.text) >= 0 & int.Parse(target.text) <= 5)
        {
            game_settings.tarAmount = int.Parse(target.text);
            tarAmountSlider.value = float.Parse(target.text);
        }
    }

    // Change the amount of Explosive Barrel Traps allowed to the amount on the slider
    private void ChangeBarrelAmountOnSlider(Slider target)
    {
        game_settings.barrelAmount = (int)target.value;
        barrelAmountField.text = target.value.ToString();
    }

    // Change the amount of Explosive Barrel Traps allowed to value of the input field
    private void ChangeBarrelAmountOnField(InputField target)
    {
        if (int.Parse(target.text) >= 0 & int.Parse(target.text) <= 5)
        {
            game_settings.barrelAmount = int.Parse(target.text);
            barrelAmountSlider.value = float.Parse(target.text);
        }
    }

    // Update is called once per frame
    void Update () {
        /*
        Debug.Log("The sound volume is: " + game_settings.soundVolume);
        Debug.Log("Mute the sounds? " + game_settings.muteSounds);
        Debug.Log("The play time is: " + game_settings.playTime);
        Debug.Log("The build time is: " + game_settings.buildTime);
        Debug.Log("The amount of player lives is: " + game_settings.playerLives);
        Debug.Log("The amount of queens is: " + game_settings.queenAmount);
        Debug.Log("The amount of bishops is: " + game_settings.bishopAmount);
        Debug.Log("The amount of mines is: " + game_settings.mineAmount);
        Debug.Log("The amount of tar is: " + game_settings.tarAmount);
        Debug.Log("The amount of barrels is: " + game_settings.barrelAmount);
        */
    }
}
