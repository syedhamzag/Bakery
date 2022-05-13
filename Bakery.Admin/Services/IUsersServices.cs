using Bakery.DataTransferObject.DTOs.Users;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bakery.Admin.Services
{
    public interface IUsersServices
    {
        [Get("/User")]
        Task<List<GetUser>> Get();


        [Get("/User/Get/{id}")]
        Task<GetUser> Get(int id);

        [Post("/User/Post")]
        Task<HttpResponseMessage> Post([Body] PostUser User);

        [Put("/User/Put")]
        Task<HttpResponseMessage> Put([Body] PutUser User);

    }
}
