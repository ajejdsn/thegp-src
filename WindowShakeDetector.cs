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
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
public class WindowShakeDetector : MonoBehaviour
{
    [Header("Parser")]
    public MsgParser parser;
    [Header("Shake")]
    public float shakeThreshold = 5f;      
    public float requiredDuration = 0.1f;  
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    private IntPtr gHwnd = IntPtr.Zero;
    private Vector2 lastPosition;
    private float shakeDuration;

    void Start()
    {
        gHwnd = GetGWindow();
        lastPosition = Vector2.zero; 
    }

    void Update()
    {
        if (parser == null || gHwnd == IntPtr.Zero)
            return;
        if (!GetWindowRect(gHwnd, out RECT rect))
            return;
        Vector2 currentPosition = new Vector2(rect.left, rect.top);
        if (lastPosition == Vector2.zero)
        {
            lastPosition = currentPosition;
            return;
        }
        float displacement = Vector2.Distance(currentPosition, lastPosition);
        if (displacement > shakeThreshold)
        {
            shakeDuration += Time.deltaTime;
            if (shakeDuration >= requiredDuration)
            {
                parser.TriggerWindowShakeReaction();
                shakeDuration = 0f;
            }
        }
        else
        {
            shakeDuration = 0f;
        }
        lastPosition = currentPosition;
    }
    private IntPtr GetGWindow()
    {
        IntPtr found = IntPtr.Zero;
        uint currentProcessId = (uint)Process.GetCurrentProcess().Id;

        EnumWindows((hWnd, lParam) =>
        {
            GetWindowThreadProcessId(hWnd, out uint processId);

            if (processId == currentProcessId)
            {
                found = hWnd;
                return false; 
            }
            return true;
        }, IntPtr.Zero);
        return found;
    }
}
