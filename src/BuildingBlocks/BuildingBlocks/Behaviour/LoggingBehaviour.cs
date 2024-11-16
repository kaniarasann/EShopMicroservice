using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Behaviour
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest,TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull,  IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[Start] Handle request={Request} - Response={Response} - RequestData ={RequestData}",
                                  typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();

            timer.Start();

            var response = await next();

            timer.Stop();

            if (timer.Elapsed.Seconds > 5)
                logger.LogWarning(
                    "[Perfomance] the {request} with {response} took time interval of {timer.Elapsed.Seconds} seconds the request type {reqType} and response type {resType}",
                    request, response, timer.Elapsed.Seconds, typeof(TRequest).Name,typeof(TResponse).Name);


            logger.LogInformation("[End] Request has been handled sucessfully with the {Request} - Response={Response} - Response data ={RequestData}",
                                   typeof(TRequest).Name, typeof(TResponse).Name, response);
            return response;
        }

    }
}
