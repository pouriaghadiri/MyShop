using Microsoft.EntityFrameworkCore;
using ShoppingSiteApi.Core.DTOs.Products;
using ShoppingSiteApi.Core.Services.Interfaces;
using ShoppingSiteApi.DataAccess.Entities.Product;
using ShoppingSiteApi.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSiteApi.Core.Services.Implementations
{
    public class CommentService : BaseCRUD<Comment>, ICommentService
    {
        private readonly IGenericRepository<Comment> _Commentrepository;
        public CommentService(IGenericRepository<Comment> CommentRepository):base(CommentRepository)
        {
            _Commentrepository = CommentRepository;
        }

        public async Task<List<Comment>> GetActiveProductComments(int productId)
        {
            return await _Commentrepository.GetEntitiesQuery().Where(x => !x.IsDelete && x.ProductId == productId).ToListAsync();
        }

        async Task<Comment> ICommentService.Create(CommentDTO entity)
        {
            Comment comment = new()
            {
                CommentText = entity.CommentText,
                CreatedWhen = entity.CreatedWhen,
                IsDelete = entity.IsDelete,
                ProductId = entity.ProductId,
                UserId = entity.UserId,
            };
            await _Commentrepository.AddEntity(comment);
            await _Commentrepository.SaveChenges();

            return comment;
        }

    }
}
