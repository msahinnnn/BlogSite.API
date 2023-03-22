using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.Business.Abstract;
using BlogSite.DataAccsess.Abstract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class CommentCacheService : ICommentCacheService
    {
        private const string commentKey = "comments";
        private readonly ICommentRepository _commentRepository;
        private readonly CacheService _cacheService;
        private readonly IDatabase _cacheRepository;
        private IMapper _mapper;

        public CommentCacheService(ICommentRepository commentRepository, CacheService cacheService, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _cacheService = cacheService;
            _cacheRepository = _cacheService.GetDb(0);
            _mapper = mapper;
        }


        public async Task<bool> CreateAsync(CreateCommentVM createCommentVM)
        {
            Comment comment = _mapper.Map<Comment>(createCommentVM);
            comment.Id = Guid.NewGuid();
            comment.CreateTime = DateTime.Now;
            var newComment = await _commentRepository.CreateAsync(comment);

            if (await _cacheRepository.KeyExistsAsync(commentKey))
            {
                await _cacheRepository.HashSetAsync(commentKey, comment.Id.ToString(), JsonSerializer.Serialize(newComment));
            }

            return true;
        }

        public async Task<List<Comment>> GetAsync()
        {
            if (!await _cacheRepository.KeyExistsAsync(commentKey))
                return await LoadToCacheFromDbAsync();

            var comments = new List<Comment>();

            var cacheComments = await _cacheRepository.HashGetAllAsync(commentKey);
            foreach (var item in cacheComments.ToList())
            {
                var comment = JsonSerializer.Deserialize<Comment>(item.Value);

                comments.Add(comment);

            }
            return comments;
        }

        public async Task<Comment> GetByIdAsync(Guid id)
        {
            if (_cacheRepository.KeyExists(commentKey))
            {
                var comment = await _cacheRepository.HashGetAsync(commentKey, id.ToString());
                return comment.HasValue ? JsonSerializer.Deserialize<Comment>(comment) : null;
            }


            var coments = await LoadToCacheFromDbAsync();
            return coments.FirstOrDefault(x => x.Id == id);
        }

        private async Task<List<Comment>> LoadToCacheFromDbAsync()
        {

            var comments = await _commentRepository.GetAllAsync();


            comments.ForEach(p =>
            {
                _cacheRepository.HashSetAsync(commentKey, p.Id.ToString(), JsonSerializer.Serialize(p));

            });

            return comments;


        }
    }
}
