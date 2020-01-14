namespace publishing_api.Entities
{
    /// <summary>
    /// Код новости
    /// </summary>
    public class Code
    {
        /// <summary>
        /// 18-разрядный идентификатор
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Тип кода и соответствие общепринятым справочникам.
        /// [
        /// "OKATO",
        /// "IPTC",
        /// "OKSM",
        /// "MOEX",
        /// "PRODUCT",
        /// "OTHER"
        /// ]
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Текстовое наименование кода на русском языке
        /// </summary>
        public string name_ru { get; set; }

        /// <summary>
        /// Текстовое наименование кода на английском языке
        /// </summary>
        public string name_en { get; set; }

        /// <summary>
        /// Код
        /// </summary>
        public string code { get; set; }
    };
}
