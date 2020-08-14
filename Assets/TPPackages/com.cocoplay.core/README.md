# TC core module

Implementation of TC core module

## Runtime Features

### Singleton

- Generic*
  - the singleton implementation for general objects
- Mono*
  - the singleton implementation for MonoBehaviour
- Scriptable*
  - singleton implementation for ScriptableObject


### Utility

- AnimationUtil
  - Sample in Animation or Animator
  
- DictionaryUtil
  - Create dictionary from list
  - Get value in dictionary
  - Foreach in dictionary
  
- EnumUtil
  - Convert string to Enum
  
- ListUtil
  - Create list or array with specific value
  - Get random element in list
  
- MathUtil
  - Approximately with specific deviation
  - Wrap angle in (-180, 180)
  
- ObjectUtil
  - Add/remove component on GameObject or Component
  - Instantiate or create GameObject or Component form Prefab or
    Resources path
  
- RayUtil
  - Create ray from camera by screen position/view port/world position
  - Raycast from camera by screen position/view port/world position
  - Get position from ray
  
- TransformUtil
  - Change position of Transform
  - Remove or set layer for children of Transform
  - Set parent of Transform with stay option
  
- WaitUtil
  - Execute action after wait for time/frame/predicate/yield

### Extension

- AnimationExtensions
  - Sample in Animation or Animator

- ComponentExtensions
  - Add/remove component on Component
  
- DictionaryExtensions
  - Create dictionary from list
  - Get value in dictionary
  - Foreach in dictionary
  
- EnumExtensions
  - Convert string to Enum

- GameObjectExtensions
  - Add/remove component on GameObject

- TransformExtensions
  - Change position of Transform
  - Remove or set layer for children of Transform
  - Set parent of Transform with stay option
  
### DataStructure

- Serializable*
  - Serializable implementation from ISerializationCallbackReceiver

### Property

- EnumFlagAttribute
  - Enum with flags attribute implementation

- OptionalValue
  - Optional value (int, float, Vector2, Vector3, etc) implementation
  
- RangeValue
  - Range value (int, float, Vector2, Vector3, etc) implementation
  
### Widget

- NonRepeatRandom
  - Generate non-repeatable random ids

- TransformRotate
  - Rotate transform automatically
  
  
## Editor Features

### Utility

- EditorPreferences
  - Preferences for menu item name and priority

- AssetEditorUtil
  - Create asset and folder from path

### Inspector

- EnumFlagDrawer
  - Property drawer for EnumFlagAttribute

- OptionalValue
  - Property drawer for OptionalValue (int, float, Vector2, Vector3, etc) 

- RangeValue
  - Property drawer for RangeValue (int, float, Vector2, Vector3, etc)
  
### Menu

- CleanEmptyFolders
  - menu item for remove all empty folders
  
- FindMissingScriptsRecursively
  - menu item for find all missing scripts for specific object
    (recursively)

- FindProjectReferences
  - menu item for find all references in project for specific object
