# Oblivionburn Productions Game Engine
### This is the source code for a simple C# game engine used to make 2D tile mapped games for Windows (DirectX) in Visual Studio 2022. Uses MonoGame libraries for rendering pipeline and FMOD for sound.
###### Disclaimer: This does not include an editor like you would find with Unity or Unreal Engine, this is for programming games from scratch in Visual Studio. I created this 'engine' to save myself some time with getting new games up and running. I've mostly used it for making RPG, Strategy, and Simulation games.

### Dependencies:
- MonoGame v3.7.1.189+ for Windows Desktop DirectX (dll's included in the zip release, but you'll still need to install it for the templates in VS)
- FMOD Sound System dll v0.2.1.4+ (not included for legal reasons, but C# wrapper for it is)
- Your game to be a DirectX Windows Application project using .NET Framework 4.8+

### Features:
- Focus on organization, optimizations, and keeping things simple
- Overridable methods for per-game customizations
- Modular component architecture for only including what's needed/wanted in a project
  - Example: "Components.Add(new InputManager(this));" in your game's LoadContent() method
- UI objects: Button, Label, InputBox, Picture, ProgressBar, and Slider
- World object (for 2D tile mapping) consisting of a list of Map, each Map has a list of Layer, and each Layer has a list of Tile
  - Purpose: so you can do stuff like rendering a floor tile with a table on it, a plate on that table, and a piece of food on that plate
  - Tile class includes Inventory (for when stuff was dropped on the ground) and ProgressBar (for objects that can sustain damage)
  - Includes large list of randomly generated fantasy-sounding names for villages/cities/maps/etc

# Components:

## InputManager
- For handling Mouse, Keyboard and up to 4 gamepads
  - Input handlers have Updated_By_Game boolean flag to flip between updating per game's update loop (True) or being event-driven (False) which is True by default in every handler (KeyboardHandler, MouseHandler, and GamepadHandler)
  - Have to manually add keys in KeyboardHandler.KeysMapped, so iteration time can be kept as efficient as possible
  
## CharacterManager
- For flexible grouping of NPCs and/or players
  - Contains a list of Army, each Army has a list of Squad, and each Squad has a list of Character
  - Includes large lists of first/last names for random name generation
  - Character class includes:
    - Inventory (an empty list of Items)
    - Empty string lists for Stats, Skills, Traits, and StatusEffects
    - A* pathing
    - Events for detecting noises, sights, and smells (handy for stealth mechanics and realistic NPC reactions)
    - Job class for queueing Tasks and handling AI schedules
      - Task class includes a progress bar to render progress on the screen
    - Health/Mana progress bars
    - Spellbook (see SpellbookManager below for details)
    - Some basic animation code for spritesheets with 4 directions of movement (can override Animator class for more)
      - Default usage expects spritesheet to have sprites facing South in first row, West in second row, East in third row, and North in fourth row
    - Boolean flags for Interacting, Unconscious, Dead, InCombat, and Travelling
    
## JobManager
- For central organization of Jobs (jobs not added to it by default)
  - Purpose: so you can iterate through all the Jobs without having to iterate through all the Characters, or to run Jobs separate from the Characters' Jobs
  
## InventoryManager
- For central organization of Inventories (inventories not added to it by default)
  - Purpose: so you can iterate through all the Inventories without having to iterate through all the Characters/Tiles/etc, or to store inventories independently
  
## TimeManager
- For tracking global in-game time with custom TimeHandler class for event-driven time tracking
  - Purpose: so you can do stuff like adding a minute of in-game time and trigger logic for every millisecond passing in that minute (very handy for simulations)
  
## CraftingManager
- For central organization of crafting recipes
  - Purpose: convenient storage/lookup
  
## ResearchManager
- For central organization of research trees
  - Purpose: convenient storage/lookup

## MenuManager
- For central organization of Menus (menus not added to it by default)
  - Purpose: handling the updating/rendering of multiple menus
    - If a menu is Active but not Visible, then its Update method will still run without the menu rendering to the screen
  - Menu class contains lists of all UI objects, and has methods for getting menus built more quickly/easily
  
## SceneManager
- For central organization of Scenes (scenes not added to it by default)
  - Purpose: to organize stuff like screens (e.g. Title and Loading screens), levels, cutscenes, etc
  - Scene class has its own Menu and World instance
  
## SoundManager
- For handling playing/pausing/stopping and volume control of sounds, music, and ambient noise using FMOD

## SpellbookManager
- For central organization of Spellbooks (spellbooks not added to it by default)
  - Purpose: so you can iterate through all the Spellbooks without having to iterate through all the Characters, or to store Spellbooks independently (for Characters to pick up later?)
  - Spellbook class has a list of Spells
  - Spell class has a list of Properties to handle spells with multiple elements/effects/textures/sounds
  
## ParticleManager
- For handling/rendering particles (very basic, could probably expand on this more yet)

## WeatherManager
- For handling some basic weather effects (e.g. Raining, Storming, Snowing and Fog) with methods for transitioning between them over time
  - Has its own instance of ParticleManager for rendering the weather textures
  - Requires texture names to match the weather types (Rain, Storm, Snow, and Fog)
  
## AssetManager
- For organizing and loading Textures (.png), Shaders (.FxDX), Fonts (.xnb), Sounds, Ambient noise, and Music
  - Expects "Content" folder in game dir with the following structure:
    - "Ambient" folder
      - Better to use wav/ogg for Ambient Noise tracks, since they're more loop-friendly (mp3 always has a second of silence at the start of it)
      - There's an AmbientLooping option in SoundManager
    - "Fonts" folder
      - Monogame is a continuation of Microsoft's XNA framework, which compiled fonts into .xnb files, and I haven't gotten around to creating a custom spritefont loader, yet, so fonts are still expected to be .xnb files in order for Monogame/XNA to load them
    - "Music" folder
      - Music organized in sub-folders (e.g. a "Day" folder with various music tracks to play during the game's daytime and a "Night" folder with various music tracks to play during the game's nighttime, which the game can randomly select from when a track finishes playing)
      - There's a MusicLooping option in SoundManager if you want to use that instead of randomly selecting tracks from the sub-folders
    - "Shaders" folder
      - Shaders need to be written as .fx files and compiled into .FxDX using Monogame's 2MGFX.EXE tool in order to be loaded into the game as an "Effect" object
    - "Sounds" folder
      - Sound variants organized in sub-folders (e.g. "Click" folder with Click1.mp3, Click2.mp3, etc up to a max of 9 since only the last digit is grabbed for numbering (number is used for random selection))
      - Non-varients in base Sounds folder
      - There's no loop option for sounds
    - "Textures" folder
      - Textures organized in sub-folders

# Utilities:
- Something class that most objects inherit from which includes many basic variables (so everything can have an ID, Name, etc)
- Region class that renderable objects use instead of Monogame's Rectangle struct
  - Makes it possible to do stuff like making a higher layer of tiles reference the regions in a lower layer of tiles, so you only have to iterate through a single layer to move multiple layers across the screen (far more efficient than iterating through every Tile in every Layer when you have thousands of tiles in many layers)
- Cryptography-grade random number generator for extreme randomness (named CryptoRandom in library)
- GetLine method for fast ray-tracing between two coordinates (returns list of coordinates between the two points)
- Various enums, structs, and other classes
  
