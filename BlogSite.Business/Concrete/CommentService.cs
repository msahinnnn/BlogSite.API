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

        public Task<List<Comment>> GetAllCommentsAsync()
        {
            throw new NotImplementedException();
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

        public Task<List<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            throw new NotImplementedException();
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

        public Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }

        public bool CreateComment(CreateCommentVM createCommentVM)
        {
            //var validation = _validator.Validate(createCommentVM);
            ValidationTool.Validate(new CommentValidator(), createCommentVM);
            //if (validation.IsValid)
            //{
                Comment comment = _mapper.Map<Comment>(createCommentVM);
                comment.Id = Guid.NewGuid();
                comment.CreateTime = DateTime.Now;
                var res = _commentRepository.CreateComment(comment);
                if (res == true)
                {
                    return true;
                }
                return false;
            //}
            //return false;
        }

        public Task<bool> CreateCommentAsync(CreateCommentVM createCommentVM)
        {
            throw new NotImplementedException();
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

        public Task<bool> UpdateCommentAsync(UpdateCommentVM updateCommentVM, Guid commentId)
        {
            throw new NotImplementedException();
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

        public Task<bool> DeleteCommentAsync(Guid commentId)
        {
            throw new NotImplementedException();
        }
    }
}
