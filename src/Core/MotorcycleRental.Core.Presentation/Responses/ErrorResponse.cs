using System.Text.Json.Serialization;

namespace MotorcycleRental.Core.Presentation.Responses;

public class ErrorResponse
{
    /// <summary>
    /// Disponível quando o status code for 500.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? EventId { get; set; }

    /// <summary>
    /// Erro de código interno da aplicação, útil para debugging dos devs.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Mensagem de erro, estará disponível na maior parte das vezes.
    /// Una exceção existente é o status code 400.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Error { get; set; }

    /// <summary>
    /// Lista de erros de validação, disponível apenas quando o Status Code for 400.
    /// A chave do objeto se refere a qual propriedade dentro do corpo da request teve um erro de validação
    /// Os valores são um array de string com as mensagens de erro emitidas por essa propriedade.
    /// Caso a propriedade possua um "$" significa que o corpo foi inserializável também.
    /// Caso a propriedade possua um "@" significa que uma validação ocorreu e ela não tem propriedades para indicar.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyDictionary<string, IReadOnlyCollection<string>>? ValidationErrors { get; set; }
}
