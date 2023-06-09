using AutoMapper;
using BlogSite.API.Controllers;
using BlogSite.API.Models;
using BlogSite.API.ViewModels.CommentVMs;
using BlogSite.API.ViewModels.PostVMs;
using BlogSite.Business.Abstract;
using BlogSite.Entities.ViewModels.CommentVMs;
using BlogSite.Entities.ViewModels.PostVMs;
using FakeItEasy;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Test
{
    public class CommentsControllerTest
    {
        public ICommentService _commentService;
        public ICommentCacheService _commentCacheService;
        public IPublishEndpoint _publishEndpoint;
        public IMapper _mapper;
        public CommentsController _commentsController;

        public CommentsControllerTest()
        {
            _commentService = A.Fake<ICommentService>();
            _commentCacheService = A.Fake<ICommentCacheService>();
            _publishEndpoint = A.Fake<IPublishEndpoint>();
            _mapper = A.Fake<IMapper>();
            _commentsController = new CommentsController(_commentService, _publishEndpoint, _commentCacheService);
        }

        [Fact]
        public async void GetPost_ReturnsOK()
        {
            var comments = A.Fake<List<Comment>>();
            A.CallTo(() => _commentCacheService.GetAsync()).Returns(comments);

            var result = await _commentsController.GetAsync();
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetPostByIdPost_ReturnsOK()
        {
            var comment = A.Fake<Comment>();
            A.CallTo(() => _commentCacheService.GetByIdAsync(comment.Id)).Returns(comment);

            var result = await _commentsController.GetCommentByIdAsync(comment.Id);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetAllPostsByUserIdPost_ReturnsOK()
        {
            var post = A.Fake<Post>();
            var comment = A.Fake<List<Comment>>();
            A.CallTo(() => _commentService.GetCommentsByPostIdAsync(post.Id)).Returns(comment);

            var result = await _commentsController.GetAllCommentsByPostIdAsync(post.Id);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void CreatePost_ReturnsOK()
        {
            var commentVM = new CreateCommentVM()
            {
                Content = "Test Content",
            };

            var comment = A.Fake<Comment>();

            A.CallTo(() => _mapper.Map<Comment>(commentVM)).Returns(comment);

            A.CallTo(() => _commentService.CreateAsync(commentVM)).Returns(comment);
            Assert.NotNull(comment);

            var result = await _commentsController.CreateAsync(commentVM);
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async void UpdatePost_ReturnsOK()
        {
            var commentVM = new UpdateCommentVM()
            {
                Content = "Test Content",
            };

            var comment = A.Fake<Comment>();

            A.CallTo(() => _mapper.Map<Comment>(commentVM)).Returns(comment);
            var id = Guid.NewGuid();
            A.CallTo(() => _commentService.UpdateAsync(commentVM, comment.Id)).Returns(true);

            var result = await _commentsController.UpdateAsync(commentVM, comment.Id);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async void DeletePost_ReturnsOK()
        {
            var comment = A.Fake<Comment>();

            A.CallTo(() => _commentService.DeleteAsync(comment.Id)).Returns(true);
            Assert.NotNull(comment);

            var result = await _commentsController.DeleteAsync(comment.Id);
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }
    }
}
