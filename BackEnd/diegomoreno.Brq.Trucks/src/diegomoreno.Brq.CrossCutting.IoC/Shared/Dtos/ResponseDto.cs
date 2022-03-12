using System.Net;

namespace diegomoreno.Brq.CrossCutting.IoC.Shared.Dtos;

public class ResponseDto : ResponseDto<object> { }

public class ResponseDto<T> where T : class, new()
{
    public ResponseDto() { }

    public ResponseDto(T data) => Data = data;

    public ResponseDto(HttpStatusCode status, T data)
    {
        Status = status;
        Data = data;
    }

    public T? Data { get; set; }

    public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;

    public object Errors { get; set; }

    public virtual BadRequestDto? BadRequestReason { get; set; }
}