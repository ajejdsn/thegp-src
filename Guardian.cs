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


// ---------============== ATTENTION ==============---------
//  This software uses Windows system libraries that do not require administrator privileges (UAC).

//  This software is provided 'as is' without warranties of any kind. The author is not ...
//  responsible for any damages, data loss, or system instability caused ...
//  directly or indirectly by the use of this software.
//
//
//  The game can read these components from your OS:
//  Username, Device name, OS, GPU name string, CPU name string ...
//  System RAM size, Window handles(HWND), cursor position.
//  No personal data is collected, stored, or transmitted outside the local memory during gameplay.
//
//  The game may intentionally close, crash, or move the cursor and minimize windows during gameplay using user32.dll library.
//  These behaviors are intentional game mechanics designed to enhance the horror experience."
//
//  Some gameplay features may interact with windows or the cursor in unusual ways. These behaviors are intentional but may affect other running applications.
// BY INSTALLING THIS GAME, YOU ACKNOWLEDGE AND ACCEPT ALL RISKS ASSOCIATED WITH IT.
// (Checkbox In Install wizard)


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;
using System.Diagnostics;
using UnityEngine.Diagnostics;
using TMPro;




public class Guardian : MonoBehaviour
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type); // I regret nothing.

    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")]
    private static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
    [DllImport("user32.dll")]
    private static extern bool IsZoomed(IntPtr hWnd); // MacOS? No. Linux? Absolutely not. BSD? Are you kidding me? ReactOS? May be.


    private const int SW_MINIMIZE = 6;
                                                
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    private static extern bool BlockInput(bool fBlockIt);

    public struct POINT
    {
        public int X;
        public int Y;
    }
    public float shakeDuration = 10f;
    public int shakeAmount = 10;
    public float shakeInterval = 0.05f;
    private float timer;
    private const uint MB_OK = 0x00000000;
    private const uint MB_ICONERROR = 0x00000010;
    private const uint MB_ICONWARN = 0x00000030;
    private const uint MB_ICONINFORMATION = 0x00000040;
    private const uint MB_ICONQUESTION = 0x00000020;
    public float CrashDelay = 10f;
    public AudioSource voice;
    public Sprite EyeEx;
    public Sprite MouthEx;
    public Sprite addexrEx;
    public Image eye;
    public Image addexp;
    public Image mouth;
    public TMP_Text valueText;



    void Start()
    {
        
    }
    public MsgParser msgParser;
    public void SetViolation(int counter) { 
    if (counter == 1)
        {
            MessageBox(IntPtr.Zero, $"Hey, {Environment.UserName}.\nStop it.\nAny your action have consequences.", "Guardian", MB_ICONWARN);
        } else if (counter == 2)
        {
            msgParser.valueText.font = msgParser.violFont;
            KillEm();
        } else if (counter >= 3)
        {
            MessageBox(IntPtr.Zero, $"{Environment.UserName}, I warned you.", "Guardian", MB_ICONWARN);
            StartCoroutine(CrashCoroutine(10)); //10
            
        }
    
    
    }

    private IEnumerator CrashCoroutine(float seconds)
    {
        eye.sprite = EyeEx;
        addexp.sprite = addexrEx; // nah
        mouth.sprite = MouthEx;
        // voice.Play();
        UnityEngine.Debug.Log("Dummkopf.");
        float timer = shakeDuration;

        while (timer > 0)
        {
            timer -= shakeInterval;

            if (GetCursorPos(out POINT pos))
            {
                int offsetX = UnityEngine.Random.Range(-shakeAmount, shakeAmount);
                int offsetY = UnityEngine.Random.Range(-shakeAmount, shakeAmount);
                SetCursorPos(pos.X + offsetX, pos.Y + offsetY);
            }

            yield return new WaitForSeconds(shakeInterval);

        }
        
        Crashout();
    }
    void Crashout()
    {
        UnityEngine.Debug.LogWarning("Crash");
        MessageBox(IntPtr.Zero, $"TheGP.exe encountered a fatal error.\n \nDebug info:\nCPUName:{SystemInfo.processorType} \nGPUName:{SystemInfo.graphicsDeviceName}\nRAM:{SystemInfo.systemMemorySize}MB\nOS:{SystemInfo.operatingSystem}\nDeviceName:{SystemInfo.deviceName}\n \nError info:\nParser.FindUserManners() could not be found\n\nCall Stack:\nSystem.NullReferenceException: Object reference not set to an instance of an object\n  at EmotionController.Blinker() in EmotionController.cs:154\n  at Guardian.SetViolation() in Guardian.cs:115\n  at UnityEngine.PlayerLoop.Update()\n  at UnityEngine.PlayerLoop.MainLoop()\n \nUnityCrashHandler will open now.\n \nPress OK button to close application.", "Crash!", 0x10);
        Utils.ForceCrash(ForcedCrashCategory.AccessViolation);
    }

    void KillEm()
        {
            IntPtr gameWindow = Process.GetCurrentProcess().MainWindowHandle;
            EnumWindows((hWnd, lParam) =>
            {
                GetWindowThreadProcessId(hWnd, out int processId);


                if (hWnd != gameWindow && IsZoomed(hWnd))
                {
                    ShowWindow(hWnd, SW_MINIMIZE);
                }
                return true;
            }, IntPtr.Zero);
        }
    }


