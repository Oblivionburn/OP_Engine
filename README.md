# Oblivionburn Productions Game Engine
## Source code for simple C# game engine used to make 2D tilemapped games for Windows (DirectX) in Visual Studio 2019. Uses MonoGame library for rendering pipeline and FMOD for sound.
###### Disclaimer: This does not include an editor like you would find with Unity or Unreal Engine, this is for programming games from scratch in Visual Studio. I created this 'engine' to save myself some time with getting new games up and running. I've mostly used it for making RPG, Strategy, and Simulation games.



### Dependencies:
- MonoGame for Windows Desktop DirectX v3.7.1.189+ (dll's included)
- FMOD Sound System dll v0.2.1.4+ (not included, but C# wrapper for it is)

### Features:
- Focus on organization, optimizations, and keeping things simple
- Virtually no focus or effort put into fancy graphics effects (a simple pixel-based lighting system would be nice, though)
- 99% of the methods are overridable for per-game customizations, and nearly everything is Disposable
- Object-oriented design and optional Game Component architecture (for only including what's needed/wanted in a project)
- CharacterManager component consisting of a list of Army, each Army has a list of Squad, and each Squad has a list of Character. Used for grouping NPCs and/or players.
  - Includes large lists of first/last names for random name generation
  - Character class prefit with Inventory, Stats and Skills lists, A* pathing, Job class for task queueing and AI scheduling, Health/Mana progress bars, Spellbook, and some basic animation code for spritesheets of 4 animations (can override Animator class for more)
- JobManager component for central organization of jobs/tasks (jobs not added to it by default)
- InputManager component for handling Mouse, Keyboard and up to 4 gamepads
- InventoryManager component for central organization of inventories (inventories not added to it by default)
- World (tile map) consisting of a list of Map, each Map has a list of Layer, and each Layer has a list of Tile
  - Includes large list of randomly generated fantasy-sounding names for villages/cities/maps/etc
- Basic UI objects/classes for Buttons, Labels, Input boxes, Pictures/Images, Progress Bars, and Sliders
- MenuManager component for central organization of menus (menus not added to it by default)
  - Menu class contains lists of all basic UI controls and methods for getting menus built more quickly/easily
- SceneManager component for central organization of screens/scenes/levels (scenes not added to it by default)
  - Scene class has its own Menu and World instance
- SoundManager component for handling basic playing/pausing/stopping and volume control of sounds/music/etc using FMOD
- SpellbookManager component for central organization of spellbooks (none are added to it by default)
- ParticleManager component for handling particles
- AssetManager component for organizing and loading Textures (.png), Shaders (.FxDX), Fonts (.xnb), Sounds, Ambient noise, and Music
- Utilities:
  - Cryptography-grade random number generator for extreme randomness (named CryptoRandom in library)
  - GetLine method for fast ray-tracing between two coordinates (returns list of coordinates between the two points)
- WeatherManager component for handling/rendering basic weather effects (Clear, Rain, Storm, Snow and Fog) with methods for smoothly transitioning between them over time
  - Has its own instance of ParticleManager for rendering

### Patch Notes:
v1.0.1.0
- Moved Pathing from Utilities to Characters
- Created new Task class
- Created Job class with List of Task for handling queued tasks
- Created JobManager with List of Job for centrally handling/referencing jobs
- Replaced task variables on Character with new Job class (all variables removed exist in new Task class)
- Removed Patience variable on Character
- Added Job and Pathing disposal to Character.Dispose()
- Made ALocation disposable
- Replaced Price variable on Something class with Cost, Sell_Price, and Buy_Price

v1.0.0.4
- Added more generic variables to Something class
- Changed Something.Max_Value from int to float
- Added some extra code commentary
- Changed Category/Material on Item from single string to List of string

v1.0.0.3
- Updated AssetManager and SoundManager to use Sound object for Ambient/Music
- Added ContentManager to AssetManager
- Added base Content directories in AssetManager.Init
- Updated AssetManager loading methods to use new Content and base directories
- Added some other Play methods in AssetManager for Sounds/Ambient/Music

v1.0.0.2
- Fixed AssetManager not using static methods
- Updated AssetManager.LoadSounds to use Sound.Name instead of Sound.Description

v1.0.0.1
- Removed UpdateGear() from Animator
- Made Square disposable
- Changed Region on Item and Character to Square object instead of Rectangle struct
- Updated Character.Update and Character.MoveTo to use Vector2 instead of Rectangle
- Added max_distance parameter to Pathing.Get_Path

v1.0.0.0
- Initial release

