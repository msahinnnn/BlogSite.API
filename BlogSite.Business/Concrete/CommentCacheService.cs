using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Validations;
using BlogSite.Core.Utilities.Results;
using BlogSite.DataAccsess.Abstract;
using BlogSite.DataAccsess.Concrete.AdoNet;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class CommentCacheService : ICommentCacheService
    {
        private IDistributedCache _distributedCache;
        private ICommentRepository _commentRepository;
        private IMapper _mapper;

        public CommentCacheService(IMapper mapper, IDistributedCache distributedCache, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _distributedCache = distributedCache;
            _commentRepository = commentRepository;
        }

        public async Task<IDataResult<Comment>> CreateAsync(CreateCommentVM createCommentVM)
        {
            ValidationTool.Validate(new CommentValidator(), createCommentVM);
            Comment comment = _mapper.Map<Comment>(createCommentVM);
            comment.Id = Guid.NewGuid();
            comment.CreateTime = DateTime.Now;
            var res = await _commentRepository.CreateAsync(comment);
            if (res == true)
            {
                var jsonComment = JsonConvert.SerializeObject(comment);
                await _distributedCache.SetStringAsync($"comment:{comment.Id}", jsonComment);
                return new DataResult<Comment>(comment, true, "Comment successfully created...");
            }
            return new DataResult<Comment>(null, false, "Something went wrong! Please try again.");
        }

        public async Task<IDataResult<Comment>> GetByIdAsync(Guid id)
        {
            var res = await _distributedCache.GetStringAsync($"comment:{id.ToString()}");
            Comment comment = JsonConvert.DeserializeObject<Comment>(res);
            return new DataResult<Comment>(comment, true, "Cached Comment by Id...");
        }

        public async Task<IResult> RemoveAsync(Guid id)
        {
            var res = await _commentRepository.DeleteAsync(id);
            if (res == true)
            {
                await _distributedCache.RemoveAsync(id.ToString());
                return new Result(true, "Comment successfully deleted...");
            }
            return new Result(false, "Something went wrong! Please try again.");
        }

    }
}
