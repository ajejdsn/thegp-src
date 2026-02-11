
/*
 * © 2026 SnAjejd
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
using UnityEngine.UI;

public class ParamUI : MonoBehaviour
{
    public Slider musicVolumeSlider;
    void Start()
    {
        if (SettingsManager.Instance == null)
        {
            return;
        }
        musicVolumeSlider.value = SettingsManager.Instance.MusicVolume;

        musicVolumeSlider.onValueChanged.AddListener(SettingsManager.Instance.SetMusicVolume);
    }
    void OnDestroy()
    {
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.RemoveAllListeners();
        }
    }
}
