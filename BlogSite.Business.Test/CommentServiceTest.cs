using AutoMapper;
using BlogSite.API.Models;
using BlogSite.API.Validations;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.Business.Concrete;
using BlogSite.Business.Validations;
using BlogSite.DataAccsess.Abstract;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Business.Test
{
    public class CommentServiceTest
    {
        private ICommentRepository _commentRepository;
        private IMapper _mapper;
        private IAuthService _authService;
        private ICommentService _commentService;
        public CommentServiceTest()
        {
            _commentRepository = A.Fake<ICommentRepository>();
            _mapper = A.Fake<IMapper>();
            _authService = A.Fake<IAuthService>();
            _commentService = new CommentService(_commentRepository, _mapper, _authService);
        }

        [Fact]
        public async void GetComments_ReturnsComments()
        {
            var comments = A.Fake<List<Comment>>();
            A.CallTo(() => _commentRepository.GetAllAsync()).Returns(comments);
            var result = await _commentService.GetAllAsync();
            Assert.IsAssignableFrom<List<Comment>>(result);
        }

        [Fact]
        public async void GetCommentById_ReturnsComment()
        {
            var id = Guid.NewGuid();
            var comment = A.Fake<Comment>();
            A.CallTo(() => _commentRepository.GetByIdAsync(id)).Returns(comment);

            var result = await _commentService.GetByIdAsync(id);
            Assert.IsAssignableFrom<Comment>(result);
        }

        [Fact]
        public async void GetCommentsByPostId_ReturnsCommentsPerPost()
        {
            var id = Guid.NewGuid();
            var comments = A.Fake<List<Comment>>();
            A.CallTo(() => _commentRepository.GetCommentsByPostIdAsync(id)).Returns(comments);

            var result = await _commentService.GetCommentsByPostIdAsync(id);
            Assert.IsAssignableFrom<List<Comment>>(result);
        }

        [Fact]
        public async void CreateComment_ReturnsComment()
        {
            var id = Guid.NewGuid();
            var commentVM = new CreateCommentVM()
            {
                Content = "Test Content",
            };
            var comment = A.Fake<Comment>();

            ValidationTool.Validate(new CommentValidator(), commentVM);

            A.CallTo(() => _commentRepository.GetByIdAsync(id)).Returns(comment);
            A.CallTo(() => _mapper.Map<Comment>(commentVM)).Returns(comment);
            A.CallTo(() => _commentRepository.CreateAsync(comment)).Returns(comment);

            var result = await _commentService.CreateAsync(commentVM);
            Assert.NotNull(result);

        }

        [Fact]
        public async void UpdateComment_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var commentVM = new CreateCommentVM()
            {
                Content = "Test Content",
            };
            var comment = A.Fake<Comment>();

            A.CallTo(() => _commentRepository.GetByIdAsync(id)).Returns(comment);
            A.CallTo(() => _mapper.Map<Comment>(commentVM)).Returns(comment);
            A.CallTo(() => _commentRepository.UpdateAsync(comment)).Returns(true);

            var result = await _commentService.UpdateAsync(commentVM, id);
            Assert.True(result);
        }

        [Fact]
        public async void Deletecomment_ReturnsTrue()
        {
            var id = Guid.NewGuid();
            var comment = A.Fake<Comment>();

            A.CallTo(() => _commentRepository.GetByIdAsync(id)).Returns(comment);
            A.CallTo(() => _commentRepository.DeleteAsync(id)).Returns(true);

            var result = await _commentService.DeleteAsync(id);
            Assert.True(result);
        }
    }
}
