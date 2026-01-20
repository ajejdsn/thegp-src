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
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class EventManager : MonoBehaviour
{
    [Header("UI")]
    public Image pcImage;
    [Header("Sprites")]
    public Sprite defSprite; // char1
    public Sprite winterSprite; // char_event1
    public Sprite loveSprite; // char_event2
    public Sprite autumnSprite; //  char_event3
    public Sprite birthdaySprite; // char_event4
    // made with love and circuits
    void Start()
    {
        Debug.Log("START");
        System.DateTime today = System.DateTime.Now;

        DateTime winterDate = new DateTime(today.Year, 12, 20);
        DateTime winterEnd = new DateTime(today.Year, 1, 5);
        DateTime autumnDate = new DateTime(today.Year, 10, 31);
        DateTime autumnEnd = new DateTime(today.Year, 11, 2); // why is it still here? 

        if (today.Month == 12 && today.Day >= 20)
        {
            pcImage.sprite = winterSprite; // winter!! ^w^
            Debug.Log("winter");
        } else if (today.Month == 11 && today.Day <= 10)
        {
            pcImage.sprite = autumnSprite;
            Debug.Log("autumn");
        } else if (today.Day == 21 && today.Month == 6)
        {
            pcImage.sprite = birthdaySprite; // 21.06 ^-^
        } else if (today.Day == 14 && today.Month == 2)
        {
            pcImage.sprite = loveSprite; // <3
        } else
        {
            pcImage.sprite = defSprite;
        }


        // ngl he is so cute
    }
}
