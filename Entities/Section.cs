namespace publishing_api.Entities
{
    /// <summary>
    /// Объект текста новости
    /// </summary>
    public class Section
    {
        /// <summary>
        /// Тип объекта.
        /// [
        /// "TEXT" - абзац текста,
        /// "TABLE" - строка таблицы
        /// ]
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Содержимое объекта
        /// </summary>
        public string data { get; set; }
    };
}
