using MediatR;

namespace Ossum.CQRS;

public interface ICommand<out TResponse> : IRequest<TResponse>;
