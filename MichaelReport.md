## User Interface - Michael Bai - github: Mbai2000
### Menus
Several menus were implemented for our game. All menus are contained within their own scenes, with the exception of the Pause Menu, which is able to pause any gameplay(non-menu) scene. Each menu scene is loaded through Unity's Scene Manager.
**Start/Main Menu** 
 The start menu has three buttons: [Start](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/MainMenu.cs#L9), [How To Play](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/MainMenu.cs#L20), and [Quit](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/MainMenu.cs#L15). The Start and How To Play buttons use Unity's scene manager to load their respective scenes. The Start menu loads the scene containing the Introductory Cutscene, while the How To Play button loads the scene containing the instructions. The Quit button exits the application. 
 
**Controls/How To Play Menu** 
This menu features only two elements, a [BACK](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/InstructionMenu.cs#L8) button, and a text element. The text element provides a very brief description of the game and how it is played, as well as the controls. The [BACK](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/InstructionMenu.cs#L8) button returns the player to the previous screen (Start/Main Menu).

**Game Over** 
This menu is loaded upon the player reaching one of the game over conditions. It is very similar to the Start Menu, featuring three buttons; [Restart](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/QuitMenu.cs#L10), [Quit](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/QuitMenu.cs#L19), and [Credits](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/QuitMenu.cs#L26). The Restart button returns the player to the Start/Main Menu, Quit exits the application, and Credits loads the scene containing the Credits menu. 

**Credits Menu**
This menu contains three elements, a text element, and two buttons. The [Restart](https://github.com/rodsters/CrunchTime/blob/0450fd3b6b2aedde3f93ec77417aaf5ad466f61a/CrunchTime/Assets/Scripts/CreditsMenu.cs#L10) and [Quit](https://github.com/rodsters/CrunchTime/blob/0450fd3b6b2aedde3f93ec77417aaf5ad466f61a/CrunchTime/Assets/Scripts/CreditsMenu.cs#L19) buttons function exactly the same as the Restart and Quit from the Game Over menu. The text element contains the names of all the group members and their respective roles. 

**References for all menus above**
The initial menu (Start/Main Menu), was created/designed with video reference. All other menus were adapted from the original menu.
[Menu reference](https://www.youtube.com/watch?v=zc8ac_qUXQY&t=563s)

**Pause Menu**
Upon pressing the Escape (ESC) key in any gameplay scene, the pause menu will appear. The pause menu is a transparent panel that appears on top of the current scene when ESC is pressed, with two buttons, Resume and Main Menu. Hitting ESC will also close the pause menu. 

The pause menu functions through Update checking whether or not the game is paused through a boolean variable [GamePaused](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/PauseMenu.cs#L13). When [Paused](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/PauseMenu.cs#L34), the game will set the pause menu panel to be active. The time scale of the scene will be set to 0, causing the scene elements to become paused. GamePaused is also updated to be true. When [Resume](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/PauseMenu.cs#L43) is called, the reverse occurs, where the pauseMenu is set to false, the time scale is reverted to 1, and GamePaused is updated. 

The Resume button functions exactly the same as pressing ESC again, where the pause menu will be hidden, and all values reset to their original states.

The [Main Menu](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/PauseMenu.cs#L55) button resets the time scale of the application, and then will load the Start/Main Menu scene again.

Two video references were used when creating the Pause menu:
[Pause Reference 1](https://www.youtube.com/watch?v=JivuXdrIHK0&t=1s)
[Pause Reference 2](https://www.youtube.com/watch?v=tfzwyNS1LUY)

### Other UI Elements
There were multiple non-menu UI elements we needed. Unlike the various menus which were contained within their own scenes, these UI elements are present in the gameplay scenes, displayed on the player's HUD.
**Timer**
A core mechanic of our game is the usage of time. 
