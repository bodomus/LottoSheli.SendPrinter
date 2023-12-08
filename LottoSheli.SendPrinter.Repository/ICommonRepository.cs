namespace LottoSheli.SendPrinter.Repository
{
    /// <summary>
    /// Provides common management methods for current storage.
    /// </summary>
    public interface ICommonRepository : IBaseRepository
    {
        /// <summary>
        /// Vacuums current storage.
        /// </summary>
        void VacuumStorage();
    }
}
