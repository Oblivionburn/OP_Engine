# Oblivionburn Productions Game Engine
## Source code for simple C# game engine used to make 2D tilemapped games for Windows (DirectX) in Visual Studio 2022. Uses MonoGame library for rendering pipeline and FMOD for sound.
###### Disclaimer: This does not include an editor like you would find with Unity or Unreal Engine, this is for programming games from scratch in Visual Studio. I created this 'engine' to save myself some time with getting new games up and running. I've mostly used it for making RPG, Strategy, and Simulation games.

### Dependencies:
- MonoGame for Windows Desktop DirectX v3.7.1.189+ (dll's included)
- FMOD Sound System dll v0.2.1.4+ (not included, but C# wrapper for it is)

### Features:
- Focus on organization, optimizations, and keeping things simple
- Overridable methods for per-game customizations
- Modular component architecture for only including what's needed/wanted in a project
- InputManager component for handling Mouse, Keyboard and up to 4 gamepads
  - Inputs have options for handling as event-driven or per game update loop
- AssetManager component for organizing and loading Textures (.png), Shaders (.FxDX), Fonts (.xnb), Sounds, Ambient noise, and Music
  - Expects "Content" folder in game dir with the following structure:
    - "Ambient" folder
    - "Fonts" folder
    - "Music" folder
      - Music organized in sub-folders (e.g. a "Day" folder with various music tracks to play during the game's daytime and a "Night" folder with various music tracks to play during the game's nighttime, which the game can randomly select from when a track finishes playing)
    - "Shaders" folder
    - "Sounds" folder
      - Sound variants organized in sub-folders (e.g. "Click" folder with Click1.mp3, Click2.mp3, Click3.mp3)
      - Non-varients in base Sounds folder
    - "Textures" folder
      - Textures organized in sub-folders
- CharacterManager component for flexible grouping of NPCs and/or players consisting of a list of Army, each Army has a list of Squad, and each Squad has a list of Character
  - Includes large lists of first/last names for random name generation
  - Character class includes:
    - Inventory (an empty list of Items)
    - Empty string lists for Stats, Skills, Traits, and StatusEffects
    - A* pathing
    - Events for detecting noises, sights, and smells
    - Job class for task queueing and AI scheduling
    - Health/Mana progress bars
    - Spellbook (an empty list of Spells, each Spell contains an empty list of Properties inheriting from generic Something class)
    - Some basic animation code for spritesheets with 4 directions of movement (can override Animator class for more)
    - Boolean flags for Interacting, Unconscious, Dead, InCombat, and Travelling
- JobManager component for central organization of Jobs (jobs not added to it by default)
- InventoryManager component for central organization of Inventories (inventories not added to it by default)
- TimeManager component for tracking global in-game time with custom TimeHandler class for event-driven time tracking
- CraftingManager component for central organization of crafting recipes
- ResearchManager component for central organization of research trees
- World object (for 2D tile mapping) consisting of a list of Map, each Map has a list of Layer, and each Layer has a list of Tile
  - Includes large list of randomly generated fantasy-sounding names for villages/cities/maps/etc
- UI objects: Button, Label, Input box, Picture, Progress Bar, and Slider
- MenuManager component for central organization of Menus (menus not added to it by default)
  - Menu class contains lists of all UI objects, and has methods for getting menus built more quickly/easily
- SceneManager component for central organization of Scenes (scenes not added to it by default)
  - Scene can be used for stuff like screens, levels, cutscenes, etc
  - Scene class has its own Menu and World instance
- SoundManager component for handling playing/pausing/stopping and volume control of sounds, music, and ambient noise using FMOD
- SpellbookManager component for central organization of Spellbooks (spellbooks not added to it by default)
  - Spellbook class has a list of Spells
  - Spell class has a list of Properties to handle spells with multiple elements/effects/textures/sounds
- ParticleManager component for handling particles
- Utilities:
  - Something class that most objects inherit from which includes many basic variables (so everything can have an ID, Name, etc)
  - Region class that renderable objects use instead of Monogame's Rectangle struct
    - Makes it possible to do stuff like making a higher layer of tiles reference the regions in a lower layer of tiles, so you only have to iterate through a single layer to move multiple layers across the screen (far more efficient than iterating through every Tile in every Layer when you have thousands of tiles in many layers)
  - Cryptography-grade random number generator for extreme randomness (named CryptoRandom in library)
  - GetLine method for fast ray-tracing between two coordinates (returns list of coordinates between the two points)
  - Various enums, structs, and other classes
- WeatherManager component for handling some basic weather effects (e.g. Raining, Storming, Snowing and Fog) with methods for transitioning between them over time
  - Has its own instance of ParticleManager for rendering the weather textures
