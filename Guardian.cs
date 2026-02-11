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


/*
 * This software utilizes Windows system libraries that do not require administrator privileges (UAC).
 * The software is provided “as is” without any warranties, express or implied. The author shall not be liable for any damages, data loss, or system instability arising directly or indirectly from the use of this software.
 * During execution, the game may access certain system components, including but not limited to:
 * Username and device name
 * Operating system information
 * GPU and CPU identifiers
 * System RAM size
 * Window handles (HWND) and cursor position
 * No personal information is collected, stored, or transmitted outside of the local system memory during gameplay.
 * Certain gameplay mechanics may intentionally manipulate system behavior, including closing or minimizing windows and controlling the cursor via the user32.dll library. These actions are part of the game design intended to enhance the intended horror experience.
 * Some gameplay features may interact with the Windows environment or cursor in unconventional ways. While these behaviors are intentional, they may affect other running applications.
 * By installing this software, you acknowledge and accept all associated risks.
*/

using System.Collections;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Diagnostics;
using UnityEngine.Diagnostics;



// Guided by the SIEGE`S GRACE,
// Seding you to HEAVEN`S GATES.
// When the FLAIL knoks your SKULL,
// See the FEAR OF GOD with rules.
// Soldiers dropping like flies,
// And someone is not dies.
// Don`t retry this domination,
// IT`S ETERNAL DAMNATION!

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
    public struct POINT
    {
        public int X;
        public int Y;
    }
    public float shakeDuration = 10f;
    public int shakeAmount = 10;
    public float shakeInterval = 0.05f;
    private const uint MB_ICONWARN = 0x00000030;
    public float CrashDelay = 10f;
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
        UnityEngine.Debug.LogWarning("Crash!");
        MessageBox(IntPtr.Zero, $"TheGP.exe encountered a fatal error.\n \nDebug Info:\nRAX=000034EAFC579CEF RBX=00007FF6A125EACC RCX=0000CCF23EBC87FF\nRDX=00004F8A93C17B2D RSI=000012E7A6D4C9B1 RDI=00008C31D5A7F2E4\nRIP=00007A9D14C3B8E6 RSP=00003D6F81A2C5B9 RBP=0000B4C8297DA1E3\nR8=000069A1C3E7D2F8  R9=00005E2C94B1A7D3 R10=0000C17D8A3F6B29\nR11=00002A7C5D91E4B8 R12=000091C4E2A7D63B r13=0000D3A7C1E58B24\nR14=000086B1D4C29A7F R15=0000F2C8A17D5E93\nCS=0x1B DS=0x2030 \nES=0x238F FS=0x309B\nGS=0xFEFF SS=0x2406 \nHardware info:\nCPU:{SystemInfo.processorType} \nGPU:{SystemInfo.graphicsDeviceName}\nSystemRAM:{SystemInfo.systemMemorySize}MB\nOS:{SystemInfo.operatingSystem}\nDeviceName:{SystemInfo.deviceName}\n \nEngine error info:\nParser.FindUserManners() could not be found\n\nEngine CallStack:\nSystem.NullReferenceException: Object reference not set to an instance of an object\n  at EmotionController.Blinker() in EmotionController.cs:154\n  at Guardian.SetViolation() in Guardian.cs:115\n  at UnityEngine.PlayerLoop.Update()\n  at UnityEngine.PlayerLoop.MainLoop()\n\n \nPress OK button to close application.", "Fatal Error", 0x10);
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

