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

## Movement/Physics

**Describe the basics of movement and physics in your game. Is it the standard physics model? What did you change or modify? Did you make your movement scripts that do not use the physics system?**

## Animation and Visuals

**List your assets including their sources and licenses.**

**Describe how your work intersects with game feel, graphic design, and world-building. Include your visual style guide if one exists.**

## Input

**Describe the default input configuration.**

**Add an entry for each platform or input style your project supports.**

## Game Logic

**Document what game states and game data you managed and what design patterns you used to complete your task.**

## Enemy AI - Trevor Lopez

**Document the AI systems present that allow for intelligent enemies, and describe how these systems were adjusted to account for a smoother gameplay experience.**

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

## Audio

**List your assets including their sources and licenses.**

**Describe the implementation of your audio system.**

**Document the sound style.** 

## Gameplay Testing

**Add a link to the full results of your gameplay tests.**

**Summarize the key findings from your gameplay tests.**

## Narrative Design - Michael Bai - github: Mbai2000

### Intro Cutscene

Our game features a knight who has been trapped within ancient ruins filled with monsters. Upon entering he is cursed for disturbing the ruins. He has to fight against time to become stronger and escape before the curse begins taking hold, and the monsters overrun him. Although our game does not have a deep or complex narrative, we wanted to tie it to the theme of "The Best of Times, the Worst of Times". This took shape in the utilization of time. When the player first begins playing the game, the player has plenty of time to spend on upgrades, and has no negative effects applied to them, representing the "best of times". However, when their time reaches 0, the player will begin receiving de-buffs, and eventually die if time reaches -3 minutes (the curse taking effect). Additionally, although the player can still technically spend time on upgrades, this will cause the player to receive additional de-buffs and bring them closer to game over. This represents the worst of times. 

In order to setup the scene somewhat, a short intro cutscene was created that would play upon the player clicking Start on the Start/Main Menu. The cutscene can be skipped upon pressing ESC. This cutscene shows the player character (a knight), as well as an assortment of the enemies found within the game. The knight runs through a hall at a medium pace, as monsters begin to appear behind him. The knight turns around and notices the monsters, before running away at a faster speed. The monsters chase the knight off-screen, at which point the game begins. 

A video was referenced when creating the opening cutscene.

[Cutscene Reference](https://youtu.be/Y5RDtN1jM6A)


## Press Kit and Trailer - Trevor Lopez

**Include links to your presskit materials and trailer.**

[Press Kit](https://trevnerd.github.io/CrunchTimePressKit/)
[Trailer](https://youtu.be/PIaADqKG3h0)

**Describe how you showcased your work. How did you choose what to show in the trailer? Why did you choose your screenshots?**

When I created the trailer I wanted the watcher to be able to understand the main gameplay system without any words on screen. As such, I constructed the trailer with a rough sequence in mind to have the progression of the trailer match the general progression of the game. We see the main menu, then the player, then the timer, then the player combating foes. These are all main aspects of the game that a player would immediately see and interact with. Then, the player eventually finds the shop in which they can buy great upgrades at the cost of that time they saw emphasized earlier. Through the purchase of the item, the player is affected by *negative time* which has it's own visual element that appear for the trailer. Now, that all the elements have been introduced we are able to have some fun and dash around fighting enemies at a fast pace until we die. With such gameplay we are enticed to press start again. Thus, the call to action is put forth with an end card for the game. The trailer was made with the game sounds and music edited for dramatization to give additional power to the visual elements and cuts.

I chose the screenshots with much less narrative thought involed. I similarly wanted to display the various features of the game that could be represented visually, so I took a sample of pictures throughout my gameplay.

## Game Feel

**Document what you added to and how you tweaked your game to improve its game feel.**

