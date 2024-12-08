using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace App.Settings
{
    public class Arguments
    {
        #region Singleton

        // implemented as a singleton
        // could also be implemented as a MonoBehaviour, ScriptableObject, service implementation etc.

        private static Arguments m_Instance;

        /// <summary>
        /// Gets the singleton instance of the <see cref="Arguments"/> class.
        /// This property provides access to a single, shared instance of the <see cref="Arguments"/> object,
        /// ensuring that application settings are consistently managed throughout the application lifespan.
        /// </summary>
        public static Arguments Instance
        {
            get { return m_Instance ??= new Arguments(); }
        }

        #endregion

        private readonly Dictionary<string, string> m_AppSettings = new();

        /// <summary>
        /// Represents application settings that can be accessed globally.
        /// </summary>
        /// <remarks>
        /// This class is implemented as a singleton, providing a single
        /// instance that can be accessed throughout the application.
        /// The settings can be loaded from various sources like command-line
        /// arguments, files, or databases.
        /// </remarks>
        private Arguments()
        {
            // potentially load other settings, eg. from file or database
            // ...

            // load command line arguments
            var args = Environment.GetCommandLineArgs();
            ParseCommandLineArguments(args, m_AppSettings);

            LogToConsole();
        }

        /// <summary>
        /// Parses command line arguments into a dictionary of key-value pairs.
        /// </summary>
        /// <param name="args">An array of command line arguments, typically from the main entry point of the application.</param>
        /// <param name="dictionary">A dictionary where each entry's key is derived from the argument name (excluding the "--" prefix), and the value is the subsequent argument.</param>
        private void ParseCommandLineArguments(string[] args, Dictionary<string, string> dictionary)
        {
            // skip first element, usually executable name
            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (!arg.StartsWith("--")) continue;

                string key = arg.Substring(2).ToLowerInvariant();
                string value = args[i + 1];
                dictionary[key] = value;
            }
        }

        /// <summary>
        /// Attempts to retrieve the value associated with the specified key from the application settings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter. This parameter is passed uninitialized.</param>
        /// <returns>True if the application settings contain an element with the specified key; otherwise, false.</returns>
        public bool TryGet(string key, out string value)
        {
            return m_AppSettings.TryGetValue(key.ToLowerInvariant(), out value);
        }

        /// <summary>
        /// Attempts to retrieve and convert the value associated with the specified key from the application settings to the specified type.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve and convert.</param>
        /// <param name="value">When this method returns, contains the converted value associated with the specified key if the key is found and the conversion is successful; otherwise, the default value for the type parameter T. This parameter is passed uninitialized.</param>
        /// <typeparam name="T">The type to which the retrieved value should be converted.</typeparam>
        /// <returns>True if the application settings contain an element with the specified key and the conversion to type T is successful; otherwise, false.</returns>
        public bool TryGet<T>(string key, out T value)
        {
            if (!m_AppSettings.TryGetValue(key.ToLowerInvariant(), out string stringValue))
            {
                value = default;
                return false;
            }

            try
            {
                value = (T) Convert.ChangeType(stringValue, typeof(T));
                return true;
            }
            catch (Exception e) when (e is InvalidCastException or FormatException)
            {
                Debug.LogWarning($"Failed to convert value for key '{key}' to type '{typeof(T)}': {e.Message}");
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Outputs all current application settings to the console.
        /// </summary>
        /// <remarks>
        /// This method constructs a formatted string of all key-value pairs in the application settings and logs it to the console for debugging or informational purposes.
        /// </remarks>
        private void LogToConsole()
        {
            StringBuilder sb = new();
            sb.AppendLine("Arguments:");
            foreach (var setting in m_AppSettings)
            {
                sb.AppendLine($"{setting.Key} := {setting.Value}");
            }

            Debug.Log(sb.ToString());
        }
    }
}