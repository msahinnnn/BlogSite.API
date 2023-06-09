﻿using BlogSite.Messages.Events;
using Caching.Abstract;
using Caching.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.API.Caching.Consumers
{
    public class CommentUpdatedEventConsumer : MassTransit.IConsumer<CommentUpdatedEvent>
    {
        private ICommentService _commentService;

        public CommentUpdatedEventConsumer(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task Consume(ConsumeContext<CommentUpdatedEvent> context)
        {
            Console.WriteLine(context.Message);
            var x = await _commentService.SaveOrUpdateAsync(new Comment()
            {
                Id = context.Message.Id,
                Content = context.Message.Content,
                UserId = context.Message.UserId,
                PostId = context.Message.PostId,
                CreateTime = context.Message.CreateTime,
            });
            Console.WriteLine(nameof(CommentUpdatedEventConsumer) + "- worked");
        }
    }
}
