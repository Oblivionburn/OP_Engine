# Oblivionburn Productions Game Engine
## Source code for simple C# game engine used to make 2D tilemapped games for Windows (DirectX) in Visual Studio 2019. Uses MonoGame library for rendering pipeline and FMOD for sound.
###### Disclaimer: This does not include an editor like you would find with Unity or Unreal Engine, this is for programming games from scratch in Visual Studio. I created this 'engine' to save myself some time with getting new games up and running. I've mostly used it for making RPG, Strategy, and Simulation games.

Dependencies:
- MonoGame (included)
- FMOD (not included)

Features:
- Focus on organization, optimizations, and game mechanics (not graphics)
- 98% of the classes are overridable for per-game customizations
- Object-oriented design
- Flexible CharacterManager component consisting of multiple Armies with multiple Squads with multiple Characters for grouping NPCs and/or players
  - Includes large lists of first/last names for random name generation
  - Character class prefit with Inventory, Stats and Skills lists, A* pathing, AI-oriented variables, Health/Mana/Task progress bars, Spellbook, and some basic animation code for spritesheets of 4 animations (can override Animator class for more)
- Basic UI controls for Buttons, Labels, Input boxes, Pictures/Images, Progress Bars, and Sliders
- InputManager component for handling Mouse, Keyboard and up to 4 gamepads
- InventoryManager component for central organization of inventories (inventories are not added to it by default)
- MenuManager component for organizing/iterating/referencing menus
  - Menu class contains lists of all basic UI controls and methods for getting menus built more quickly/easily
- ParticleManager component for handling particles
- SceneManager component for organizing screens/worlds/levels
  - Scene class has its own Menu and World
- SoundManager component for handling basic playing/pausing/stopping of sounds/music/etc using FMOD
- SpellbookManager component for central organization of spellbooks (none are added to it by default)
- Tilemap (named World in library) consisting of multiple maps with multiple layers
  - Includes large list of randomly generated fantasy-sounding names for villages/cities/maps/etc
- AssetManager component for organizing and loading Textures (.png), Shaders (.FxDX), Fonts (.xnb), Sounds, Ambient noise, and Music
- Utilities:
  - Cryptography-grade random number generator for extreme randomness
  - GetLine method for fast ray-tracing between two coordinates (returns list of coordinates between the two points)
- WeatherManager component for handling/rendering basic weather effects (Clear, Rain, Storm, Snow and Fog) with methods for smoothly transitioning between them over time
  - Has its own instance of ParticleManager for rendering
