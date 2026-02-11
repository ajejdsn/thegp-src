# thegp-src

<h2>TheGen Project original Source code.</h2>
More info: http://ajejdsn.itch.io/thegp
<h3> ---------============== ATTENTION ==============---------</h3>
  This software utilizes Windows system libraries that do not require administrator privileges (UAC).
  The software is provided “as is” without any warranties, express or implied. The author shall not be liable for any damages, data loss, or system instability arising directly or indirectly from the use of this software.
  During execution, the game may access certain system components, including but not limited to:
  Username and device name
  Operating system information
  GPU and CPU identifiers
  System RAM size
  Window handles (HWND) and cursor position
  No personal information is collected, stored, or transmitted outside of the local system memory during gameplay.
  Certain gameplay mechanics may intentionally manipulate system behavior, including closing or minimizing windows and controlling the cursor via the user32.dll library. These actions are part of the game design intended to enhance the intended horror experience.
  Some gameplay features may interact with the Windows environment or cursor in unconventional ways. While these behaviors are intentional, they may affect other running applications.
  By installing this software, you acknowledge and accept all associated risks.

This product requires the installation of third-party software (Google Gemma3 and Ollama). There are not included, and by installing them you agree to their respective license terms and privacy policies. (https://ollama.org/terms and https://ai.google.dev/gemma/terms for more info)

***
It contains:
- ``AudioManager``
- ``EmotionController``
- ``EventManager``
- ``FadeManager``
- ``Guardian``
- ``MsgParser``
- ``NoteLoader``
- ``ParamUI``
- ``ScenePlay``
- ``SettingsManager``
- ``SoundFXPlay``
- ``UserDeParse``
- ``WindowShakeDetector``

***

The main components:
- *MsgParser* - Parses LLM Message and partially controls violation system.
- *UserDeParse* - Builds, sends and receives LLM Message.
- *EmotionController* - Applies emotions to character`s face.
- *Guardian* - Violation system.
