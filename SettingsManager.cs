
/*
 * © 2025 SnAjejd
 * Part of the TheGen project.
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <https://www.gnu.org/licenses/>.
 */
using UnityEngine;
using System;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioManager audioManager;


    public static SettingsManager Instance;
   

    private const string MusicVolumeKey = "MusicVolume";
    private const string SkinIndexKey = "SkinIndex"; // pls buy me some pizza
    public string bDay;
    public int bMonth;

    [Header("Settings")]
    [Range(0.0001f, 1f)]
    public float MusicVolume = 0.5f;

    public int CurrentSkinIndex = 0; // unity and alcohol are incompatible, trust me 
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetMusicVolume(float volume)
    {
        MusicVolume = volume;
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ApplyVolume(volume);
        }
        SaveSettings();
    }

    
    public void SetSkin(int index) // why is it still here?
    {
       
        // (^o.-^)
        SaveSettings(); 
    }


    private void SaveSettings()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume); // -Oh, uhm... What it that? Why is it white? And... Sticky?
        PlayerPrefs.SetInt(SkinIndexKey, CurrentSkinIndex); // LFMAOOOO WHAT TF IS THAT


        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {

        MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, MusicVolume);
       // CurrentSkinIndex = PlayerPrefs.GetInt(SkinIndexKey, CurrentSkinIndex);


        ApplyLoadedSettings();
    }


    private void ApplyLoadedSettings()
    {

        SetMusicVolume(MusicVolume);


//        SetSkin(CurrentSkinIndex); // Maggots!
    }
}