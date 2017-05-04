namespace Cmas.DataLayers.CouchDb.Contracts
{
    /// <summary>
    /// Константы БД
    /// </summary>
    internal class DbConsts
    {
        /// <summary>
        /// Имя БД
        /// </summary>
        public const string DbName = "contracts";    //FIXME: перенести в конфиг

        /// <summary>
        /// Строка подключения к БД
        /// </summary>
        public const string DbConnectionString = "http://cmas-backend:backend967@cm-ylng-msk-03:5984";    //FIXME: перенести в конфиг

        /// <summary>
        /// Имя дизайн документа
        /// </summary>
        public const string DesignDocumentName = "contracts";

        /// <summary>
        /// Имя представления всех документов
        /// </summary>
        public const string AllDocsViewName = "all";
    }
}
