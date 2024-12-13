# Launch Arguments

This sample project aims to show how to get and use launch arguments (or commandline arguments) in a Unity project.

After building the application launch it from the terminal with custom parameters like:

```powershell
.\LaunchArgumentsSample.exe --portal-user-name "Anton" --app-session-id 1234 --app-avatar-image-url https://placecats.com/neo/256/256
```

## Structure

<img src="./Readme/Dependencies.png" style="width: 581px;" />

The relevant parts of the sample project for that are the files [Bootstrap.cs](./Assets/App/Bootstrap.cs) and [Arguments.cs](./Assets/App/Settings/Arguments.cs).

The class `Bootstrap` is the main entry point for the application and (in this case) the only part of the code that directly makes use of the `Arguments` class to access launch arguments.

<img src="./Readme/DependenciesSimple.png" style="width: 369px;" />

The rest of the app simulates functionality through console logging and basic screen displays, forgoing full implementation of features like multiplayer and avatar loading.

## Flow

Below you can find a flowchart for how the `Bootstrap` class works.

<img src="./Readme/Flowchart.png" style="width: 461px;" />

## `Arguments` class

The arguments class is implemented as a Singleton, although it could easily be refactored to a simple `MonoBehaviour`, `ScriptableObject`, as a service to be used through a service locator (eg. [Reality Collective Service Framework](https://github.com/realitycollective/com.realitycollective.service-framework)) or any other kind of architecture.

It reads the applications launch arguments and makes them easily available through the `TryGet` method. Internally it stores all available launch arguments in a `Dictionary<string, string>` for performant lookup.

### API

```csharp
// The singleton instance of the Arguments class.
static Instance { get; }

// Use this to get any value from the arguments given a key.  
// There is also a non generic version which gets the value as a string, as this is internally what the arguments are stored as.
bool TryGet<T>(string key, out T value)
bool TryGet(string key, out string value)
```

#### Example

```csharp
// > App.exe --my-string-argument "Hello world"

var arguments = Arguments.Instance;

if (arguments.TryGet("my-string-argument", out string myStringArgument))
{
    // do something with the value
    Debug.Log($"Launch argument 'my-string-argument' found: {myStringArgument}");
}
else 
{
    // handle the missing argument
    Debug.LogWarning("Could not find launch argument 'my-string-argument', quitting.");
    Application.Quit();
}
```

## Potential improvements
- Dedicated public properties for common parameters, e.g. in a class derived from `Arguments`.
- "Toggles" without a value, i.e. boolean value: 
  ```powershell
  ./App.exe --enable-passthrough --my-string-argument "Hello World"
            ^^^^^^^^^^^^^^^^^^^^
  ```
- Also capture other launch parameters, e.g. Unity engine parameters with single dashes (e.g. `-screen-fullscreen 0`) for easy access inside the application.