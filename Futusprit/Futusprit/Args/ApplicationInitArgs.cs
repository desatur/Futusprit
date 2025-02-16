namespace Futusprit
{
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public class ApplicationInitArgs
    {
        /// <summary>
        /// The name of your Application.
        /// </summary>
        public string ApplicationName { get; set; } = "Application";

        /// <summary>
        /// The title of the game window.
        /// </summary>
        public string WindowTitle { get; set; } = "Application";

        /// <summary>
        /// Custom user-defined data or settings for initialization.
        /// </summary>
        public object? CustomData { get; set; }

        /// <summary>
        /// Default constructor for initializing with default values.
        /// </summary>
        public ApplicationInitArgs() { }

        /// <summary>
        /// Constructor for initializing with custom values.
        /// </summary>
        public ApplicationInitArgs(string windowTitle, object? customData = null)
        {
            WindowTitle = windowTitle;
            CustomData = customData;
        }
    }
}
