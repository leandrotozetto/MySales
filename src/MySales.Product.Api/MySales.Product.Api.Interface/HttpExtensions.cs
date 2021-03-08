using Microsoft.AspNetCore.Http;
using MySales.Product.Api.Domain.Core.Entities.Interfaces;
using MySales.Product.Api.Domain.Core.Json;
using MySales.Product.Api.Domain.Dtos.Product;
using System;
using System.Buffers;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MySales.Product.Api.Interface
{
    //public interface IApiResponse
    //{
    //    /// <summary>
    //    /// Write json in response.
    //    /// </summary>
    //    /// <param name="response">HttResponse will be written the message.</param>
    //    /// <param name="result">Data that will be serialized and returned.</param>
    //    /// <returns></returns>
    //    Task Create<T>(T result) where T : IEmpty<T>;

    //    /// <summary>
    //    /// Write json in response.
    //    /// </summary>
    //    /// <param name="response">HttResponse will be written the message.</param>
    //    /// <param name="result">Data that will be serialized and returned.</param>
    //    /// <returns></returns>
    //    Task Create();
    //}

    //public class ApiResponse : IApiResponse
    //{
    //    private readonly INotification _notification;

    //    private readonly HttpResponse _httpResponse;

    //    public ApiResponse(INotification notification,
    //        HttpResponse httpResponse)
    //    {
    //        _notification = notification;
    //        _httpResponse = httpResponse;
    //    }

    //    /// <summary>
    //    /// Write json in response.
    //    /// </summary>
    //    /// <param name="response">HttResponse will be written the message.</param>
    //    /// <param name="result">Data that will be serialized and returned.</param>
    //    /// <returns></returns>
    //    public async Task Create<T>(T result) where T : IEmpty<T>
    //    {
    //        if(result is null)
    //        {
    //            Create();
    //        }

    //        if (_notification.HasErrors)
    //        {
    //            await _httpResponse.Error(_notification);

    //            return;
    //        }

    //        if (result.IsEmpty)
    //        {
    //            _httpResponse.NoContent();

    //            return;
    //        }

    //        await _httpResponse.Ok(result);
    //    }

    //    /// <summary>
    //    /// Write json in response.
    //    /// </summary>
    //    /// <param name="response">HttResponse will be written the message.</param>
    //    /// <param name="result">Data that will be serialized and returned.</param>
    //    /// <returns></returns>
    //    public async Task Create()
    //    {
    //        if (_notification.HasErrors)
    //        {
    //            await _httpResponse.Error(_notification);

    //            return;
    //        }

    //        _httpResponse.NoContent();
    //    }

    //    /// <summary>
    //    /// Converts json to entity
    //    /// </summary>
    //    /// <typeparam name="T">Type.</typeparam>
    //    /// <param name="buffer">Byte[] containing json.</param>
    //    /// <returns>returns new entity</returns>
    //    public T ConvertToEntity<T>(ReadOnlySequence<byte> buffer)
    //    {
    //        var utf8Reader = new Utf8JsonReader(buffer);
    //        var serializerOptions = CreateSerializerOptions();

    //        return JsonSerializer.Deserialize<T>(ref utf8Reader, serializerOptions);
    //    }

    //    /// <summary>
    //    /// Create a SerializeOption
    //    /// </summary>
    //    /// <returns>returns new SerializeOption.</returns>
    //    private JsonSerializerOptions CreateSerializerOptions()
    //    {
    //        return CustomSerializeOption.New()
    //            .AddConverter(new CustomJsonConverter<ProductCommandDto>())
    //            .Option();
    //    }

    //    /// <summary>
    //    /// Write json in response.
    //    /// </summary>
    //    /// <param name="response">HttResponse will be written the message.</param>
    //    /// <param name="result">Data that will be serialized and returned.</param>
    //    /// <returns></returns>
    //    private async Task Ok(object result = null)
    //    {
    //        SetStatusCode(HttpStatusCode.OK);

    //        if (result != null)
    //        {
    //            var json = ConvertToJson(result);

    //            await _httpResponse.WriteAsync(json);
    //        }
    //    }

    //    /// <summary>
    //    /// Write json in response.
    //    /// </summary>
    //    /// <param name="response">HttResponse will be written the message.</param>
    //    private HttpResponse NoContent()
    //    {
    //        SetStatusCode(HttpStatusCode.NoContent);

    //        return _httpResponse;
    //    }

    //    /// <summary>
    //    /// Set ContentType (to Json) and httpStatusCode.
    //    /// </summary>
    //    /// <param name="httpStatusCode">HttpStatusCode to response</param>
    //    private void SetStatusCode(HttpStatusCode httpStatusCode)
    //    {
    //        _httpResponse.ContentType = "application/json";
    //        _httpResponse.StatusCode = (int)httpStatusCode;
    //    }

    //    /// <summary>
    //    /// Converts entity to json.
    //    /// </summary>
    //    /// <param name="entity">entity.</param>
    //    /// <returns>Returns string json.</returns>
    //    private string ConvertToJson(object entity)
    //    {
    //        var serializerOptions = CreateSerializerOptions();

    //        return JsonSerializer.Serialize(entity, serializerOptions);
    //    }
    //}

    /// <summary>
    /// Extensions for HttpContext.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Read json from HttpRequest.
        /// </summary>
        /// <typeparam name="T">Type of entity to be written</typeparam>
        /// <param name="httpContext">Context of the request.</param>
        /// <param name="webHostEnvironment">provider data from web hosting.</param>
        /// <returns></returns>
        public static async ValueTask<T> ReadFromJson<T>(this HttpContext httpContext)
        {
            var pipeReader = await httpContext.Request.BodyReader.ReadAsync();
            var buffer = pipeReader.Buffer;

            return ConvertToEntity<T>(buffer);
        }

        /// <summary>
        /// Create a SerializeOption
        /// </summary>
        /// <returns>returns new SerializeOption.</returns>
        private static JsonSerializerOptions CreateSerializerOptions()
        {
            return CustomSerializeOption.New()
                .AddConverter(new CustomJsonConverter<ProductCommandDto>())
                .Option();
        }

        /// <summary>
        /// Converts json to entity
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="buffer">Byte[] containing json.</param>
        /// <returns>returns new entity</returns>
        public static T ConvertToEntity<T>(ReadOnlySequence<byte> buffer)
        {
            var utf8Reader = new Utf8JsonReader(buffer);
            var serializerOptions = CreateSerializerOptions();

            return JsonSerializer.Deserialize<T>(ref utf8Reader, serializerOptions);
        }

        /// <summary>
        /// Converts entity to json.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>Returns string json.</returns>
        public static string ConvertToJson(object entity)
        {
            var serializerOptions = CreateSerializerOptions();

            return JsonSerializer.Serialize(entity, serializerOptions);
        }

        /// <summary>
        /// Reads a value from header
        /// </summary>
        /// <param name="httpContext">Context of the request.</param>
        /// <param name="hostingEnvironment">provider data from web hosting.</param>
        /// <param name="key">Key gets value returned.</param>
        /// <returns>Returns string with value.</returns>
        public static string ReadFromHeader(this HttpContext httpContext, string key)
        {
            httpContext.Request.Headers.TryGetValue(key, out var values);

            return values.FirstOrDefault();
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        /// <param name="result">Data that will be serialized and returned.</param>
        /// <returns></returns>
        public static async Task Error(this HttpResponse response, Domain.Core.Notifications.Interfaces.INotification notification)
        {
            response.SetStatusCode(GetHttpStatusStatus(notification));

            if (notification.HasErrors)
            {
                var json = ConvertToJson(notification.Errors);

                await response.WriteAsync(json);
            }
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        /// <param name="result">Data that will be serialized and returned.</param>
        /// <returns></returns>
        public static async Task Create<T>(this HttpResponse response, Domain.Core.Notifications.Interfaces.INotification notification, T result) where T : IEmpty<T>
        {
            if (notification.HasErrors)
            {
                await response.Error(notification);

                return;
            }

            if (result.IsEmpty)
            {
                response.NoContent();

                return;
            }

            if (result != null)
            {
                var json = ConvertToJson(result);

                await response.WriteAsync(json);
            }
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        /// <param name="result">Data that will be serialized and returned.</param>
        /// <returns></returns>
        public static async Task Create(this HttpResponse response, Domain.Core.Notifications.Interfaces.INotification notification)
        {
            if (notification.HasErrors)
            {
                await response.Error(notification);

                return;
            }

            response.NoContent();
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        /// <param name="result">Data that will be serialized and returned.</param>
        /// <returns></returns>
        public static async Task Ok(this HttpResponse response, object result = null)
        {
            response.SetStatusCode(HttpStatusCode.OK);

            if (result != null)
            {
                var json = ConvertToJson(result);

                await response.WriteAsync(json);
            }
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        public static HttpResponse NoContent(this HttpResponse response)
        {
            response.SetStatusCode(HttpStatusCode.NoContent);

            return response;
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        public static async Task SuccessAsync<T>(this HttpResponse httpResponse, T response)
        {
            if (response is null)
            {
                httpResponse.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            await httpResponse.WriteAsync(JsonSerializer.Serialize(response,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));

        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        /// <returns></returns>
        public static HttpResponse Unauthorized(this HttpResponse response)
        {
            response.SetStatusCode(HttpStatusCode.Unauthorized);

            return response;
        }

        /// <summary>
        /// Set ContentType (to Json) and httpStatusCode.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        /// <param name="httpStatusCode">HttpStatusCode to response</param>
        private static void SetStatusCode(this HttpResponse response, HttpStatusCode httpStatusCode)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;
        }

        private static HttpStatusCode GetHttpStatusStatus(Domain.Core.Notifications.Interfaces.INotification notification)
        {
            Enum.TryParse<HttpStatusCode>(notification.ErrorCode.ToString(), out var httpStatusCode);

            return httpStatusCode;
        }
    }
}
