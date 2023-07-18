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
        private readonly IGenericRepository<Product> _ProductRepository;
        public CommentService(IGenericRepository<Comment> CommentRepository, IGenericRepository<Product> productRepository) : base(CommentRepository)
        {
            _Commentrepository = CommentRepository;
            _ProductRepository = productRepository;

        }

        public async Task<List<Comment>> GetActiveProductComments(int productId)
        {
            return await _Commentrepository.GetEntitiesQuery().Where(x => !x.IsDelete && x.ProductId == productId).ToListAsync();
        }

        async Task<Comment> ICommentService.Create(AddCommentDTO entity , int userId)
        {
            var product = await _ProductRepository.GetEntitiesAsyncById(entity.ProductId);
            if (product != null)
            {
                Comment comment = new()
                {
                    CommentText = entity.CommentText,
                    IsDelete = entity.IsDelete,
                    ProductId = entity.ProductId,
                    UserId = userId,
                };
                await _Commentrepository.AddEntity(comment);
                await _Commentrepository.SaveChenges();

                return comment;
            }
            return null;
            
        }

    }
}
