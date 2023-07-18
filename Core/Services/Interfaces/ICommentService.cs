using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.DataAccess.Entities.Product;

namespace ShoppingSiteApi.Core.Services.Interfaces
{
    public interface ICommentService : IBaseCRUD<Comment>
    {
        public Task<List<Comment>> GetActiveProductComments(int productId);
        public Task<Comment> Create(AddCommentDTO entity , int userId);

    }
}
