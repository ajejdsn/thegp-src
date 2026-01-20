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
using System.Runtime.InteropServices;
using System;

public class WindowShakeDetector : MonoBehaviour
{
    [Header("Msg Parser")]
    public MsgParser parser;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [DllImport("user32.dll")] // I regret nothing.
    private static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect); 

    private Vector2 lastPosition;
    private float shakeThreshold = 0.05f;
    private float shakeDuration = 0f;
    private float requiredDuration = 0.1f;

    void Start()
    {
        lastPosition = Vector2.zero; // aaahhh~
    }

    void Update()
    {
        if (parser == null) return;

        IntPtr hWnd = GetForegroundWindow(); // works with any window xD
        RECT rect;

        if (GetWindowRect(hWnd, out rect))
        {
            Vector2 currentPosition = new Vector2(rect.left, rect.top);

            if (lastPosition == Vector2.zero)
            {
                lastPosition = currentPosition;
                return;
            }

            float displacement = Vector2.Distance(currentPosition, lastPosition); //how tf it works?

            if (displacement > shakeThreshold)
            {
                shakeDuration += Time.deltaTime;
            }
            else
            {
                shakeDuration = 0f;
            }

            if (shakeDuration > requiredDuration)
            {
                parser.TriggerWindowShakeReaction(); // he`s adorable >////<
                shakeDuration = 0f;
            }

            lastPosition = currentPosition; 
        }
    } 
}

