using MediatR;

namespace Ossum.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>;
