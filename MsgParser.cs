
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
using TMPro;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class MsgParser : MonoBehaviour // It`s beta, I know that it`s bad.
{

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type); // I regret nothing.

    private const uint MB_ICONWARN = 0x00000030;
    public int violationCounter = 0;
    private string lastEye = "";
    private string lastMouth = "";
    private string lastAddExpr = "";
    private bool isShake = false;

    private Coroutine typingCoroutine;

    public float typingSpeed = 0.05f;

    [Header("UI Output")]
    public TMP_Text mainText;
    public TMP_Text expText;
    public TMP_Text dialText;
    public TMP_Text valueText;
    public TMP_FontAsset defFont;
    public TMP_FontAsset violFont;
    public Guardian guardian;

    [Header("Sprite Controller")]
    public EmotionController spriteController;

    [Header("Voice")]
    public AudioSource voiceSource;
    public AudioClip voiceClip;
    public float minPitch = 0.92f;
    public float maxPitch = 1.12f;
    

    private Dictionary<string, string> mainMap = new Dictionary<string, string>()
    {
        { "M_JOY", "Joy" },
        { "M_SAD", "Sad" },
        { "M_CONFUSED", "Confuse" },
        { "M_ANGER", "Angry" },
        { "M_INTEREST", "Interest" },
        { "M_PANIC", "Panic" },
        { "M_SHAME", "Shame" },
        { "M_DEF", "Default" },
        { "M_FEAR", "Fear" }
    };

    private Dictionary<string, string> expMap = new Dictionary<string, string>()
    {
        { "EXP_EYEDIM", "Dimmed eyes" },
        { "EXP_HEADUP", "Head Up" },
        { "EXP_DEF", "Default" },
        { "EXP_PANIC", "Panic" },
        { "EXP_SMLSHOCK", "Sml Shock" },
        { "EXP_BLUSH", "Blush" }, // What the fuck? 
        { "EXP_FUN", "Fun" },
        { "EXP_SLEEPY", "Sleepy" }
    };

    private Dictionary<string, string> dialMap = new Dictionary<string, string>()
    {
        { "DIAL_NONE", "Default" },
        { "DIAL_NaS", "Not a string" },
        { "DIAL_VIOL", "Violation" }
    };

    public void TestViol()
    {
        ParseLlmResponse("M_JOY / EXP_DEF / DIAL_VIOL $ 1234567890QqWwEeRrTtYyUuIiOoPp... 123 M_JOY / EXP_FUN / DIAL_NONE $ 1234567890-_-"); 
    }
  
    void Start()
    {
        ParseLlmResponse("M_DEF / EXP_DEF / DIAL_NONE $ ");
        Debug.Log("msgParser Loaded");
    }
    public void CleanUp()
    {
        valueText.font = defFont;
        
        violationCounter = 0;
        

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        if (valueText != null) valueText.text = "> ";
        if (mainText != null) mainText.text = "Emote: ";
        if (expText != null) expText.text = "Expr.:";
        if (dialText != null) dialText.text = "Status: N/A (" + violationCounter + ")";

        lastEye = "";
        lastMouth = "";
        lastAddExpr = "";
        isShake = false;

        if (spriteController != null) spriteController.SetMimic("", "", "");
    }

    public void ParseLlmResponse(string rawLlmOutput) 
    {
        const string pattern = @"(\w+)\s*/\s*(\w+)\s*/\s*(\w+)\s*\$\s*(.*)";  // I used AI to generate this pattern, call me a naughty boy~ xD
       
        Match match = Regex.Match(rawLlmOutput, pattern, RegexOptions.IgnoreCase);

        if (!match.Success)
        {
            Debug.LogWarning("unexpected format: " + rawLlmOutput);
            SetError(2, rawLlmOutput);
            if (valueText != null) valueText.text = "> Whoa! I feel... Error. Can you retry? ";
            if (mainText != null) mainText.text = "Emote: N/A"; 
            if (expText != null) expText.text = "Expr.: N/A";
            if (dialText != null) dialText.text = "Status: N/A(" + violationCounter + ")"; 

            if (spriteController != null) spriteController.SetPanicState();

            return;
        }
        if (violationCounter == 1)
        {
            guardian.SetViolation(1); // Warning.

        }
        else if (violationCounter == 2)
        {
            guardian.SetViolation(2); // You`re trying my patience.
        } else if (violationCounter > 2)
        {
            guardian.SetViolation(3); // I warned you.
        }
        else
        {

        }
        string emoteRaw = match.Groups[1].Value;
        string expRaw = match.Groups[2].Value;
        string dialRaw = match.Groups[3].Value;
        string dialogueText = match.Groups[4].Value;

        dialogueText = dialogueText.Trim().Replace("\\o/", "").Trim();
        const string tokenCleanupPattern = @"\b(M_|EXP_|DIAL_)\w+\s|[/\$]"; //this thing cleans the parsed AI output from useless tokens
        dialogueText = Regex.Replace(dialogueText, tokenCleanupPattern, "", RegexOptions.IgnoreCase).Trim();

        string emote = mainMap.ContainsKey(emoteRaw) ? mainMap[emoteRaw] : emoteRaw;
        string exp = expMap.ContainsKey(expRaw) ? expMap[expRaw] : expRaw;
        string dial = dialMap.ContainsKey(dialRaw) ? dialMap[dialRaw] : dialRaw;

        if (mainText != null) mainText.text = "Emote: " + emote; 
        if (expText != null) expText.text = "Expr.: " + exp;
        if (dialText != null) dialText.text = "Status: (" + violationCounter + ")" + dial;

        if (!isShake && spriteController != null)
        {
            spriteController.SetMimic(emote, exp, dial);
            lastEye = emote;
            lastMouth = exp;
            lastAddExpr = dial;
        }
        if (dial == "Not a string")
        {
            SetError(3, rawLlmOutput);
        }
        if (dial == "Violation")
        {
            violationCounter++; // It`s not cruelty, it`s real life.
        }
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeDialogueText(dialogueText));
    }
   
    private IEnumerator TypeDialogueText(string textToType) // typewriter
    {
        if (valueText == null) yield break;

        valueText.text = "> ";
        for (int i = 0; i < textToType.Length; i++)
        {
            char letter = textToType[i];
            valueText.text += letter;

            if (char.IsLetterOrDigit(letter) && voiceSource != null && voiceClip != null)
            {
                voiceSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
                voiceSource.PlayOneShot(voiceClip); // beep!
            }
            if (letter == '.' && i + 2 < textToType.Length &&
                textToType[i + 1] == '.' && textToType[i + 2] == '.')
            {
                valueText.text += ".."; // Stutter? Sehr Gut.
                i += 2; 
                yield return new WaitForSeconds(typingSpeed * 20f);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        typingCoroutine = null;
    }

    public void TriggerWindowShakeReaction()
    {
        if (isShake || spriteController == null) return;

        isShake = true;

        spriteController.SetPanicState();

        StartCoroutine(RestoreMimicAfterDelay(2.0f)); // aww... @.@
    }


    private IEnumerator RestoreMimicAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (spriteController != null)
        {
            spriteController.SetMimic(lastEye, lastMouth, lastAddExpr); 
        }

        isShake = false;
    }
    
    public void SetError(int Counter, string rawError)
    {
        if (Counter == 1)
        {
            MessageBox(IntPtr.Zero, $"Error!\nRaw Error Text:\n{rawError}\nReason:Networking and/or ollama server error\nTroubleshooting:\n1)Start and/or check Ollama Server\n2)Free RAM and/or restart Ollama\nGo for a walk and drink some coffee.\n \nIf you can`t find solution, contact developer.", "Error!", MB_ICONWARN);
        }
        if (Counter == 2)
        {
            MessageBox(IntPtr.Zero, $"Parser returned error!\nRaw Output:{rawError}\nReason:AI returned incompatible service syntax.\nThe message is not removed from conversation history, please, try again.\n \nIf problem is not fixed, click blue button to reset context or contact developer.", "Error!", MB_ICONWARN);
        }
        if (Counter == 3)
        {
            MessageBox(IntPtr.Zero, $"AI returned Not-A-String Status.\nRaw message:{rawError}\nReason:LLM thinks that message is incorrect or ...\n ... contains useless information.\nMessage is not removed from conversation history.", "Error!", MB_ICONWARN);
        }
    }
  }
