using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Validations;
using BlogSite.DataAccsess.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Concrete
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        private IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public List<Comment> GetAllComments()
        {
            var res = _commentRepository.GetAllComments();
            if(res is not null)
            {
                return res;
            }
            return null;
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            var res = await _commentRepository.GetAllCommentsAsync();
            if (res is not null)
            {
                return res;
            }
            return null;
        }

        public List<Comment> GetCommentsByPostId(Guid postId)
        {
            var res = _commentRepository.GetCommentsByPostId(postId);
            if(res is not null)
            {
                return res;
            }
            return null;
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            var res = await _commentRepository.GetCommentsByPostIdAsync(postId);
            if (res is not null)
            {
                return res;
            }
            return null;
        }

        public Comment GetCommentById(Guid commentId)
        {
            var res = _commentRepository.GetCommentById(commentId);
            if (res is not null)
            {
                return res;
            }
            return null;
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            var res = await _commentRepository.GetCommentByIdAsync(commentId);
            if (res is not null)
            {
                return res;
            }
            return null;
        }

        public bool CreateComment(CreateCommentVM createCommentVM)
        {
            ValidationTool.Validate(new CommentValidator(), createCommentVM);
            Comment comment = _mapper.Map<Comment>(createCommentVM);
            comment.Id = Guid.NewGuid();
            comment.CreateTime = DateTime.Now;
            var res = _commentRepository.CreateComment(comment);
            if (res == true)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CreateCommentAsync(CreateCommentVM createCommentVM)
        {
            ValidationTool.Validate(new CommentValidator(), createCommentVM);
            Comment comment = _mapper.Map<Comment>(createCommentVM);
            comment.Id = Guid.NewGuid();
            comment.CreateTime = DateTime.Now;
            var res = await _commentRepository.CreateCommentAsync(comment);
            if (res == true)
            {
                return true;
            }
            return false;
        }

        public bool UpdateComment(UpdateCommentVM updateCommentVM, Guid commentId)
        {
            Comment comment = _mapper.Map<Comment>(updateCommentVM);
            comment.Id = commentId;
            var res = _commentRepository.UpdateComment(comment);
            if (res == true)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCommentAsync(UpdateCommentVM updateCommentVM, Guid commentId)
        {
            Comment comment = _mapper.Map<Comment>(updateCommentVM);
            comment.Id = commentId;
            var res = await _commentRepository.UpdateCommentAsync(comment);
            if (res == true)
            {
                return true;
            }
            return false;
        }

        public bool DeleteComment(Guid commentId)
        {
            var res = _commentRepository.DeleteComment(commentId);
            if (res == true) 
            {
                return true;
            };
            return false;
        }

        public async Task<bool> DeleteCommentAsync(Guid commentId)
        {
            var res = await _commentRepository.DeleteCommentAsync(commentId);
            if (res == true)
            {
                return true;
            };
            return false;
        }
    }
}
