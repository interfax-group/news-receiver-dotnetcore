using System.Collections.Generic;

namespace publishing_api.Entities
{
    /// <summary>
    /// Данные новости
    /// </summary>
    public class News
    {
        /// <summary>
        /// Идентификатор новости
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Идентификатор родительской новости
        /// </summary>
        public int parent_id { get; set; }

        /// <summary>
        /// Тип новости.
        /// [
        /// "FLASH" - молния,
        /// "EXPRESS" - экспресс,
        /// "NEWS" - новость
        /// ]
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Язык новости.
        /// [
        /// "RU",
        /// "EN"
        /// ]
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// Cлаглайн новости
        /// </summary>
        public string slugline { get; set; }

        /// <summary>
        /// Заголовок новости
        /// </summary>
        public string header { get; set; }

        /// <summary>
        /// Справочная строка
        /// </summary>
        public string trashline { get; set; }

        /// <summary>
        /// Дейтлайн новости
        /// </summary>
        public string dateline { get; set; }

        /// <summary>
        /// Список объектов текста новости
        /// </summary>
        public List<Section> body { get; set; }

        /// <summary>
        /// Список объектов текста новости
        /// </summary>
        public List<Section> background { get; set; }

        /// <summary>
        /// Список авторов новости
        /// </summary>
        public List<Author> authors { get; set; }

        /// <summary>
        /// Список кодов новости
        /// </summary>
        public List<Code> codes { get; set; }

        /// <summary>
        /// Список связанных новостей
        /// </summary>
        public List<Link> links { get; set; }
    };
}
