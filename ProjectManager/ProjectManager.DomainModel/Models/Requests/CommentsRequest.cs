﻿namespace ProjectManager.DomainModel.Models.Requests
{
    public class CommentsRequest
    {
        public string? Username { get; set; } //email = username

        public DateTime InsertDate { get; set; }

        public string? CommentArea { get; set; }

        public int ProjectObjectId { get; set; }
    }
}
