﻿namespace Models.User
{

    public class UserGetResp
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateOnly? Created { get; set; }
        public bool? IsActive { get; set; }
    }
}
