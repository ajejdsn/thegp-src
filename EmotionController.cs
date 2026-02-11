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


// v1.5.9

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmotionController : MonoBehaviour
{
    [Header("Mimic Renderer")]
    public Image eyeRender;
    public Image mouthRender;
    public Image addExprRender;
    public Image fmaskRender;
    [Header("Eyes")] // fuck
    public Sprite eyeJoy;
    public Sprite eyeSad;
    public Sprite eyeConfused;
    public Sprite eyeShame;
    public Sprite eyeDim;
    public Sprite eyeFear;
    public Sprite eyePanic;
    public Sprite eyeDef;
    public Sprite eyeAnger;
    public Sprite eyeBlink1;
    public Sprite eyeBlink2;
    [Header("Mouth")] // aww hell nah
    public Sprite mouthJoy;
    public Sprite mouthDef;
    public Sprite mouthFun;
    public Sprite mouthSad;
    public Sprite mouthBlush;
    public Sprite mouthFear;
    public Sprite mouthPanic;
    public Sprite mouthSmlShock;
    public Sprite mouthHeadUp;
    public Sprite mouthShame; 
    public Sprite mouthAnger; 
    [Header("Additional Expression")]
    public Sprite addExprNone;
    public Sprite addExprBlush;
    //addexpr violation
    public Sprite addExprViolation;
    public Sprite addExprViolation2;
    //addexpr sweat
    public Sprite addExprSweat1;
    public Sprite addExprSweat2;
    public Sprite addExprSweat3;

    [Header("Facial Mask")]
    public Sprite Glitch01; // not used in demo
    public Sprite Glitch02;
    public Sprite Glitch03;
    public Sprite Glitch04;

    public Sprite oldEye;
    private bool isBlink = false;
    private bool isGlitch = false;
    // :3

    public void SetMimic(string emote, string exp, string dial)
    {
        
        Sprite newEyeSprite = GetEyesSprite(emote, exp); // eyes
        if (eyeRender != null && newEyeSprite != null)
        {
            eyeRender.sprite = newEyeSprite;
        }

        Sprite newMouthSprite = GetMouthSprite(emote, exp); // mouth 
        if (mouthRender != null && newMouthSprite != null)
        {
            mouthRender.sprite = newMouthSprite;
        }

        Sprite newAddExprSprite = GetAddExprSprite(exp, dial); // additional expression
        if (addExprRender != null)
        {
            addExprRender.sprite = newAddExprSprite;
        }
    }

    public void SetPanicState()  // panik!!
    {
        if (eyeRender != null) eyeRender.sprite = eyeConfused;
        if (mouthRender != null) mouthRender.sprite = mouthBlush;
        if (addExprRender != null) addExprRender.sprite = addExprNone;
    }

    private Sprite GetEyesSprite(string emote, string exp)
    {
        if (exp == "Dimmed eyes" || exp == "Sleepy") return eyeDim;

        switch (emote) // why am i not using enum? why not?
        {
            case "Anger": return eyeAnger;
            case "Joy": return eyeJoy;
            case "Sad": return eyeSad;
            case "Confuse": return eyeConfused;
            case "Shame": return eyeShame;
            case "Fear": return eyeFear;
            case "Panic": return eyePanic;
            default: return eyeDef; 
        }
    }

    private Sprite GetMouthSprite(string emote, string exp)
    {

        switch (emote)
        {
            case "Anger": return mouthAnger;
            case "Joy": return mouthJoy;
            case "Sad": return mouthSad;
            case "Confuse": return mouthPanic;
            case "Shame": return mouthShame;
            case "Fear": return mouthFear;
            case "Panic": return mouthPanic;
        }

        switch (exp)  
        {
   
            case "Head Up": return mouthHeadUp;
            case "Fun": return mouthFun;
            case "Shame": return mouthShame;
            case "Fear": return mouthFear;
            case "Blush": return mouthBlush;
            case "Panic": return mouthPanic;
            case "Sad": return mouthSad;
            case "Sml Shock": return mouthSmlShock;
            default: return mouthDef;
        }
       
    }



    private Sprite GetAddExprSprite(string exp, string dial)
    {

        if (dial == "Violation") return addExprViolation;
        if (exp == "Blush") return addExprBlush;

        return addExprNone;
    }

    // @.@

    IEnumerator Blinker()
    {

        if (isBlink) yield break;
        isBlink = true;
        oldEye = eyeRender.sprite;
        eyeRender.sprite = eyeBlink1;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.15f));
        eyeRender.sprite = eyeBlink2;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.15f));
        eyeRender.sprite = oldEye;
        yield return new WaitForSeconds(UnityEngine.Random.Range(7f, 15f));
        isBlink = false;
    }


    IEnumerator Glitcher()
    {
        if (isGlitch) yield break; 
        isGlitch = true;
        fmaskRender.sprite = Glitch01;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.15f));
        fmaskRender.sprite = Glitch02;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.15f));
        fmaskRender.sprite = Glitch04;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.15f));
        fmaskRender.sprite = Glitch03;
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, 0.15f));
        fmaskRender.sprite = Glitch01;
        yield return new WaitForSeconds(UnityEngine.Random.Range(15f, 35f));
        isGlitch = false;
    }
    public void GlitchTest()
    {
        StartCoroutine(Glitcher());
    }
   

    void Update()
    {
        if (!isBlink && eyeRender.sprite != eyeShame && eyeRender.sprite != eyeJoy && eyeRender.sprite != eyeConfused)
        {
            StartCoroutine(Blinker());  
        }                            
        if (!isGlitch)
        {
            StartCoroutine(Glitcher());
        }
       
    }

}
