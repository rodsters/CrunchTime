# Game Basic Information #

## Summary ##

A knight finds himself trapped in ancient ruins filled with monsters! To make things worse, vengeful spirits have placed a curse on him for disturbing their rest. Time is quickly running out! He needs to escape before the curse takes hold and he is overrun by the monsters or the curse claims him. 

## Gameplay Explanation ##

Move around with WASD

Left click to shoot/interact

Right click to dash

Escape/ESC to pause

Time is your most important resource. It can be gained through defeating enemies, and used to purchase upgrades. As you progress through the game, time will also be continually counting down, and can go into the negative. When time reaches 0, the game will become more difficult. If time reaches -3 minutes before you reach the end, the game is over. Reach the end of the dungeon before you run out of time.

Defeat all the enemies in a room to open the door and progress through the dungeon. Upgrades can be purchased by interacting with the shop, represented by a hourglass, to help you. Upon reaching the end, interact with the exit to escape.

## Main Roles

## User Interface - Michael Bai - github: Mbai2000

### Menus

Several menus were implemented for our game. All menus are contained within their own scenes, with the exception of the Pause Menu and Shop Menu, which are panels that appear within gameplay scenes. Each menu scene is loaded through Unity's Scene Manager.

#### Start/Main Menu

The start menu has three buttons: [Start](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/MainMenu.cs#L9), [How To Play](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/MainMenu.cs#L20), and [Quit](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/MainMenu.cs#L15). The Start and How To Play buttons use Unity's scene manager to load their respective scenes. The Start menu loads the scene containing the Introductory Cutscene, while the How To Play button loads the scene containing the instructions. The Quit button exits the application. 
 
#### Controls/How To Play Menu 

This menu features only two elements, a [BACK](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/InstructionMenu.cs#L8) button, and a text element. The text element provides a very brief description of the game and how it is played, as well as the controls. The [BACK](https://github.com/rodsters/CrunchTime/blob/dacf0b907f6cb09a92b36b1f767469e3d1e9c2cf/CrunchTime/Assets/Scripts/InstructionMenu.cs#L8) button returns the player to the previous screen (Start/Main Menu).

#### Game Over

This menu is loaded upon the player reaching one of the game over conditions. It is very similar to the Start Menu, featuring three buttons; [Restart](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/QuitMenu.cs#L10), [Quit](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/QuitMenu.cs#L19), and [Credits](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/QuitMenu.cs#L26). The Restart button returns the player to the Start/Main Menu, Quit exits the application, and Credits loads the scene containing the Credits menu.

#### Victory Menu

This menu is loaded upon the player reaching a victory condition. This menu is identical to the Game Over screen, with the exception of the text element. The text element displays "You Escaped!" to represet victory. All three buttons [Restart](https://github.com/rodsters/CrunchTime/blob/9cb516ddd54eadeb1d52754969635e1b00ca74bd/CrunchTime/Assets/Scripts/VictoryMenu.cs#L10), [Quit](https://github.com/rodsters/CrunchTime/blob/9cb516ddd54eadeb1d52754969635e1b00ca74bd/CrunchTime/Assets/Scripts/VictoryMenu.cs#L19), and [Credits](https://github.com/rodsters/CrunchTime/blob/9cb516ddd54eadeb1d52754969635e1b00ca74bd/CrunchTime/Assets/Scripts/VictoryMenu.cs#L26) function the same. This menu is loaded when the player reaches the end of the ruins, and clicks the exit. 

#### Credits Menu

This menu contains three elements, a text element, and two buttons. The [Restart](https://github.com/rodsters/CrunchTime/blob/0450fd3b6b2aedde3f93ec77417aaf5ad466f61a/CrunchTime/Assets/Scripts/CreditsMenu.cs#L10) and [Quit](https://github.com/rodsters/CrunchTime/blob/0450fd3b6b2aedde3f93ec77417aaf5ad466f61a/CrunchTime/Assets/Scripts/CreditsMenu.cs#L19) buttons function exactly the same as the Restart and Quit from the Game Over menu. The text element contains the names of all the group members and their respective roles. 

#### References for all menus above

The initial menu (Start/Main Menu), was created/designed with video reference. All other menus were adapted from the original menu.

[Menu reference](https://youtu.be/zc8ac_qUXQY)

#### Pause Menu

Upon pressing the Escape (ESC) key in any gameplay scene, the pause menu will appear. The pause menu is a transparent panel that appears on top of the current scene when ESC is pressed, with two buttons, Resume and Main Menu. Hitting ESC will also close the pause menu. 

The pause menu functions through Update checking whether or not the game is paused through a boolean variable [GamePaused](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/PauseMenu.cs#L13). When [Paused](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/PauseMenu.cs#L34), the game will set the pause menu panel to be active. The time scale of the scene will be set to 0, causing the scene elements to become paused. GamePaused is also updated to be true. When [Resume](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/PauseMenu.cs#L43) is called, the reverse occurs, where the pauseMenu is set to false, the time scale is reverted to 1, and GamePaused is updated. 

The Resume button functions exactly the same as pressing ESC again, where the pause menu will be hidden, and all values reset to their original states.

The [Main Menu](https://github.com/rodsters/CrunchTime/blob/22125e986ef727c82f3ffdd6d10fe9c7e6d47193/CrunchTime/Assets/Scripts/PauseMenu.cs#L55) button resets the time scale of the application, and then will load the Start/Main Menu scene again.

Two video references were used when creating the Pause menu:

[Pause Reference 1](https://youtu.be/JivuXdrIHK0)

[Pause Reference 2](https://youtu.be/tfzwyNS1LUY)

#### Shop

The shop is composed of two separate game objects, one that allows the player to open the shop, and one that is the shop UI. The first object is a transparent button overlaid on top of the shop sprite. When the shop sprite is clicked, the shop UI panel will be set to active. The time scale of the game is set to 0 upon opening the shop. This is to prevent the player from being overrun while shopping.

The shop UI is similar to the pause menu, in that it utilizes a panel with multiple buttons. The Back button hides the shop panel by setting it to false. For each upgrade offered, there is a text element describing the upgrade, as well as a button allowing the purchase of each upgrade, with its associated cost in seconds. These upgrade buttons will decrease the time remaining on the timer by calling a setter function (mentioned below).

Two videos were also referenced when creating the shop UI.

[Button to open Panel](https://youtu.be/LziIlLB2Kt4)

[Shop UI design](https://youtu.be/EEtOt0Jf7PQ)

### Other UI Elements

#### HUD

There were multiple non-menu UI elements we needed. Unlike the various menus which were contained within their own scenes, these UI elements are present in the gameplay scenes, displayed on the player's HUD.

#### Timer

A core mechanic of our game is the usage of time. As such, a timer needed to be implemented for the player to be able to track their remaining time. The timer is a [text object](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/Timer.cs#L19) that will continually decrease from the initial [total time](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/Timer.cs#L12) every second. The player's [current time](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/Timer.cs#L11) remaining is tracked in a variable. This text is displayed in the format [MM:SS](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/Timer.cs#L49). Upon reaching a time of 0, the game will increase in difficulty. Upon reaching a time of -3 minutes, the game will end. 

Time also functions as a currency in this game. As such, [get](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/Timer.cs#L35) and [set](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/Timer.cs#L30) functions were created, allowing other scripts to modify the player's current remaining time. This is used in the Shop (described below), as well as in gameplay elements such as killing enemies increasing the player's current time. The timer will update when actions that would change the time occur.

A video was referenced to properly update the text element with the correct time.

[Timer Text Reference](https://youtu.be/o0j7PdU88a4)

#### Health Bar

The health bar for this game is composed of two separate images. The first is simply the background for the health bar. The [second image](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/HealthBar.cs#L10https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/HealthBar.cs#L10) is an image of type "filled" that will change its fill amount horizontally based on the player's health. 

The health bar updates by accessing values from the [Player Controller](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/HealthBar.cs#L13). Through get functions provided within the Player Controller, the health bar accesses the player's [current health](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/HealthBar.cs#L25), and the player's [maximum health](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/HealthBar.cs#L26). The fill amount of the health bar image is then set to [currentHealth/maxHealth](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/HealthBar.cs#L27). The health bar will constantly update based on the player's health regeneration, and immediately decrease upon taking damage.

The health bar also checks whether or not the player has [died](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/HealthBar.cs#L28). Upon reaching 0.0 HP, the game will load the GameOver scene ([Death](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/HealthBar.cs#L35)).

A video reference was used for the health bar.

[Health Bar Reference](https://youtu.be/NE5cAlCRgzo)

#### Dash Cooldown

The player is able to dash on a cooldown determined by the length of the dash. As such, a visual element is required to allow the player to know when they are able to dash. The dash icon is similar to the health bar in that it is composed of two images. The first image is the default icon, when the ability is available. The [second image](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/DashCooldown.cs#L8) is a darkened version of the icon of type filled that represents the ability on cooldown. This darkened image is initially set to a fill of 0, completely hiding it. This causes the player to only see the first image.

The dash icon updates by accessing values from the [Player Controller](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/DashCooldown.cs#L12). The [cooldown](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/DashCooldown.cs#L32) of the dash is accessed, as well as a boolean value called [isDashing](https://github.com/rodsters/CrunchTime/blob/44a2ee9c9fcf48b233ec80d5e13348767a0c2c1f/CrunchTime/Assets/Scripts/DashCooldown.cs#L33) that states whether or not the player is dashing. If the player is dashing, the fill of the darkened image is set to 1 (full). A separate boolean value onCooldown is also set to true. While the dash is on cooldown, the fill of the darkened image is decreased, revealing the normal icon below. Once the fill amount reaches 0, onCooldown is reset to false.

A video was referenced to create the visual effect tracking the dash's cooldown.

[Dash Cooldown Reference](https://youtu.be/wtrkrsJfz_4)

#### Minimap

A minimap was also created to allow the player to better track their position. The minimap is the only UI object without a script attached to it. The minimap is a raw image linked to a camera (seperate from the main camera) through a rendered texture. The minimap camera is attached to the player, keeping the player centered in the minimap while the player moves. As our game is 2D, the minimap camera is set to a negative z position, in order to display the proper image.

A video was referenced to create the minimap.

[Minimap Reference](https://youtu.be/28JTTXqMvOU)

#### Enemy Health Bars
As our game has enemies that need to be defeated, enemy health bars were created to provide a visual indicator for the player that damage is being dealt and show their progress towards defeating each enemy. Unlike the player health bar, the enemy health bars are attached to the enemies themselves so that they will follow the enemies as they move. Additionally, the enemy health bars are slider components, rather than images like the player health bar. However, these sliders function much like the player health bar image.

The enemy health bar controller script only has two functions. Update simply moves the health bar with the enemy's location, at an offset 'y' value so that it appears above the enemy, rather than within. The other function, [SetHealth](https://github.com/rodsters/CrunchTime/blob/701319b196fb3181710df6ce3694fd6b67ba5f57/CrunchTime/Assets/Scripts/EnemyHealthBar.cs#L15), adjusts the slider. An enemy's health bar only appears upon the enemy taking damage. The slider's current value is then updated based on the enemies current health and max health is set based on their max health.

In order to properly set the slider's values, [EnemyController](https://github.com/rodsters/CrunchTime/blob/701319b196fb3181710df6ce3694fd6b67ba5f57/CrunchTime/Assets/Scripts/EnemyController.cs#L94) needed to be modified. Upon an enemy being created, SetHealth is called within Start of EnemyController with the proper values. When the enemy takes damage, within the ChangeEnemyHealth function, the SetHealth function is again called to update the current health in the health bar to that of the enemy. 

A video was referenced to create the enemy health bars.

[Enemy Health Bar Reference](https://youtu.be/v1UGTTeQzbo)

## Producer - Rodney Johnston

In order for our game to operate smoothly, we needed to make sure that the producer was active and communicative to make sure that all parts of the game worked seamlessly together without massive merge errors in github. Furthermore, the producer needed to organize the workload for the other group members. I made sure to organize tasks for everyone, while being flexible in order for our game to be able to be made smoother and finished on time.

Github:

To start the project, I made a github, and invited all members to it. Then, I organized the github where we would make branches based on what each person was working on. To signify this, I did my best to enforce the naming convention of: firstname_whattheyworked on. Furthermore, to help prevent merge errors, I would make the others work in their own scenes, and we would work together to merge all the changes in the main scene. Lastly, I was the main one in charge of going through pull requests and making sure all the code was working and dealt with merge errors. Furthermore, I made folders in Unity that helped organize all different assets of the project, like scripts and audio. However, in Unity, if your folders are empty, github doesn’t recognize them. So, I made initial text documents where people could save links to where they got code.

Meetings:

In order to increase efficiency and communication, I created a discord server for our Group, where we had multiple channels that helped outline links, roles, and goals. Furthermore, we used that discord server to have a large number of meetings throughout the weeks. In order to organize our meetings, I set up a whentomeet.com up as seen here: https://www.when2meet.com/?15413933-Z7Xs4 where we were able to find that everyone was free on the weekends. Then, we would have weekly meetings on the weekends, either on Saturday or Sunday. Furthermore, I organized more meetings when the deadline was approaching.

Helping Other Roles:

In order to help people get used to unity: I made/reused a few basic assets and scripts to help get the project started. I added Rainbowman as a starting asset for the player, a basic set of tile maps, created a basic map with walls and a floor, and a basic camera controller that I repurposed from project 2: https://github.com/rodsters/CrunchTime/blob/main/CrunchTime/Assets/Scripts/BasicPositionLockCamera.cs 
Source (I’m not entirely sure how to put project 2 as a source, but here is the link from my project 2) :https://github.com/ensemble-ai/exercise-2-camera-control-rodsters/blob/master/Obscura/Assets/Scripts/PositionLockCameraController.cs

Then, as time progressed on, I helped Amaan and Trevor learn how to use the tile map for their roles, and helped Amaan with some suggestions for the map design. Namely, I talked to him about the map size and door system. 

Furthermore, when Trevor was a bit behind working on enemy Ai, I helped lessen his burden by asking the group who was able to help work on the enemies and was able to delegate what they needed to do. Then, we were able to receive help with Jasper adding some more variants to enemies.


Work Delegation: 

While working on CrunchTime, we wanted to have an efficient workflow. To start the project, I initially planned to use Trello to solve this issue. However, with how well we found the initial game plan to be documented, we mainly delegated tasks from that and updated our tasks in the general meetings we had. I was able to make sure that everyone had some sort of task to do and worked on them.

Final Pitch:

Made a presentation for the final pitch using Canva.com, where I made a sort of canvas to speak over. After going through the presentation, we would show the trailer made by Trevor.

Presentation found here:
https://github.com/rodsters/CrunchTime/blob/main/Crunchtime%20Presentation.pdf

## Movement/Physics - James Jasper Fadden O’Roarke

For our game, we intended a very fast paced gameplay to pair with the fact that the less time you take, the more currency you have. However, in early testing we realized that our generic “if player pressed d, set velocity to 1” physics code that consists of 10 lines would not just not cut it. Not only that, but the physics had to be high quality for other areas, such as shooting/aiming, as well as even enemy movement, or enemy shooting. Physics was a surprisingly encompassing part of our game, so I had quite a bit of work to do.

ADSR Movement:

In our original project draft, I mentioned hoping to create an especially complex movement system with momentum and direction. While I did not create something quite as complex as I originally envisioned, partially due to the fact that I had to aid my team in many other aspects of the project, I did create an extremely detailed ADSR movement system, based on code that the teacher wrote for a lecture. While just taking the teacher’s code makes the job sound easy, this was a huge challenge considering that our game took place in two directions (compared to our teacher's single directional left/right example). I ended up writing somewhere around 200 lines of code in the PlayerController.cs script to properly add directional momentum and ADSR stages for both left and right movement. This paid off nonetheless, as our final movement system is easily tunable and feels wonderfully smooth with WASD controls. Currently, we emulate the square root based progression of Mario's jump with a round curve for the attack stage.

Dashing:

In order to achieve our ideal fast paced combat system, we planned to go beyond just smooth movement and add some versatility and nuance to combat. One mechanic we added that was originally a stretch goal was dashing. In the current game, dashing will record the direction the mouse is compared to the player, travel in that direction at a high speed for a specific duration, and set the player to be invulnerable during the duration of the dash (complete with a rainbow trail effect to add to the sense of speed). The player then suffers a cooldown (three times as long as the duration of the dash by default) which is complete with a visual stamina bar as an indicator.
The dash is multipurpose. It adds a lot to the fast pace of the game by allowing the player to strategically dash around the maze-like walls of the dungeon. Dashing allows the player to be far more mobile in combat and always have an escape from an impossible set of projectiles or flanking enemies. All of this makes it a nice use of the right mouse click button to break up shooting.

Shooting:

Part of the implementation for shooting was created by Quang. This included the instantiation of the projectile object and the button mapping. I aided with setting the code for projectile movement so that the projectile properly aims at the location of the mouse, as well as setting tunable variables for damage that derive from a modifiable static variable in the player controller (something that allows for upgrades to affect the damage of new projectiles). In addition, I created a system where the player will have a faster firing speed when clicking the button vs when holding it down, something done by many games to make shooting feel more responsive by almost always registering clicks to shoot.

Ranged Enemy Shooting:

After finishing much of the player controller by implementing all player movement and shooting, I realized I would have to go out of my role a bit more to help some of my teammates. At the time, we were low on enemies so I helped create a ranged variant of the single melee enemy we had at the time. Instead of dealing contact damage, ranged enemies will go into attack mode when they make line of sight with the player, charging up a shot and slightly slowing down their movement speed but still following the player. They shoot projectiles at the player, which behave somewhat similarly to player projectiles but target the player’s position instead of the mouse position.

For our game, we intended a very fast paced gameplay to pair with the fact that the less time you take, the more currency you have. However, in early testing we realized that our generic “if player pressed d, set velocity to 1” physics code that consists of 10 lines would not just not cut it. Not only that, but the physics had to be high quality for other areas, such as shooting/aiming, as well as even enemy movement, or enemy shooting. Physics was a surprisingly encompassing part of our game, so I had quite a bit of work to do.

ADSR Movement:

In our original project draft, I mentioned hoping to create an especially complex movement system with momentum and direction. While I did not create something quite as complex as I originally envisioned, partially due to the fact that I had to aid my team in many other aspects of the project, I did create an extremely detailed ADSR movement system, based on code that the teacher wrote for a lecture. While just taking the teacher’s code makes the job sound easy, this was a huge challenge considering that our game took place in two directions (compared to our teacher's single directional left/right example). I ended up writing somewhere around 200 lines of code in the PlayerController.cs script to properly add directional momentum and ADSR stages for both left and right movement. This paid off nonetheless, as our final movement system is easily tunable and feels wonderfully smooth with WASD controls. Currently, we emulate the square root based progression of Mario's jump with a round curve for the attack stage.

Dashing:

In order to achieve our ideal fast paced combat system, we planned to go beyond just smooth movement and add some versatility and nuance to combat. One mechanic we added that was originally a stretch goal was dashing. In the current game, dashing will record the direction the mouse is compared to the player, travel in that direction at a high speed for a specific duration, and set the player to be invulnerable during the duration of the dash (complete with a rainbow trail effect to add to the sense of speed). The player then suffers a cooldown (three times as long as the duration of the dash by default) which is complete with a visual stamina bar as an indicator.
The dash is multipurpose. It adds a lot to the fast pace of the game by allowing the player to strategically dash around the maze-like walls of the dungeon. Dashing allows the player to be far more mobile in combat and always have an escape from an impossible set of projectiles or flanking enemies. All of this makes it a nice use of the right mouse click button to break up shooting.

Shooting:

Part of the implementation for shooting was created by Quang. This included the instantiation of the projectile object and the button mapping. I aided with setting the code for projectile movement so that the projectile properly aims at the location of the mouse, as well as setting tunable variables for damage that derive from a modifiable static variable in the player controller (something that allows for upgrades to affect the damage of new projectiles). In addition, I created a system where the player will have a faster firing speed when clicking the button vs when holding it down, something done by many games to make shooting feel more responsive by almost always registering clicks to shoot.

Ranged Enemy Shooting:

After finishing much of the player controller by implementing all player movement and shooting, I realized I would have to go out of my role a bit more to help some of my teammates. At the time, we were low on enemies so I helped create a ranged variant of the single melee enemy we had at the time. Instead of dealing contact damage, ranged enemies will go into attack mode when they make line of sight with the player, charging up a shot and slightly slowing down their movement speed but still following the player. They shoot projectiles at the player, which behave somewhat similarly to player projectiles but target the player’s position instead of the mouse position.

For our game, we intended a very fast paced gameplay to pair with the fact that the less time you take, the more currency you have. However, in early testing we realized that our generic “if player pressed d, set velocity to 1” physics code that consists of 10 lines would not just not cut it. Not only that, but the physics had to be high quality for other areas, such as shooting/aiming, as well as even enemy movement, or enemy shooting. Physics was a surprisingly encompassing part of our game, so I had quite a bit of work to do.

### Jasper Misc

I found that I had a lot of extra time to devote to the project after I had finished my main role, so I did several things to help in areas of the project less related to physics when people were busy and needed some help. Many of these things can be seen below:

Enemy Variants: Much more simple than it sounds, because I help with creating enemy stat systems I decided to help with enemy variety as we were moving onto populating our main map. This included 3 new melee enemies, including a small swarming enemy, an incredibly fast and damaging elite enemy, and an extremely high HP and damage enemy that is somewhat slow. These became the imp, orc, and ogre respectively. This also included 3 new ranged enemies, including a multi shot shotgun enemy, an enemy that deals melee and ranged damage with two projectiles, and an enemy that randomly fires blasts of projectiles in all directions. These became the orc shaman, skeleton, and pumpkin respectively. This simply involved creating specialized versions of the melee and ranged enemy prefabs with specialized stats, creating a huge amount of interesting enemy variety with little development cost. As a bonus, I also made a static pillar prefab that blocked the player, mostly to help Amaan and Harrison with map development.

Upgrade System: Most of this was handled by other team members, but I helped create some getter and setter functions to allow teammates to easily modify the player’s stats for upgrades. I did help in designing the functionality of two upgrades, being the health/regen boost and minigun upgrades, but most functionality was with the aid of teammates due to UI being actively worked on quite a bit (and contributions sadly creating nasty merge conflicts).

Gun Sprite: Originally a template sprite, I drew the rainbow colored gun that appears in gameplay, as well as creating the base code for animating it pointing at the mouse.

Enemy/Player Health: As we were preparing to create a basic combat system, some team members were busy so I created a public function for both enemies and players that allowed changing health. This function allows healing or damage, having proper interactions for when positive health is added or the function is used to subtract health. This allowed player regeneration to be implemented, player damage (while respecting the invulnerability timer I implemented, that’s another thing I added but I don’t want to crowd things too much), enemies being damaged, and all of this with variable inputs so the player can take different damage from different enemies (or deal varying damage with upgrades).


With all of that out of the way, here are the scripts I wrote:

Implemented HP system, damage increase on negative time, and basic sprite flipping towards the player: 

https://github.com/rodsters/CrunchTime/blob/7e7ff9f1401a437ff8b8087999bd693199f9f0a5/CrunchTime/Assets/Scripts/EnemyController.cs#L244


Created most of the code for animating the player’s gun (besides a few updates, like teammates making it held out from the player):

https://github.com/rodsters/CrunchTime/blob/3cc4ec73027d4d57ff8859342c4e3c78141b38a0/CrunchTime/Assets/Scripts/RainbowGunAnimation.cs#L1


Implemented the full sound manager and basically all code that uses it (applies to the audio role below):

https://github.com/rodsters/CrunchTime/blob/3cc4ec73027d4d57ff8859342c4e3c78141b38a0/CrunchTime/Assets/Scripts/SoundManager.cs#L1


After the base projectile system was implemented by Quang, I added a system for the projectile to be fired in the direction of the mouse, properly deal damage to enemies, and have tunable inaccuracy/damage based on player statistics.

https://github.com/rodsters/CrunchTime/blob/3cc4ec73027d4d57ff8859342c4e3c78141b38a0/CrunchTime/Assets/Scripts/ProjectileController.cs#L1


Mostly a copy of projectile controller, I added most of the new code for ranged enemy damanging and aiming: 

https://github.com/rodsters/CrunchTime/blob/7e7ff9f1401a437ff8b8087999bd693199f9f0a5/CrunchTime/Assets/Scripts/EnemyProjectileController.cs#L1


Much of my work is here, as I helped with most of the code for the player due to my connections with movement and physics. Some code is from the teacher but cited (mostly ADSR functions), and there are various contributions from teammates.

https://github.com/rodsters/CrunchTime/blob/7e7ff9f1401a437ff8b8087999bd693199f9f0a5/CrunchTime/Assets/Scripts/PlayerController.cs#L1


## Animation and Visuals - Harrison Nguyen - HN4982

Assets:
Dungeon Tileset II - https://0x72.itch.io/dungeontileset-ii
(../Art/frames/ & ../Art/paletteDungeon)
16x16 Dungeon Walls Reconfig - https://aekae13.itch.io/16x16-dungeon-walls-reconfig (../Art/paletteDungeonv2)
DungeonUI - https://0x72.itch.io/dungeonui (../Art & ../Art/UI)

One of my goals for this project was to provide a somewhat visually coherent game. The first step I took in this was in procuring assets for the game. Between creating original art and using free assets I chose the latter and luckily found Dungeon Tileset II by 0x72. It had enough sprites to build a palette and tilemap as well as dozens of characters and animations. Another great benefit to using it was that a lot of people have been involved in it, whether by using it, making tutorials with it, or best of all, extending it. The 16x16 Dungeon Walls Reconfig was one such extension and Dungeon UI was also created by 0x72. These three assets formed the core visuals for the game.

Palette: After I had gathered all of the assets, I spent some time gathering and picking colors from the assets to build a basic color palette:.
I would use GIMP and this color palette to create and stitch together most of the assets that were not already made and ready. A lot of the UI assets where stitched together and hopefully uses these colors to a decent effect in order to mesh with the game.

Tile Palette: After finding the first asset, the first piece of work I did was to create a basic tile palette. This would allow Amaan to think about map design and it could be used for any sort of development from anyone else. A drawback to the paletteDungeon tile palette was that it was bothersome to use in regards to walls, with walls having multiple tile parts making placement tedious.

Rule Tile: After implementing the first basic tile palette, I used the second asset to create a rule tile for walls. This would make editing the tile map somewhat quicker.
References:
 https://www.youtube.com/watch?v=Ky0sV9pua-E 
https://www.youtube.com/watch?v=QBrgrIbjXnE 

Animation: For this project, I stuck with Unity’s built-in Animator. I mostly stitched together the sprites in the first asset to create animations and passed them along with Animation Controllers for each set of character animations. For the player, I created a couple of coroutines to make the player fade/flash in response to in-game events. A good chunk of what I did was insert code into scripts produced by other team members in order to produce some visual effect.
References:
https://www.youtube.com/watch?v=hkaysu1Z-N8 
https://www.youtube.com/watch?v=65IrtBEZeVs 

UI: After Michael had outlined a basic UI, I went in and created/added assets to give it a good look. I tweaked the various menus and UI elements such that they were positioned well. I created some small icons such as the pointers and the hourglasses and added some code to make sure the buttons were responsive and not too jarring to click.

Visual Effects: As previously mentioned I created some coroutines to make the player fade/flash (primarily for when the player gets hurt or dashes). I also created the darkness effect by using sprites/sprite masks and writing a script to control the sprite mask scale. The scrolling background in some of the menus came from another simple script moving their transform (I couldn’t figure out how to change a texture offset).
References:
https://www.youtube.com/watch?v=4pl8DcsCQ_k 
https://www.youtube.com/watch?v=7hvcTKFNG9M 

After most of the game had been developed, I focused my time on making sure that all of the art and assets were applied well and properly. I spent some time applying the second tile palette to the map. Jasper turned a great number of the assets into working prefabs. I placed a number of the pillar prefabs he created. I created the simple red circle asset used for the enemy projectiles.

## Input - Quang Nguyen

For this game, we used what we have learned in the command pattern exercise in our first assignment. However, instead of creating an abstract class / interface for the play moment, we handled the input straight inside the PlayerController.cs.

### Inputs

During the main gameplay, the inputs for the game are the following:
Go Left - A key
Go right - D key
Go up - W key 
Go down - S key
Fire projectile - Left Mouse button or Spacebar key
Dash - Right Mouse button

Reference to Scripts:
PlayerController.cs

## Game Logic - Amaan Khan

### General Game Logics

For general game logic, I implemented a level system, where enemies of the next level  only spawn, when we finish the current level. At first I started with creating a system where the doors open and close if enemies in the current level are all killed. 

For this I had to come up with a way to recognize that all enemies in the current level are dead. Since we only wanted to spawn new enemies when the player unlocks a new room/level, I decided to create some form of a global state tracker that tracks current enemies alive. We already had an empty object called GameManager that was managing the overall timer. I attached another script to it, called EnemyTracker.cs, which has increment and decrement methods that are used to update the current number of enemies in the scene. The increment method is called everytime a new enemy is instantiated, and decrement method is called every time the enemy is killed.

 Some issues that we ran into were that sometimes unity thought that the enemy is being killed multiple times due to how fast it's being killed, so in order to fix this issue, Jasper recommended that we put this in a boolean “lock” the first time we find an enemy to be dead. This tracking is important for our game to function because our doors only open up when all enemies in the current scene are dead, and if some enemies are being recognized as multiple enemies being dead, then our door and levels can be unlocked before all the enemies in the current level are dead. 

I created another script called LevelManager.cs which just increments level each time the number of total enemies are zero. The global variable “level” that tracks current level is used by the door tilset which they only open up if their door level matches the current level. 

I also created a system to easily place empty objects in the scene and the script spawns enemies in that place, depending on the name of that empty game object. This all, The script basically takes the parent game object “spawns” as input, then loops over its children to access each of the child's positions and spawn it. Before this we had to manually input the vector3 location in our script to instantiate enemies. 

References used:
For enemy spawner: 
https://www.youtube.com/watch?v=C3VExnf4kmY 
For door system:
https://youtu.be/QRp4V1JTZnM

## Enemy AI - Trevor Lopez

*A\* Pathfiniding* - This aspect of enemy movement tracks the player or any other arbitrary `Transform` object and moves the enemy agent toward that object. I initially worked to create the algorithm for this from scratch since the Unity `NavMesh` system I had heard about doesn't work for 2D games. I had tried to implement A* using Dijkstra's algorithm with a heuristic function based on straight line distance to the target, but I ran into so many issues with this. Such issues included accounting for the size of the AI agent, making an efficent static graph structure, the main thread being blocked during path creation, and needing to reimplement basic data structures that aren't a part of the C# standard library in the version that our Unity runs (eg. priority queue). Thus, I opted instead to use [Aron Granberg's A* library](https://arongranberg.com/astar/docs/) so that my teammates wouldn't have to wait ages for me to get that all to work. Using this library I was able generate and update paths between two points along the tilemap grid thanks to the static graph generated from putting our walls on a separate obstacle layer that could be scanned by our A* object. Even with this solution, the enemy agent size still was not properly accounted for in grid navigation, and it had some jittery movement when regenerating paths sometimes, but I was able later to deal with this through the addition of steering behavior. 
[Original implementaion](https://github.com/rodsters/CrunchTime/blob/0807745196ad7705d8781913eb20c7e96f942474/CrunchTime/Assets/Scripts/EnemyController.cs#L13-L73)
[Main presence in current system](https://github.com/rodsters/CrunchTime/blob/701319b196fb3181710df6ce3694fd6b67ba5f57/CrunchTime/Assets/Scripts/EnemyController.cs#L99-L139)

*Flocking and Steering* - We ended up having problems with enemies clumping together as the A* paths would converge close to the player. Thus, we ended up needing a system to not so rigidly follow the A* generated paths by controlling the speed and direction of movement of an agent based on proximity to other nearby enemies. Using some basic ideas as inspiration from [boids](https://www.youtube.com/watch?v=bqtqltqcQhw), I have the enemy agent find a starting direction based on the direction that they should be moving due to the A* path. I then modify the direction of movement by having the agent look within some set viewing radius and angle outward in the starting direction. Using raycasts evenly spaced in this viewing region outward from the agent, we can add up what fraction of the viewing radius that a given ray was hit at (taking into account the direction vector of the ray). After normalizing the sum of vectors, this effectively comes up with a new direction weighted toward a more open space. This is what I denote as *steering*. I then use another raycast in this new direction to determine if there is still an enemy within the viewing radius, and if there is, I scale the agent's max speed down to avoid clumping. In a very rudimentary sense, I denote this as *flocking*. I also made gizmos to help my teammates in deciding an enemy agent's viewing area (radius and angle), and I also used them to help me in debugging.
[Flocking implementation](https://github.com/rodsters/CrunchTime/blob/701319b196fb3181710df6ce3694fd6b67ba5f57/CrunchTime/Assets/Scripts/EnemyController.cs#L129-L181)
[Steering implementation](https://github.com/rodsters/CrunchTime/blob/701319b196fb3181710df6ce3694fd6b67ba5f57/CrunchTime/Assets/Scripts/EnemyController.cs#L183-L194)
[Gizmos](https://github.com/rodsters/CrunchTime/blob/701319b196fb3181710df6ce3694fd6b67ba5f57/CrunchTime/Assets/Scripts/EnemyController.cs#L218-L242)

*Obstacle Impulse* - With the flocking and steering system in place, I still had other troubles with enemy agents pushing each other into obstacles (eg. the walls). As such I made it so that if the enemy gets too close to a wall, there is an impulse in the steering direction opposite the direction of the ray that hit the wall. This ended up being a simple but effective solution for smoother movement. I would like to note that I wan't able look at this problem in a similar way to the [boids with obstacles](https://youtu.be/bqtqltqcQhw?t=104) since the enemy doesn't rotate toward its movement direction and, as such, a ray not hitting an obstacle doesn't guarantee a path along that ray that avoids the obstacles when also accounting for the enemy's size.
[Impulse implementation](https://github.com/rodsters/CrunchTime/blob/701319b196fb3181710df6ce3694fd6b67ba5f57/CrunchTime/Assets/Scripts/EnemyController.cs#L163-L172)

*Straight Shot Attack Movement* - One last problem of the A* pathfinding is that since paths are regenerated at a set interval, enemies near the player would end up reaching the end of their path where the player used to be. A convenient fix that I made was to have the enemy agent use a starting direction pointing straight toward the player if they were in line-of-sight without obstruction by a wall (used raycast to determine this). Due to obstacle impulse and steering, this doesn't have a problem with bumping into obstacles partailly within the line-of-sight path.
[Straight shot implementation](https://github.com/rodsters/CrunchTime/blob/701319b196fb3181710df6ce3694fd6b67ba5f57/CrunchTime/Assets/Scripts/EnemyController.cs#L118-L128)

Sadly, I was unable to get enough time to design a boss for our game, but I believe that these systems could be easily extended to meet the need of such a system in the abstraction of the `EnemyController` as an underlying `MonoBehavior` of an enemy — including a boss.

# Sub-Roles

## Audio - James Jasper Fadden O’Roarke

For Crunch Time, I created three original compositions that you may hear in game. These are the main gameplay theme (known as Crunch Time), the theme for when the player reaches negative time (known as TestGen), and the theme for when the player is in the gameover or menu screens (known as Altar). These three compositions were generated using OpenAI’s MuseNet as MIDI files (Payne, Christine. "MuseNet." OpenAI, 25 Apr. 2019, www.openai.com/blog/musenet). This was not as simple as pressing a button to get random music, but painstakingly monitoring outputs as the AI generates seconds long pieces of the song and guiding it along the right path. After generation, the audio is modified using the tool Audacity to allow level sound, looping, and more. Finally, a soundfont is applied to set the instruments of the music, and considering our retro aesthetic I used instruments seen in the NES. This arduous process allows the creation of the unique style of music you hear in game.

Here are the 3 files below for listening purposes:
https://github.com/rodsters/CrunchTime/blob/e3467c7d0cd08c485d8032785b7c012d60c6dfe0/CrunchTime/Assets/Audio/Altar.mp3
https://github.com/rodsters/CrunchTime/blob/e3467c7d0cd08c485d8032785b7c012d60c6dfe0/CrunchTime/Assets/Audio/Crunchtime.mp3
https://github.com/rodsters/CrunchTime/blob/e3467c7d0cd08c485d8032785b7c012d60c6dfe0/CrunchTime/Assets/Audio/TestGen.mp3

Besides music, I also helped organize the CC0 sound effects used in the game (of which there are 6 total for player shooting, player damage, enemy damage, etc.). To make use of this, I created the audio system that the game uses based on the audio system seen in exercises. This was surprisingly tough though, as I had the challenge of creating a persisting audio engine between scenes. In the end, to preserve ease of debugging, I added an audio engine to the main menu and gameplay scene. Whichever audio engine exists first is selected as the main audio engine to be used in gameplay (allowing audio when the main menu is skipped for debugging purposes) and is set to persist between scenes. In addition to creating the audio system itself, I created most of the code for the audio logic, whether playing sound effects when a bullet hits an enemy (or a wall with a lower noise), or playing the music track for negative time when the player spends too long.

## Gameplay Testing - Rodney Johnston

In order to improve the game, I found 10 people unrelated to this project and organized a time for them to playtest our game. From their feedback, we were able to find important bugs and made adjustments to the game based off of this feedback. 

Some of the issues people had with our beta game, was that movement speed provided an ability to phase through walls, shooting was too fast making the game too easy, shop was hard to find/ understand, movement was really finicky to some playtesters especially with upgrades, you could shoot through thin walls, and the dash was a little hard for certain players to utilize as it was based off mouse position and not the direction it was facing.

For some of the fixes, we made sure that movement upgrades wouldn't stack and we made the game considerably harder, as the gameplay was too easy for the playtesters. Furthermore, I think that given more time, we would have been able to fix other visual or gameplay issues the playtesters had. Like, the shop interface and shopkeeper, story line, and other minor bugs being addressed. Overall, I think watching the playtesters play our game was pretty cool, satisfying, and helpful even if they broke our game a lot.

Feedback:
https://github.com/rodsters/CrunchTime/blob/main/Observations%20and%20Playtester%20Comments.pdf

## Map Design - Amaan Khan

For this role, with the help of some tutorials and Rodney’s help, I was able to understand how 2d grid and tilesets work. We started off with just two placeholder tiles that represented the walls which had a collider on and floor with no collider. I created the base map using these trial tile sets, here is the first version of our map.

![Map 1](https://github.com/rodsters/CrunchTime/blob/michael---summary-and-gameplay-explanation/MapImage1.png)

When designing this map, I wanted the levels to progressively get harder. We start off with a simple introductory level with few enemies to introduce basic game mechanics, and then we go to another level with 4 barriers in the middle where players can try dodging and taking cover using these barriers. 
The next two levels were inspired by pac man, I wanted the player to move around and take cover, in my head i would place some enemies in the around the edges more so the player is forced to take the middle route which is more complex.

After that, comes the maze level, Where as the player enters, he/she sees what that there is some power up or an enemy present on the other side of the wall to the right and left, player still doesn’t know how to get to these, so it takes the first left, and realizes it’s just a loop so then player goes down finds more enemy on the way, then arrives at a place where player has to make a decision whether to go up or down, they'll have to go to both places either way in order to progress to the next level. I added some zig zag path which would force the player to be more careful, or get better at the movement in order to get get blocked by the barriers. 

Now comes the crunch time level, where In my mind, we would place a lot of enemies, creating some kind of “stress” for the player, maybe add some speedy enemies that are hard to dodge. 

After that comes an empty room, with the boss fight, we ended up just putting a lot of enemies in this room, but in my mind when i created this empty room I assumed there would be a big boss that we would need to fight off. 

The end result of the map is very similar, we added a shop in the middle of level 3 and 4 and changed the tileset to match the dungeon theme.

![Map2](https://github.com/rodsters/CrunchTime/blob/michael---summary-and-gameplay-explanation/MapImage2.png)

Each door in the map is a separate tileset that shares a script, which allows it to destroy itself when level is incremented. This system is described above in the game logic. 

## Narrative Design - Michael Bai - github: Mbai2000

### Intro Cutscene

Our game features a knight who has been trapped within ancient ruins filled with monsters. Upon entering he is cursed for disturbing the ruins. He has to fight against time to become stronger and escape before the curse begins taking hold, and the monsters overrun him. Although our game does not have a deep or complex narrative, we wanted to tie it to the theme of "The Best of Times, the Worst of Times". This took shape in the utilization of time. When the player first begins playing the game, the player has plenty of time to spend on upgrades, and has no negative effects applied to them, representing the "best of times". However, when their time reaches 0, the player will begin receiving de-buffs, and eventually die if time reaches -3 minutes (the curse taking effect). Additionally, although the player can still technically spend time on upgrades, this will cause the player to receive additional de-buffs and bring them closer to game over. This represents the worst of times. 

In order to setup the scene somewhat, a short intro cutscene was created that would play upon the player clicking Start on the Start/Main Menu. The cutscene can be skipped upon pressing ESC. This cutscene shows the player character (a knight), as well as an assortment of the enemies found within the game. The knight runs through a hall at a medium pace, as monsters begin to appear behind him. The knight turns around and notices the monsters, before running away at a faster speed. The monsters chase the knight off-screen, at which point the game begins. 

A video was referenced when creating the opening cutscene.

[Cutscene Reference](https://youtu.be/Y5RDtN1jM6A)


## Press Kit and Trailer - Trevor Lopez

[Press Kit](https://trevnerd.github.io/CrunchTimePressKit/)
[Trailer](https://youtu.be/PIaADqKG3h0)

When I created the trailer I wanted the watcher to be able to understand the main gameplay system without any words on screen. As such, I constructed the trailer with a rough sequence in mind to have the progression of the trailer match the general progression of the game. We see the main menu, then the player, then the timer, then the player combating foes. These are all main aspects of the game that a player would immediately see and interact with. Then, the player eventually finds the shop in which they can buy great upgrades at the cost of that time they saw emphasized earlier. Through the purchase of the item, the player is affected by *negative time* which has it's own visual element that appear for the trailer. Now, that all the elements have been introduced we are able to have some fun and dash around fighting enemies at a fast pace until we die. With such gameplay we are enticed to press start again. Thus, the call to action is put forth with an end card for the game. The trailer was made with the game sounds and music edited for dramatization to give additional power to the visual elements and cuts.

I chose the screenshots with much less narrative thought involed. I similarly wanted to display the various features of the game that could be represented visually, so I took a sample of pictures throughout my gameplay.

## Cross Platform - Harrison Nguyen

Through the process of this project I spent a fair amount of time debugging code. As long as the code worked, I could build the game for PC, Mac, Linux, and WebGL. 

I ended up going through a number of the scripts, gaining an understanding of some of the code base and mostly added/removed code to counter unintended behavior that I found as I played the game. While working on the UI I made sure that it would look okay on most if not all resolutions by taking advantage of Unity’s built-in capabilities. With the knowledge, I spent some time helping Micheal troubleshoot the cutscene. In a similar regard, I communicated with others to edit snippets of code and complete small tasks in order to push the project to completion.

At some point I tried to implement a Mobile UI for mobile gameplay but it fell through due to my poor grasp of controllers/input devices. I only managed to get as far as putting in a couple of joysticks and getting one joystick to shoot on mobile and had great difficulty in converting a touch screen joystick input into a WASD parallel. My failure in this part was ultimately due to poor communication and planning on my end with my teammates. I will note that technically, mobile is playable but is limited to shooting, dashing, and UI interaction which makes for clunky gameplay.


## Game Feel - Quang Nguyen

Crunch Time is a time-based dungeon crawling game. Therefore, we have selected a dark theme asset pack to fit the game. Since this is a top-down game, we believed that having a smooth camera is crucial. Having the fixed camera on top of the player is pretty static and boring. To help with the fluidity of the player’s experience, we created a Lerp Camera. This helps smooth out the player's movements. Depending on the speed of the lerping camera, we can scale up the difficulty of the game. For example, when the player moves really fast ahead, the camera will keep on catching up with the player. They will not able to see if there are enemies ahead of them. The player should think carefully before dashing forward into the abyss.
