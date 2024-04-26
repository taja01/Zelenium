namespace Zelenium.Shared
{
    /// <summary>
    /// Specifies the device used for setting up the WebDriver.
    /// Used in configuration to determine the appropriate selectors.
    /// </summary>
    public enum Device
    {
        /// <summary>
        /// Represents a desktop device.
        /// When this device type is selected, a desktop WebDriver is created, Desktop selector will be apply.
        /// </summary>
        Desktop,

        /// <summary>
        /// Represents a mobile device.
        /// When this device type is selected, a mobile WebDriver is created, Mobile selector will be apply.
        /// </summary>
        Mobile,
    }
}
