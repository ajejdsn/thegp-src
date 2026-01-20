
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


using TMPro;
using UnityEngine;

public class NoteLoader : MonoBehaviour
{
   public TMP_Text noteText;

    private string[] phrases = new string[]
    {
            "Note:Touching his eyes is not best idea.",
            "Note:You can reset him with blue button.",
            "Note:He likes the blue color and cats.",
            "Note:He may forget something, and that's okay.",
            "Note:He can stutter when confused and/or feels shy.",
            "Note:He feels everything in your system, even window shaking.",
            "Note:He has strict violation system, don`t try to do something bad",
            "Note:Updates (almost)every week!",
            "Note:Made with love and circuits.",
            "Note:i was out of ideas -_-",
            "Note:i like his voice",
            "Note:Under construction.",
            "Note:yablade is coming for u", // now it`s shizoblaze owo
            "Note: don`t make him hurt.",
            "Note:do not add ketchup to the poutine",
            "Note:run if u see pancakes xD",
            "Note:Cake is not a lie.",
            "Note:nya!",
            "Note:I regret nothing.",
            "Note:Error 404",
            "Note:hamburger",
            "Note:It`s difficult",
            "Note:Hello, world!",
            "Note:Works at English, Russian and Ukrainian languages.",
            "Note:He is a bit silly :P"
    };
        
    void Start()
    {
       if (noteText != null && phrases.Length > 0)
        {
            int rIndex = Random.Range(0, phrases.Length);
            noteText.text = phrases[rIndex];
        } else
        {
            Debug.LogWarning("Error"); // What`s the error? I don`t know. This error is a racist. Don`t be like this error because it`s in prison now.
        }
    }

  
  
}
