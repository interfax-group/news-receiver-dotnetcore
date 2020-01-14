using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using publishing_api.Entities;
using System;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace publishing_api.Controllers
{
    /// <summary>
    /// Контроллер публикации
    /// </summary>
    [Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PublishingController : ControllerBase
    {
        /// <summary>
        /// Наименование директории для сохранения события-новости
        /// </summary>
        private static readonly string _directory = "income";

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PublishingController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory?.CreateLogger(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        /// <summary>
        /// Публикация события-новости
        /// </summary>
        /// <response code="401">Несанкционированный доступ</response>
        /// <response code="201">Событие опубликовано</response>
        /// <response code="500">Произошла ошибка</response>
        [HttpPost("eventnews")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult PublishingEventNews([FromBody]EventNews eventNews)
        {
            try
            {
                // Создаем директорию, если ее нет
                CreateDirectoryIfNotExists();

                // Создаем имя файла
                var fileName = MakeFileName(eventNews.news.id);

                // Сериализуем событие - новость
                var eventNewsSerialized = SerializeEventNews(eventNews);

                // Сохраняем в директорию
                StoreEventNews(fileName, eventNewsSerialized);

                _logger?.LogInformation($"Событие-новость: \"{eventNews.news.id}\" успешно сохранена в файл: \"{fileName}\"");

                return new StatusCodeResult(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// Создание директории, если ее не существует
        /// </summary>
        private void CreateDirectoryIfNotExists()
        {
            if (!System.IO.Directory.Exists(_directory))
            {
                System.IO.Directory.CreateDirectory(_directory);
            }
        }

        /// <summary>
        /// Создание имени файла
        /// </summary>
        private string MakeFileName(int newsId)
        {
            return $"{_directory}/{newsId}.json";
        }

        /// <summary>
        /// Сериализация события-новости
        /// </summary>
        private byte[] SerializeEventNews(EventNews eventNews)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eventNews,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }));
        }

        /// <summary>
        /// Сохранение события-новости в директорию
        /// </summary>
        private void StoreEventNews(string fileName, byte[] eventNewsSerialized)
        {
            System.IO.File.WriteAllBytes(fileName, eventNewsSerialized);
        }
    }
}