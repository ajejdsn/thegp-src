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
using UnityEngine.SceneManagement;
public class ScenePlay : MonoBehaviour
{
    public FadeManager fadeMgr;
    public void LoadWScene()
    {
        fadeMgr.fadeX();
        SceneManager.LoadScene("WorkSc"); // You`re curious, aren`t you?~
        Debug.Log("WorkSc");             
    }
    public void QuitApp()
    {
        fadeMgr.fadeX();
        Application.Quit();
        Debug.Log("Quit"); // it`s sad
    }
    public void AboutSc()
    {
        fadeMgr.fadeX();
        SceneManager.LoadScene("AboutScene");
        Debug.Log("About");
    }
    public void TutorSc()
    {
        fadeMgr.fadeX();
        
        SceneManager.LoadScene("TutorSc"); 
        Debug.Log("Tutor");
    }
    public void ReturnSc()
    {
        SceneManager.LoadScene("MainMenu");
        fadeMgr.fadeX();
    }
    public void ParamSc()
    {
        fadeMgr.fadeX();
        SceneManager.LoadScene("ParamSc");
        Debug.Log("Parameters");
    }
    public void intMode()
    {
        SceneManager.LoadScene("IntMode"); // is it even works?
    }
    public void loadBrowser()
    {
        Application.OpenURL("http://ajejdsn.itch.io");
    }
}


