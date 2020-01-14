namespace publishing_api.Entities
{
    /// <summary>
    /// Автор новости
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// ФИО автора
        /// </summary>
        public string name { get; set; }
    };
}
