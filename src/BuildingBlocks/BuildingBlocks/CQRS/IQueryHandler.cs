using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.CQRS
{
    public interface IQueryHandler<in TQuery,TRespose>:IRequestHandler<TQuery,TRespose> 
        where TQuery:IQuery<TRespose>
        where TRespose: notnull
    {
    }
}
