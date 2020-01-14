using System;

namespace publishing_api.Entities
{
    /// <summary>
    /// Событие-новость
    /// </summary>
    public class EventNews
    {
        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Тип события.
        /// [
        /// "PUBLICATION" - первичное опубликование,
        /// "REPUBLICATION" - переопубликование новости,
        /// "REVOCATION" - аннулирование новости
        /// ]
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Дата и время события в формате ISO 8601 YYYY-MM-DDTHH:MM:SS+00:00 (во временной зоне UTC)
        /// </summary>
        public DateTime datetime { get; set; }

        /// <summary>
        /// Данные новости
        /// </summary>
        public News news { get; set; }
    };
}
