# thegp-src

<h2>TheGen Project original Source code.</h2>
More info: http://ajejdsn.itch.io/thegp
<h3> ---------============== ATTENTION ==============---------</h3>
This code uses Windows system libraries that do not require administrator privileges (UAC).
This code is provided 'as is' without warranties of any kind. The author is not
responsible for any damages, data loss, or system instability caused
directly or indirectly by the use of this software.

Some parts of code can read these components from your OS:
Username, Device name, OS, GPU name string, CPU name string 
System RAM size, Window handles(HWND), cursor position.
No personal data is collected, stored, or transmitted outside the local memory during gameplay.

The app with this code may intentionally close, crash, or move the cursor and minimize windows during gameplay using user32.dll library.
Some features may interact with Windows or the cursor in unusual ways. These behaviors are intentional but may affect other running applications.

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
