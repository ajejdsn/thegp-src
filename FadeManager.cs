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


using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeManager : MonoBehaviour
{
    [Header("text")]
    public TMP_Text fadeText;
    public string fadeString;
    public float typeSpeed = 0.05f;
    public float Adolf = 7f; // No, it`s not Hitler, It`s a Patrick.

    [Header("Image")]
    public Image fadeFg;
    public float fadeDelay = 1.5f;
    public float fadeDuration = 0.5f;

    [Header("Audio")]
    public AudioSource musicSource;
    public SoundFXPlay sfxPlayer;

    private static bool alreadyPlayed = false;

    private void Start()
    {
        if (!alreadyPlayed)
        {
            StartCoroutine(PlaySeq());
            alreadyPlayed = true;
        } else
        {
            fadeText.enabled = false;
            fadeFg.enabled = false;
            if (musicSource != null) musicSource.Play(); 
        }
    }

    private IEnumerator PlaySeq()
    {
        yield return new WaitForSeconds(5f);
        fadeText.text = "";
        foreach (char c in fadeString)
        {
            fadeText.text += c;
            yield return new WaitForSeconds(typeSpeed);
            if (sfxPlayer != null) sfxPlayer.PlaySFX();
        }

        yield return new WaitForSeconds(fadeDelay); 
        if (fadeText != null) fadeText.enabled = false;

        float elapsed = 0f;
        if (fadeFg != null)
        {
            Color imgColor = fadeFg.color;
            float sAlpha = imgColor.a;

            if (musicSource != null) musicSource.Play();

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / fadeDuration);
                float newAlpha = Mathf.Lerp(sAlpha, 0f, t);
                fadeFg.color = new Color(imgColor.r, imgColor.g, imgColor.b, newAlpha); 
                yield return null;
            }

            fadeFg.enabled = false;
            fadeFg.color = new Color(imgColor.r, imgColor.g, imgColor.b, 0f);
        }
    }

    public void fadeX()
    {
        StartCoroutine(fadeMain()); // how it works? 
    }

    private IEnumerator fadeMain()
    {
        float elapsed = 0f;
        if (fadeFg != null && fadeText != null)
        {
            Color imgColor = fadeFg.color;
            float sAlpha = imgColor.a;

            fadeFg.enabled = true;
            fadeText.enabled = true;
            fadeText.text = "[LOADING...]";

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / fadeDuration);
                float newAlpha = Mathf.Lerp(sAlpha, 1f, t);
                fadeFg.color = new Color(imgColor.r, imgColor.g, imgColor.b, newAlpha);
                yield return null;
            }

            fadeFg.color = new Color(imgColor.r, imgColor.g, imgColor.b, 1f);
            yield return new WaitForSeconds(fadeDelay); // ________________________ CLEANUP END
            fadeFg.color = new Color(imgColor.r, imgColor.g, imgColor.b, 0f);
            fadeText.enabled = false;
            fadeFg.enabled = false;  //                  CLEANUP ^
        }
    }
}