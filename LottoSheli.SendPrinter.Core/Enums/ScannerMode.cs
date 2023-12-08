namespace LottoSheli.SendPrinter.Core.Enums
{
    /// <summary>
    /// Scanner mode
    /// </summary>
    public enum ScannerMode
    {
        /// <summary>
        /// Running physical device
        /// </summary>
        Normal,
        /// <summary>
        /// Running scannner simulator
        /// </summary>
        Demo,
        /// <summary>
        /// Running physical device with new controller
        /// </summary>
        Controller
    }
}
