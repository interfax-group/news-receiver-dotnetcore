namespace publishing_api.Entities
{
    /// <summary>
    /// Связанная новость
    /// </summary>
    public class Link
    {
        /// <summary>
        /// Тип связи.
        /// [
        /// "REPUBLICATION" - текущая новость является переопубликованием,
        /// "REVOCATION" - текущая новость является аннулированием,
        /// "EXTENSION" - текущая новость является расширением
        /// ]
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Идентификатор родительской новости
        /// </summary>
        public int news_id { get; set; }
    };
}
