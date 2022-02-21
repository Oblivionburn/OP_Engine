# Oblivionburn Productions Game Engine
## Source code for simple C# game engine used to make 2D tilemapped games for Windows (DirectX) in Visual Studio 2022. Uses MonoGame library for rendering pipeline and FMOD for sound.
###### Disclaimer: This does not include an editor like you would find with Unity or Unreal Engine, this is for programming games from scratch in Visual Studio. I created this 'engine' to save myself some time with getting new games up and running. I've mostly used it for making RPG, Strategy, and Simulation games.

### Dependencies:
- MonoGame for Windows Desktop DirectX v3.7.1.189+ (dll's included)
- FMOD Sound System dll v0.2.1.4+ (not included, but C# wrapper for it is)

### Features:
- Focus on organization, optimizations, and keeping things simple
- Overridable methods for per-game customizations, and nearly everything is Disposable
- Object-oriented design and optional component architecture for only including what's needed/wanted in a project
- InputManager component for handling Mouse, Keyboard and up to 4 gamepads
- CharacterManager component (for grouping NPCs and/or players) consisting of a list of Army, each Army has a list of Squad, and each Squad has a list of Character 
  - Includes large lists of first/last names for random name generation
  - Character class prefit with Inventory, Stats and Skills lists, A* pathing, Job class for task queueing and AI scheduling, Health/Mana progress bars, Spellbook, and some basic animation code for spritesheets of 4 animations (can override Animator class for more)
- JobManager component for central organization of character Job (jobs not added to it by default)
- InventoryManager component for central organization of Inventory (inventories not added to it by default)
- TimeManager component for tracking global in-game time with custom TimeHandler class for event-driven time tracking
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
