using RefitSamples.Models;

using System;
using System.Collections.Generic;
using System.Text;
using Refit;
using System.Threading.Tasks;

namespace RefitSamples.SharedApi
{
    public interface IHelloModelApi
    {
        [Get("/api/HelloModels")]
        Task<List<HelloModel>> Get();

        [Get("/api/HelloModels/{id}")]
        Task<HelloModel> Get(Guid id);

        [Put("/api/HelloModels/{id}")]
        Task Put(Guid id, [Body] HelloModel helloModel);

        [Delete("/api/HelloModels/{id}")]
        Task Delete(Guid id);

        [Post("/api/HelloModels")]
        Task<HelloModel> Post([Body] HelloModel helloModel);
    }
}
