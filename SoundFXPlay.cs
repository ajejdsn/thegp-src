
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

public class SoundFXPlay : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip sfxClip;
    [Range(0.5f, 1f)]
    public float minPitch = 0.8f;
    [Range(0.6f, 1.5f)]
    public float maxPitch = 1.2f;
    public AudioSource SFXSource;
    public AudioClip beepClip;

    public void BeepSFX()
    {
        if (SFXSource.clip != null)
        {
            SFXSource.clip = beepClip;
            SFXSource.PlayOneShot(SFXSource.clip); 
        }
    }
    public void PlaySFX()
    {
        if (SFXSource.clip == null || SFXSource.clip == beepClip && sfxClip != null)
        {
            SFXSource.clip = sfxClip;
        }
        if (SFXSource.clip != null)
        {
            float rPitch = Random.Range(minPitch, maxPitch);
            SFXSource.pitch = rPitch;
            SFXSource.PlayOneShot(SFXSource.clip);
        } else
        {
            Debug.LogError("Error 2");
        }
    }

}
