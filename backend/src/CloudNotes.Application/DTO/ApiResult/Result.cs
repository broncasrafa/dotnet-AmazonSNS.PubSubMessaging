using Newtonsoft.Json;

namespace CloudNotes.Application.DTO.ApiResult;


/// <summary>
/// Classe padrão para retorno dos dados da api
/// </summary>
/// <typeparam name="T">objeto para retornar</typeparam>

[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class Result<T>
{
    private readonly T _value;

    [JsonProperty(Order = -3)]
    public bool IsSuccess { get; }

    [JsonProperty(Order = -2, NullValueHandling = NullValueHandling.Ignore)]
    public T Data
    {
        get
        {
            return _value!;
        }

        private init => _value = value;
    }

    [JsonProperty(Order = -1, NullValueHandling = NullValueHandling.Ignore)]
    public List<string> Errors { get; }


    private Result(T value)
    {
        Data = value;
        IsSuccess = true;
        Errors = null;
    }
    private Result(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            throw new ArgumentException("Invalid error", nameof(errorMessage));

        IsSuccess = false;
        Errors = new List<string> { errorMessage };
    }

    public static Result<T> Success(T value) => new Result<T>(value);
    public static Result<T> Failure(string error) => new Result<T>(error);
}
